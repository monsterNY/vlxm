using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Manage.Assist.Dto;
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

namespace Api.Manage.CusInherit.Article
{
  public class GetArticleSingleService:IDeal
  {
    public async Task<ResultModel> Run(AcceptParam acceptParam, AppSetting appSetting, HttpContext context)
    {
      var req = acceptParam.AnalyzeParam<KeyReq<long>>();

      if (req == null || req.Key <= 0)
      {
        return ResultModel.GetNullErrorModel();
      }

      var conn = appSetting.GetMysqlConn(context);

      var item = await DapperTools.GetItem<ArticleSingleDto>(conn,EntityTools.GetTableName<ArticleInfo>(),new []
      {
        $"{nameof(BaseModel.Id)} = {req.Key}",
        $"{nameof(BaseModel.ValidFlag)} = {(int)ValidFlagMenu.UseFul}",
      });

      return ResultModel.GetSuccessModel(item);

    }
  }
}
