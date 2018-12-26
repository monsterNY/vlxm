using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Manage.Assist.Dto;
using Api.Manage.Assist.Entity;
using Api.Manage.Assist.Extension;
using Api.Manage.Assist.Param;
using Api.Manage.CusInterface;
using Dapper;
using DapperContext;
using DapperContext.Const;
using Microsoft.AspNetCore.Http;
using Model.Article.Entity;
using Model.Article.Tools;
using Model.Common.ConfigModels;
using NLog;

namespace Api.Manage.CusInherit.Create
{
  public class CreateArticleService : IDeal
  {
    protected Logger logger = LogManager.GetLogger(nameof(CreateArticleService));

    public async Task<ResultModel> Run(AcceptParam acceptParam, AppSetting appSetting, HttpContext context)
    {
      var mysqlConn = appSetting.GetMysqlConn();

      var dbConnection = context.GetConnection(mysqlConn.FlagKey, mysqlConn.ConnStr);

      var createArticleDto = acceptParam.AnalyzeParam<CreateArticleDto>();

      if (createArticleDto == null)
      {
        return await Task.Run(() => ResultModel.GetNullErrorModel(String.Empty));
      }

      string msg;

      if ((msg = createArticleDto.ValidInfo()) != string.Empty)
      {
        return await Task.Run(() => ResultModel.GetNullErrorModel(string.Empty, msg));
      }

      var createArticleParam = (CreateArticleParam) createArticleDto;

      var result =
        await DapperTools.CreateItem(dbConnection, EntityTools.GetTableName<ArticleInfo>(), createArticleParam);

      return ResultModel.GetSuccessModel(string.Empty, result);

//      logger.Info($@"
//{SqlCharConst.INSERT} {SqlCharConst.INTO} {EntityTools.GetTableName<ArticleInfo>()}
//(
//  {string.Join(",", EntityTools.GetFields<CreateArticleParam>())}
//)
//{SqlCharConst.VALUES} 
//(
//  {string.Join(",", EntityTools.GetFields<CreateArticleParam>("@"))}
//)
//");
//
//      var result = await dbConnection.ExecuteAsync($@"
//{SqlCharConst.INSERT} {SqlCharConst.INTO} {EntityTools.GetTableName<ArticleInfo>()}
//(
//  {string.Join(",", EntityTools.GetFields<CreateArticleParam>())}
//)
//{SqlCharConst.VALUES} 
//(
//  {string.Join(",", EntityTools.GetFields<CreateArticleParam>("@"))}
//)
//", createArticleParam);
//
//      return ResultModel.GetSuccessModel(string.Empty, result);
    }
  }
}