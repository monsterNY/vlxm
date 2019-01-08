using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Manage.Assist.Entity;
using Api.Manage.Assist.Extension;
using Api.Manage.Assist.Req;
using Api.Manage.CusInterface;
using Dapper;
using DapperContext;
using Microsoft.AspNetCore.Http;
using Model.Common.ConfigModels;
using Model.Vlxm.Entity;
using Model.Vlxm.Tools;

namespace Api.Manage.CusInherit.Article
{
  public class AddPageViewService:IDeal
  {
    public async Task<ResultModel> Run(AcceptParam acceptParam, AppSetting appSetting, HttpContext context)
    {
      var req = acceptParam.AnalyzeParam<KeyReq<long>>();

      if (req == null || req.Key <= 0)
      {
        return ResultModel.GetParamErrorModel();
      }

      var conn = appSetting.GetMysqlConn(context);

      var pageViewField = EntityTools.GetField<ArticleInfo>(nameof(ArticleInfo.PageView));

      var result = await DapperTools.Edit(conn, EntityTools.GetTableName<ArticleInfo>(), new[]
      {
        $"{nameof(BaseModel.Id)} = {req.Key}"
      },new []
      {
        $"{pageViewField} = {pageViewField} + 1"
      },null);

      return ResultModel.GetSuccessModel(result);

    }
  }
}
