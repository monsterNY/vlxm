using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Manage.Assist.Req;
using Api.Manage.Assist.Entity;
using Api.Manage.Assist.Extension;
using Api.Manage.CusInterface;
using Dapper;
using DapperContext;
using DapperContext.Const;
using Microsoft.AspNetCore.Http;
using Model.Common.ConfigModels;
using Model.Vlxm.Entity;
using Model.Vlxm.Menu;
using Model.Vlxm.Tools;

namespace Api.Manage.CusInherit.Article
{
  public class RemoveArticleAuthService:IAuthDeal
  {
    public async Task<ResultModel> Run(AcceptParam acceptParam, AppSetting appSetting, HttpContext context, long userId)
    {
      var req = acceptParam.AnalyzeParam<KeyReq<long>>();

      if (req == null || req.Key <= 0)
      {
        return ResultModel.GetParamErrorModel("参数异常");
      }

      var dbConnection = appSetting.GetMysqlConn(context);

      var result = await dbConnection.ExecuteAsync($@"
{SqlCharConst.UPDATE} {EntityTools.GetTableName<ArticleInfo>()}
{SqlCharConst.SET} {EntityTools.GetField<BaseModel>(nameof(BaseModel.ValidFlag))} = {((int) ValidFlagMenu.UseFul)}
{SqlCharConst.WHERE} {EntityTools.GetField<BaseModel>(nameof(BaseModel.Id))} = {req.Key} 
{SqlCharConst.AND} {EntityTools.GetField<ArticleInfo>(nameof(ArticleInfo.UserId))} = {userId} 
");

      return ResultModel.GetSuccessModel(result);

    }
  }
}
