using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using API.Common;
using API.DoMain;
using API.DoMain.CusAttribute;
using API.DoMain.Customer;
using API.DoMain.Dto;
using API.DoMain.Menu;
using API.DoMain.Req;
using API.Models;
using API.Utility;
using API.Utility.Middleware;
using Newtonsoft.Json;

namespace API.Controllers.Manage
{
  /// <summary>
  /// 举报/检举相关
  /// </summary>
  [RoutePrefix("manage/accusation")]
  [ManagesAuthorize("Manage")]
  public class AccusationManageController : ApiController
  {
    #region 作品

    /// <summary>
    /// 分页获取作品举报列表
    /// </summary>
    /// <param name="page">页数</param>
    /// <param name="size">条数</param>
    /// <param name="startTime">开始时间 yyyy.mm.dd</param>
    /// <param name="endTime">结束时间 yyyy.mm.dd</param>
    /// <returns></returns>
    [HttpGet, Route("work/pageList")]
    public Result WorkPageList(int page = ParamConst.PageIndex, int size = ParamConst.PageSize
      , string startTime = null, string endTime = null,int? status = null,string searchKey = null)
    {
      var whereList = new List<string>
      {
        $"AccusationT.a_TargetType = {(int) AccusationTargetTypes.Comment}",
      };

      if (status.HasValue)
      {
        whereList.Add($"AccusationT.a_Status = {status}");
      }

      if (!string.IsNullOrWhiteSpace(searchKey))
      {
        whereList.Add($"AccusationT.a_Reason LIKE '%{searchKey}%'");
      }

      if (!string.IsNullOrWhiteSpace(startTime))
      {
        whereList.Add($"AccusationT.a_CreateTime >= '{startTime}'");
      }

      if (!string.IsNullOrWhiteSpace(endTime))
      {
        whereList.Add($"AccusationT.a_CreateTime <= '{endTime}'");
      }

      var whereSql = string.Join($"\nAND ", whereList);

      var pageList =
        AssistHelper.PagingList("manage_accusation_work_get_page_list", page, size,
          new[]
          {
            new SqlParameter("@strW", whereSql),
            new SqlParameter("@order", "AccusationT.a_Id DESC"),
          });

      return new Success(pageList);
    }


    /// <summary>
    /// 分页获取作品举报列表[带分组]
    /// </summary>
    /// <param name="page">页数</param>
    /// <param name="size">条数</param>
    /// <param name="status">举报状态 0-举报无效 1-待处理 2-举报成功</param>
    /// <param name="startTime">开始时间 yyyy.mm.dd</param>
    /// <param name="endTime">结束时间 yyyy.mm.dd</param>
    /// <returns></returns>
    [HttpGet, Route("work/pageList/group")]
    [Permission("UserCenter", "accusation_workGroupPageList")]
    public Result WorkGroupPageList(int page = ParamConst.PageIndex, int size = ParamConst.PageSize
      , int? status = null, string startTime = null, string endTime = null)
    {
      var whereList = new List<string>();

      if (!string.IsNullOrWhiteSpace(startTime))
      {
        whereList.Add($"AccusationT.a_CreateTime >= '{startTime}'");
      }

      if (!string.IsNullOrWhiteSpace(endTime))
      {
        whereList.Add($"AccusationT.a_CreateTime <= '{endTime}'");
      }

      var whereSql = string.Empty;

      if (whereList.Count > 0)
      {
        whereSql = $" AND {string.Join($"\nAND ", whereList)}";
      }
      var statusWhere = string.Empty;

      if (status.HasValue)
      {
        statusWhere = $"AND a_Status = {status.Value}";
      }

      var pageList =
        AssistHelper.PageList<AccusationGroupDto<WorkDto>>("manage_accusation_work_get_page_list_group", page, size,
          new[]
          {
            new SqlParameter("@strW", whereSql),
            new SqlParameter("@order", "AccusationT.accusationCount DESC"),
            new SqlParameter("@statusW",statusWhere),
          });


      return new Success(pageList);
    }


