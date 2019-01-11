using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Manage.Assist.Entity;
using Api.Manage.Assist.Extension;
using Api.Manage.Assist.Req;
using Api.Manage.CusInterface;
using DapperContext;
using Microsoft.AspNetCore.Http;
using Model.Common.ConfigModels;
using Model.Vlxm.Entity;
using Model.Vlxm.Menu;
using Model.Vlxm.Tools;

namespace Api.Manage.CusInherit.Article.Action
{
  public class SelectActionAuthService:IAuthDeal
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

        var count = await DapperTools.SelectSingle<int?>(conn, EntityTools.GetTableName<ArticleOptInfo>(),
        new List<string>()
        {
          $"{EntityTools.GetField<ArticleOptInfo>(nameof(ArticleOptInfo.OptionType))} = {(int) opt}",
          $"{EntityTools.GetField<ArticleOptInfo>(nameof(ArticleOptInfo.RelationKey))} = {req.ArticleId}",
          $"{EntityTools.GetField<ArticleOptInfo>(nameof(ArticleOptInfo.ActionUser))} = {userId}",
          $"{nameof(BaseModel.ValidFlag)}={(int)ValidFlagMenu.UseFul}"
        }, EntityTools.GetField<ArticleOptInfo>(nameof(ArticleOptInfo.Count)));

        count = count ?? 0;

        return ResultModel.GetSuccessModel(count);

      }
      else
      {
        return ResultModel.GetDealErrorModel($"操作<{req.ActionKey}>不存在！");
      }

    }
  }
}
