using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Manage.Assist.Dto;
using Api.Manage.Assist.Entity;
using DapperContext.Const;
using Model.Common.Models;
using Model.Vlxm.CusAttr;
using Model.Vlxm.Entity;
using Model.Vlxm.Menu;
using Model.Vlxm.Tools;

namespace Api.Manage.CusInherit.Comment
{
  public class GetArticleCommentPageListService : BaseGetCommentPageListService
  {
    public override IEnumerable<string> GetWhereEnumerable(AcceptParam acceptParam, out PageModel<object> page,
      out string msg)
    {
      var param = acceptParam.AnalyzeParam<PageModel<long>>();

      if (param == null || param.Result <= 0)
      {
        msg = "参数异常！";
      }

      msg = string.Empty;

      page = new PageModel<object>()
      {
        PageNo = param.PageNo,
        PageSize = param.PageSize,
      };

      return new[]
      {
        $"{EntityTools.GetField<CommentInfo>(nameof(CommentInfo.JoinKey))} = {param.Result}",
        $"{EntityTools.GetField<CommentInfo>(nameof(CommentInfo.CommentType))} = {(int)CommentTypeMenu.Article}",
//        $"{EntityTools.GetField<CommentInfo>(nameof(CommentInfo.ReplyId))} = {0}",//只查评论 不差回复评论
        $"{EntityTools.GetField<CommentInfo>(nameof(CommentInfo.ValidFlag))} = {(int)ValidFlagMenu.UseFul}",
      };
    }

    public override IEnumerable<string> GetFieldEnumerable(TableMapperAttribute tableMapper)
    {

      var list = EntityTools.GetFields<CommentInfo>().ToList();

//      list.Add($@"
//(
//  {SqlCharConst.SELECT} {SqlCharConst.COUNT}(0)
//  {SqlCharConst.FROM} {EntityTools.GetTableName<CommentInfo>()}
//  {SqlCharConst.WHERE} {EntityTools.GetField<CommentInfo>(nameof(CommentInfo.ReplyId))} = {tableMapper.Alias}.{nameof(BaseModel.Id)}
//  {SqlCharConst.AND} {EntityTools.GetField<CommentInfo>(nameof(CommentInfo.ValidFlag))} = {(int)ValidFlagMenu.UseFul}
//  {SqlCharConst.AND} {EntityTools.GetField<CommentInfo>(nameof(CommentInfo.CommentType))} = {(int)CommentTypeMenu.Article}
//)
// AS {EntityTools.GetField<CommentDto>(nameof(CommentDto.ReplyCount))}
//");

      list.Add($@"
(
  SELECT displayName FROM user_info
  WHERE id = (
	  SELECT actionUser FROM comment_info
	  WHERE id = {tableMapper.Alias}.replyId
  )
) AS {nameof(CommentDto.ReplyUserName)}
");

      return list;

    }

    public override IEnumerable<string> GetOrderEnumerable(TableMapperAttribute tableMapper)
    {
      return new[]
      {
        SqlCharConst.DefaultOrder
      };
    }
  }
}