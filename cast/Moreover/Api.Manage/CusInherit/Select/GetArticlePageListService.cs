using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Api.Manage.Assist.Dto;
using Api.Manage.Assist.Entity;
using Api.Manage.Assist.Extension;
using Api.Manage.CusInterface;
using DapperContext;
using DapperContext.Const;
using Microsoft.AspNetCore.Http;
using Model.Common.ConfigModels;
using Model.Common.Models;
using Model.Vlxm.Entity;
using Model.Vlxm.Tools;

namespace Api.Manage.CusInherit.Select
{
  public class GetArticlePageListService : IDeal
  {

    public async Task<ResultModel> Run(AcceptParam acceptParam, AppSetting appSetting, HttpContext context)
    {
      //解析参数
      var pageModel = acceptParam.AnalyzeParam<PageModel<ArticlePageFilterDto>>();

      if (pageModel == null)
      {
        return ResultModel.GetNullErrorModel(string.Empty);
      }

      //动态sql
      var whereArr = new List<string>();

      if (pageModel.Result.ValidFlag != null && pageModel.Result.ValidFlag >= 0)
      {
        whereArr.Add($@"
{EntityTools.GetField<ArticleInfo>(nameof(ArticleInfo.ValidFlag))} = {pageModel.Result.ValidFlag}
");
      }

      if (pageModel.Result.ArticleType != null && pageModel.Result.ArticleType > 0)
      {
        whereArr.Add($@"
{EntityTools.GetField<ArticleInfo>(nameof(ArticleInfo.ArticleType))} = {pageModel.Result.ArticleType}
");
      }

      if (pageModel.Result.FilterType == 1)
      {
        whereArr.Add($@"
{EntityTools.GetField<ArticleInfo>(nameof(ArticleInfo.UserId))} = {acceptParam.GetUserId()}
");
      }

      //获取连接
      var mysqlConn = appSetting.GetMysqlConn();

      var dbConnection = context.GetConnection(mysqlConn.FlagKey, mysqlConn.ConnStr);

      //采用工具类分页查询
      var pageList = await DapperTools.GetPageList<ArticleInfo>(pageModel.PageNo, pageModel.PageSize, dbConnection,
        whereArr);

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