    /// <summary>
    /// 举报成功/信息处理
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    [HttpPost, Route("work/accept")]
    public Result WorkAccept([FromBody] AcceptReq req)
    {
      if (req == null)
      {
        return new Failure("para is null");
      }

      var msg = req.ValidInfo();
      if (msg != string.Empty)
      {
        return new Failure(msg);
      }

      var result = SqlHelper.ExecuteNonQuery(SqlHelper.GetConnString(), CommandType.StoredProcedure,
        "accusation_edit_item_status",
        new SqlParameter("@id", req.Id),
        new SqlParameter("@status", 2),
        new SqlParameter("@userId", req.UserId));

      if (result > 0)
      {
        //p:发送举报成功通知

        var param = new EditWorkStatusReq()
        {
          Id = req.RelationId,
          Status = (int) WorkStatusTypes.REMOVE,
          UserId = req.RelationUserId
        };

        result =
          SqlHelper.ExecuteNonQuery(SqlHelper.GetConnString(), CommandType.StoredProcedure, StoreProcedureConst
              .EditWorkStatus,
            EntitySqlHelper.GetParameters(param));

        if (result > 0)
        {
          //p:发送被删除通知

          return new Success(ResultMsgConst.OperationSuccess);
        }
        else
        {
          return new Failure(ResultMsgConst.OperationFailure);
        }
      }
      else
      {
        return new Failure(ResultMsgConst.OperationFailure);
      }
    }


    /// <summary>
    /// 删除作品
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    [HttpPost, Route("work/remove")]
    [Permission("UserCenter", "accusation_acceptWork")]
    public Result RemoveWork([FromBody] EditWorkStatusReq req)
    {

      req.Status = (int) WorkStatusTypes.REMOVE;

      var result =
        SqlHelper.ExecuteNonQuery(SqlHelper.GetConnString(), CommandType.StoredProcedure, StoreProcedureConst
            .EditWorkStatus,
          EntitySqlHelper.GetParameters(req));

      if (result > 0)
      {
        //p:发送被删除通知

        return EditStatusByRelation(new AccusationEditStatusByRelationParam()
        {
          RelationId = req.Id,
          Status = 2,
          TargetType = AccusationTargetTypes.Work
        });
      }
      else
      {
        return new Failure(ResultMsgConst.OperationFailure);
      }
    }

    /// <summary>
    /// 清空此作品相关的举报
    /// </summary>
    /// <param name="workId">作品编号</param>
    /// <returns></returns>
    [HttpDelete, Route("work/clear")]
    [Permission("UserCenter", "accusation_clearWork")]
    public Result ClearWork([FromUri] int workId)
    {
      return EditStatusByRelation(new AccusationEditStatusByRelationParam()
      {
        RelationId = workId,
        Status = 0,
        TargetType = AccusationTargetTypes.Work
      });
    }


    #endregion

    #region 评论

    /// <summary>
    /// 分页获取评论举报列表
    /// </summary>
    /// <param name="page">页数</param>
    /// <param name="size">条数</param>
    /// <param name="startTime">开始时间 yyyy.mm.dd</param>
    /// <param name="endTime">结束时间 yyyy.mm.dd</param>
    /// <param name="status">举报状态 0-举报无效 1-待处理 2-举报成功</param>
    /// <param name="searchKey">搜索关键字</param>
    /// <returns></returns>
    [HttpGet, Route("comment/pageList")]
    public Result CommentPageList(int page = ParamConst.PageIndex, int size = ParamConst.PageSize,
      string startTime = null, string endTime = null,int? status = null,string searchKey = null)
    {
      var whereList = new List<string>
      {
        $"AccusationT.a_TargetType = {(int) AccusationTargetTypes.Comment}",
      };

      if (status.HasValue)
      {
        whereList.Add($"AccusationT.a_Status = {status}");
      }

      if (!string.IsNullOrWhiteSpace(searchKey))
      {
        whereList.Add($"AccusationT.a_Reason LIKE '%{searchKey}%'");
      }

      if (!string.IsNullOrWhiteSpace(startTime))
      {
        whereList.Add($"AccusationT.a_CreateTime >= '{startTime}'");
      }

      if (!string.IsNullOrWhiteSpace(endTime))
      {
        whereList.Add($"AccusationT.a_CreateTime <= '{endTime}'");
      }

      var whereSql = string.Join($"\nAND ", whereList);

      var pageList =
        AssistHelper.PagingList("manage_accusation_comment_get_page_list", page, size,
          new[]
          {
            new SqlParameter("@strW", whereSql),
            new SqlParameter("@order", "AccusationT.a_Id DESC"),
          });

      return new Success(pageList);
    }

