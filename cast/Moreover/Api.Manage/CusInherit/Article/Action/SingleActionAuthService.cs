using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Manage.Assist.Entity;
using Api.Manage.Assist.Extension;
using Api.Manage.Assist.Param;
using Api.Manage.Assist.Req;
using Api.Manage.CusInterface;
using Dapper;
using DapperContext;
using Microsoft.AspNetCore.Http;
using Model.Common.ConfigModels;
using Model.Vlxm.Entity;
using Model.Vlxm.Menu;
using Model.Vlxm.Tools;

namespace Api.Manage.CusInherit.Article.Action
{
  /// <summary>
  /// 文章单一操作
  /// </summary>
  public class SingleActionAuthService : IAuthDeal
  {
    public async Task<ResultModel> Run(AcceptParam acceptParam, AppSetting appSetting, HttpContext context, long userId)
    {
      var req = acceptParam.AnalyzeParam<SingleActionArticleReq>();

      if (req == null)
      {
        return ResultModel.GetNullErrorModel();
      }

      string msg;

      if ((msg = req.ValidInfo()) != string.Empty)
      {
        return ResultModel.GetParamErrorModel(msg);
      }

      if (Enum.TryParse(req.ActionKey, true, out ArticleOptMenu opt))
      {
        var conn = appSetting.GetMysqlConn(context);

        var info = await DapperTools.GetItem<ArticleOptInfo>(conn, EntityTools.GetTableName<ArticleOptInfo>(),
          new List<string>()
          {
            $"{EntityTools.GetField<ArticleOptInfo>(nameof(ArticleOptInfo.OptionType))} = {(int) opt}",
            $"{EntityTools.GetField<ArticleOptInfo>(nameof(ArticleOptInfo.RelationKey))} = {req.ArticleId}",
            $"{EntityTools.GetField<ArticleOptInfo>(nameof(ArticleOptInfo.ActionUser))} = {userId}",
          }, new[]
          {
            nameof(BaseModel.Id),
            nameof(BaseModel.ValidFlag)
          }, null);

        //        var validFlag = await DapperTools.SelectSingle<int?>(conn, EntityTools.GetTableName<ArticleOptInfo>(),
        //          new List<string>()
        //          {
        //            $"{nameof(BaseModel.Id)} = {req.ArticleId}",
        //            $"{EntityTools.GetField<ArticleOptInfo>(nameof(ArticleOptInfo.OptionType))} = {(int) opt}",
        //            $"{EntityTools.GetField<ArticleOptInfo>(nameof(ArticleOptInfo.RelationKey))} = {req.ArticleId}",
        //          }, nameof(BaseModel.ValidFlag));

        if (info != null)
        {
          EditValidFlagParam param = new EditValidFlagParam()
          {
            Id = info.Id,
            UpdateTime = DateTime.Now
          };
          if (info.ValidFlag == (int) ValidFlagMenu.UnUseFul)
          {
            param.ValidFlag = ValidFlagMenu.UseFul;
          }
          else
          {
            param.ValidFlag = ValidFlagMenu.UnUseFul;
          }

          var result = await DapperTools.Edit(conn, EntityTools.GetTableName<ArticleOptInfo>(), new[]
          {
            $"{nameof(BaseModel.Id)} = {info.Id}",
            $"{EntityTools.GetField<ArticleOptInfo>(nameof(ArticleOptInfo.ActionUser))} = {userId}",
          }, param);

          return ResultModel.GetSuccessModel(null,$"修改成功-{result}");

        }
        else
        {
          var param = new CreateArticleOptInfoParam()
          {
            ActionKey = opt.ToString(),
            Count = 1,
            OptionType = (int) opt,
            RelationKey = req.ArticleId,
            ActionUser = userId
          };

          var result = await DapperTools.CreateItem(conn, EntityTools.GetTableName<ArticleOptInfo>(), param);

          return ResultModel.GetSuccessModel(null, $"添加成功-{result}");

        }

//        return ResultModel.GetSuccessModel(validFlag);

//        if (await DapperTools.IsExists(conn, EntityTools.GetTableName<ArticleOptInfo>(), new List<string>()
//        {
//          $"{nameof(BaseModel.Id)} = {req.ArticleId}",
//          $"{EntityTools.GetField<ArticleOptInfo>(nameof(ArticleOptInfo.OptionType))} = {(int)opt}",
//          $"{EntityTools.GetField<ArticleOptInfo>(nameof(ArticleOptInfo.RelationKey))} = {req.ArticleId}",
//        }))
//        {
//          return ResultModel.GetDealErrorModel("请勿重复操作！");
//        }
//        else
//        {
//
//          var param = new CreateArticleOptInfoParam()
//          {
//            ActionKey = opt.ToString(),
//            Count = 1,
//            OptionType = (int) opt,
//            RelationKey = req.ArticleId
//          };
//
//          var result = await DapperTools.CreateItem(conn, EntityTools.GetTableName<ArticleOptInfo>(), param);
//
//          return ResultModel.GetSuccessModel(result);
//
//        }
      }
      else
      {
        return ResultModel.GetDealErrorModel($"操作<{req.ActionKey}>不存在！");
      }
    }
  }
}