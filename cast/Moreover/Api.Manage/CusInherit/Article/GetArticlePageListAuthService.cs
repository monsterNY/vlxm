using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Manage.Assist.Dto;
using Api.Manage.Assist.Entity;
using Api.Manage.Assist.Extension;
using Api.Manage.CusInterface;
using DapperContext;
using Microsoft.AspNetCore.Http;
using Model.Common.ConfigModels;
using Model.Common.Models;
using Model.Vlxm.Entity;
using Model.Vlxm.Tools;

namespace Api.Manage.CusInherit.Article
{
  public class GetArticlePageListAuthService : IAuthDeal
  {

    public async Task<ResultModel> Run(AcceptParam acceptParam, AppSetting appSetting, HttpContext context, long userId)
    {
      //解析参数
      var pageModel = acceptParam.AnalyzeParam<PageModel<ArticlePageFilterDto>>();

      if (pageModel == null)
      {
        return ResultModel.GetNullErrorModel(string.Empty);
      }

      //动态sql
      var whereArr = new List<string>()
      {
        $"{EntityTools.GetField<ArticleInfo>(nameof(ArticleInfo.UserId))} = {userId}"
      };

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
      
    }
    
  }
}