    /// <summary>
    /// 分页获取评论举报列表[带分组]
    /// </summary>
    /// <param name="page">页数</param>
    /// <param name="size">条数</param>
    /// <param name="startTime">开始时间 yyyy.mm.dd</param>
    /// <param name="endTime">结束时间 yyyy.mm.dd</param>
    /// <param name="status">举报状态 0-举报无效 1-待处理 2-举报成功</param>
    /// <returns></returns>
    [HttpGet, Route("comment/pageList/group")]
    [Permission("UserCenter", "accusation_commentGroupPageList")]
    public Result CommentGroupPageList(int page = ParamConst.PageIndex, int size = ParamConst.PageSize
      , int? status = null, string startTime = null, string endTime = null)
    {
      var whereList = new List<string>();

      if (!string.IsNullOrWhiteSpace(startTime))
      {
        whereList.Add($"AccusationT.a_CreateTime >= '{startTime}'");
      }

      if (!string.IsNullOrWhiteSpace(endTime))
      {
        whereList.Add($"AccusationT.a_CreateTime <= '{endTime}'");
      }

      var whereSql = string.Empty;

      if (whereList.Count > 0)
      {
        whereSql = $" AND {string.Join($"\nAND ", whereList)}";
      }

      var statusWhere = string.Empty;

      if (status.HasValue)
      {
        statusWhere = $"AND a_Status = {status.Value}";
      }

      var pageList =
        AssistHelper.PageList<AccusationGroupDto<CommentDto>>("manage_accusation_comment_get_page_list_group", page,
          size,
          new[]
          {
            new SqlParameter("@strW", whereSql),
            new SqlParameter("@order", "AccusationT.accusationCount DESC"),
            new SqlParameter("@statusW",statusWhere), 
          });

      return new Success(pageList);
    }

    /// <summary>
    /// 举报成功/信息处理
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    [HttpPost, Route("comment/accept")]
    public Result CommentAccept([FromBody] AcceptReq req)
    {
      if (req == null)
      {
        return new Failure("para is null");
      }

      var msg = req.ValidInfo();
      if (msg != string.Empty)
      {
        return new Failure(msg);
      }

      var result = SqlHelper.ExecuteNonQuery(SqlHelper.GetConnString(), CommandType.StoredProcedure,
        "accusation_edit_item_status",
        new SqlParameter("@id", req.Id),
        new SqlParameter("@status", 2),
        new SqlParameter("@userId", req.UserId));

      if (result > 0)
      {
        var param = new EditStatusReq()
        {
          Id = req.RelationId,
          Status = (int) CommentStatusTypes.UNUSEFUL
        };

        //p:发送举报成功通知

        var executeNonQuery = SqlHelper.ExecuteNonQuery(SqlHelper.GetConnString(), CommandType.StoredProcedure,
          StoreProcedureConst.EditCommentStatus,
          EntitySqlHelper.GetParameters(param));

        if (executeNonQuery > 0)
        {
          //发送通知 
          //var msg = $"尊敬的用户,您的评论xxx,由于存在...,已被删除.";
          return new Success(ResultMsgConst.OperationSuccess);
        }
        else
        {
          return new Failure(ResultMsgConst.OperationFailure);
        }
      }
      else
      {
        return new Failure(ResultMsgConst.OperationFailure);
      }
    }


    /// <summary>
    /// 删除评论
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    [HttpPost, Route("comment/remove")]
    [Permission("UserCenter", "accusation_acceptComment")]
    public Result RemoveComment([FromBody] EditStatusReq req)
    {

      req.Status = (int) CommentStatusTypes.UNUSEFUL;

      var executeNonQuery = SqlHelper.ExecuteNonQuery(SqlHelper.GetConnString(), CommandType.StoredProcedure,
        StoreProcedureConst.EditCommentStatus,
        EntitySqlHelper.GetParameters(req));

      if (executeNonQuery > 0)
      {
        //发送通知 
        //var msg = $"尊敬的用户,您的评论xxx,由于存在...,已被删除.";
        return EditStatusByRelation(new AccusationEditStatusByRelationParam()
        {
          RelationId = req.Id,
          Status = 2,
          TargetType = AccusationTargetTypes.Comment
        });
      }
      else
      {
        return new Failure(ResultMsgConst.OperationFailure);
      }
    }

    /// <summary>
    /// 清空此评论相关的举报
    /// </summary>
    /// <param name="commentId">评论编号</param>
    /// <returns></returns>
    [HttpDelete, Route("comment/clear")]
    [Permission("UserCenter", "accusation_clearComment")]
    public Result ClearComment([FromUri] int commentId)
    {
      return EditStatusByRelation(new AccusationEditStatusByRelationParam()
      {
        RelationId = commentId,
        Status = 0,
        TargetType = AccusationTargetTypes.Comment
      });
    }

    #endregion

    /// <summary>
    /// 删除举报/举报无效
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userId"></param>
    /// <param name="msg"></param>
    /// <returns></returns>
    [HttpDelete, Route(nameof(Remove))]
    public Result Remove([FromUri] int id, int userId, string msg = null)
    {
//      if (!string.IsNullOrWhiteSpace(msg))
//      {
//        //发送信息通知用户投诉无效
//      }

      var result = SqlHelper.ExecuteNonQuery(SqlHelper.GetConnString(), CommandType.StoredProcedure,
        "accusation_edit_item_status",
        new SqlParameter("@id", id),
        new SqlParameter("@status", "0"), //debug:此处直接传0会导致传递null
        new SqlParameter("@userId", userId));

      if (result > 0)
      {
        return new Success(ResultMsgConst.OperationSuccess);
      }
      else
      {
        return new Failure(ResultMsgConst.OperationFailure);
      }

      #region ***********************数据模型******************************

      #endregion
    }

