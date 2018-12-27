using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Manage.Assist.Dto;
using Api.Manage.Assist.Entity;
using Api.Manage.Assist.Extension;
using Api.Manage.CusInterface;
using Dapper;
using DapperContext;
using DapperContext.Const;
using Microsoft.AspNetCore.Http;
using Model.Common.ConfigModels;
using Model.Common.Models;
using Model.Vlxm.Entity;
using Model.Vlxm.Tools;
using NLog;

namespace Api.Manage.CusInherit
{
  public class GetArticlePageListService : IDeal
  {

    public async Task<ResultModel> Run(AcceptParam acceptParam, AppSetting appSetting, HttpContext context)
    {
      //解析参数
      var pageModel = acceptParam.AnalyzeParam<PageModel<FilterDto>>();

      if (pageModel == null)
      {
        return await Task.Run(() => ResultModel.GetNullErrorModel(string.Empty));
      }

      //动态sql
      StringBuilder whereBuilder = new StringBuilder();

      if (pageModel.Result.ValidFlag != null && pageModel.Result.ValidFlag >= 0)
      {
        whereBuilder.Append($@"
{SqlCharConst.WHERE} {EntityTools.GetField<ArticleInfo>(nameof(ArticleInfo.ValidFlag))} = {pageModel.Result.ValidFlag}
");
      }

      //获取连接
      var mysqlConn = appSetting.GetMysqlConn();

      var dbConnection = context.GetConnection(mysqlConn.FlagKey, mysqlConn.ConnStr);

      //采用工具类分页查询
      var pageList = await DapperTools.GetPageList<ArticleInfo>(pageModel.PageNo, pageModel.PageSize, dbConnection,
        whereBuilder.ToString());

      //返回结果集
      return ResultModel.GetSuccessModel(string.Empty, pageList);

//      var count = await dbConnection.QueryFirstAsync<int>($@"
//{SqlCharConst.SELECT} {SqlCharConst.COUNT}(1) 
//{SqlCharConst.FROM} {EntityTools.GetTableName<ArticleInfo>()}
//{whereBuilder}
//");
//
//      PageModel<IEnumerable<ArticleInfo>> resultPage = new PageModel<IEnumerable<ArticleInfo>>()
//      {
//        Count = count,
//        PageNo = pageModel.PageNo,
//        PageSize = pageModel.PageSize
//      };
//
//      if ((pageModel.PageNo - 1) * pageModel.PageSize <= count)
//      {
//        IEnumerable<ArticleInfo> articleInfos = await dbConnection.QueryAsync<ArticleInfo>($@"
//{SqlCharConst.SELECT} {string.Join(",", EntityTools.GetFields<ArticleInfo>())}
//{SqlCharConst.FROM} {EntityTools.GetTableName<ArticleInfo>()}
//{whereBuilder}
//
//{SqlCharConst.ORDERBY} {EntityTools.GetField<ArticleInfo>(nameof(ArticleInfo.Id))} {SqlCharConst.DESC}
//
//{SqlCharConst.LIMIT} {(pageModel.PageNo - 1) * pageModel.PageSize},{pageModel.PageSize}
//
//");
//
//        resultPage.Result = articleInfos.ToList();
//      }
//
//      return ResultModel.GetSuccessModel(string.Empty, resultPage);
    }
  }
}