    #region 内部方法

    /// <summary>
    /// 根据关联关系修改投诉状态
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    [NonAction]
    public Result EditStatusByRelation(AccusationEditStatusByRelationParam param)
    {

      var result = SqlHelper.ExecuteNonQuery(SqlHelper.GetConnString(), CommandType.StoredProcedure,
        "accusation_edit_item_status_by_relation",
        EntitySqlHelper.GetParameters(param));

      if (result > 0)
      {
        return new Success(ResultMsgConst.OperationSuccess);
      }
      else
      {
        return new Failure(ResultMsgConst.OperationFailure);
      }
      
    }

    #endregion

    #region ***********************数据模型******************************

    #region param
    
    /// <summary>
    /// 根据关联信息修改状态
    /// </summary>
    public class AccusationEditStatusByRelationParam
    {
      
      /// <summary>
      /// 检举类型
      /// </summary>
      public AccusationTargetTypes TargetType { get; set; }

      /// <summary>
      /// 举报状态 0-举报无效 1-待处理 2-举报成功
      /// </summary>
      public int Status { get; set; } 
      /// <summary>
      /// 相关信息编号
      /// </summary>
      public int RelationId { get; set; }
    }

    #endregion

    #region req

    public class AcceptReq
    {
      /// <summary>
      /// 举报编号
      /// </summary>
      public int Id { get; set; }
      /// <summary>
      /// 举报用户
      /// </summary>
      public int UserId { get; set; }
      /// <summary>
      /// 举报信息编号
      /// </summary>
      public int RelationId { get; set; }
      /// <summary>
      /// 被举报用户
      /// </summary>
      public int RelationUserId { get; set; }
      public string Msg { get; set; }

      public string ValidInfo()
      {
        if (Id <= 0 || UserId <= 0 || RelationId <= 0 || RelationUserId <= 0)
        {
          return "参数异常！";
        }

        return string.Empty;
      }
    }

    #endregion

    #region dto

    /// <summary>
    /// 检举/举报信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [CameCasePropertyNames]
    public class AccusationGroupDto<T>
    {
      /// <summary>
      /// 被举报总次数
      /// </summary>
      public int AccusationCount { get; set; }

      /// <summary>
      /// 举报理由
      /// </summary>
      [JsonConverter(typeof(SplitConvert), '\\')]
      public string Reason { get; set; }

      /// <summary>
      /// 被举报人的名称
      /// </summary>
      public string TargetUserName { get; set; }

      /// <summary>
      /// 举报状态 0-举报无效 1-待处理 2-举报成功
      /// </summary>
      public int Status { get; set; }

      /// <summary>
      /// 相关信息
      /// </summary>
      public T RelationInfo { get; set; }

    }

    /// <summary>
    /// 作品信息
    /// </summary>
    [CameCasePropertyNames]
    public class WorkDto
    {
      /// <summary>
      /// 作品编号
      /// </summary>
      public int Id { get; set; }

      /// <summary>
      /// 作者用户编号
      /// </summary>
      public int UserId { get; set; }

      /// <summary>
      /// 作品图
      /// </summary>
      [JsonConverter(typeof(SplitConvert))]
      public string Picture { get; set; }

      /// <summary>
      /// 作品标题
      /// </summary>
      public string Title { get; set; }

      /// <summary>
      /// 作品状态 1-有效 其他-无效
      /// </summary>
      public int Status { get; set; }
    }

    /// <summary>
    /// 评论信息
    /// </summary>
    [CameCasePropertyNames]
    public class CommentDto
    {
      /// <summary>
      /// 评论编号
      /// </summary>
      public int Id { get; set; }

      /// <summary>
      /// 评论的用户编号
      /// </summary>
      public int UserId { get; set; }

      /// <summary>
      /// 评论类型：1-作品；2-课程
      /// </summary>
      public int Type { get; set; }

      /// <summary>
      /// 	评论内容
      /// </summary>
      public string Content { get; set; }

      /// <summary>
      /// 评论内容类型 0-文字评论 1-语音评论
      /// </summary>
      public int CommentType { get; set; }

      /// <summary>
      /// 评论状态 1-有效 其他-无效
      /// </summary>
      public int Status { get; set; }

    }

    #endregion

    #endregion
  }
}