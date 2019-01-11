using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Api.Manage.Assist.Dto;
using Api.Manage.Assist.Req;
using Api.Manage.Assist.Entity;
using Api.Manage.Assist.Extension;
using Api.Manage.CusInterface;
using DapperContext;
using DapperContext.Const;
using Microsoft.AspNetCore.Http;
using Model.Common.ConfigModels;
using Model.Common.Models;
using Model.Vlxm.CusAttr;
using Model.Vlxm.Entity;
using Model.Vlxm.Menu;
using Model.Vlxm.Tools;

namespace Api.Manage.CusInherit.Article
{
  public class GetArticlePageListAuthService : IAuthDeal
  {
    public async Task<ResultModel> Run(AcceptParam acceptParam, AppSetting appSetting, HttpContext context, long userId)
    {
      //解析参数
      var pageModel = acceptParam.AnalyzeParam<PageModel<ArticlePageFilterAuthReq>>();

      if (pageModel == null)
      {
        return ResultModel.GetNullErrorModel(string.Empty);
      }

      //动态sql
      var whereArr = new List<string>()
      {
        $"{EntityTools.GetField<ArticleInfo>(nameof(ArticleInfo.ValidFlag))} = {(int) ValidFlagMenu.UseFul}"
      };

      var tableMapper = typeof(ArticleInfo).GetCustomAttribute<TableMapperAttribute>();

      var fields = EntityTools.GetFields<ArticleInfo>().ToList();

      fields.Add($@"
(
  {SqlCharConst.SELECT} {SqlCharConst.COUNT}(0)
	{SqlCharConst.FROM} {EntityTools.GetTableName<CommentInfo>()}
	{SqlCharConst.WHERE} {EntityTools.GetField<CommentInfo>(nameof(CommentInfo.CommentType))} = {(int) CommentTypeMenu.Article}
	{SqlCharConst.AND} {EntityTools.GetField<CommentInfo>(nameof(CommentInfo.JoinKey))} = {tableMapper.Alias}.{nameof(BaseModel.Id)}
  {SqlCharConst.AND} {nameof(BaseModel.ValidFlag)} = {(int) ValidFlagMenu.UseFul}
) {SqlCharConst.AS} {EntityTools.GetField<ArticleInfoDto>(nameof(ArticleInfoDto.CommentCount))}
"); //评论数量

      fields.Add($@"
(
  {SqlCharConst.SELECT} {SqlCharConst.COUNT}(0)
	{SqlCharConst.FROM} {EntityTools.GetTableName<ArticleOptInfo>()}
	{SqlCharConst.WHERE} {EntityTools.GetField<ArticleOptInfo>(nameof(ArticleOptInfo.OptionType))} = {(int) ArticleOptMenu.Like}
	{SqlCharConst.AND} {EntityTools.GetField<ArticleOptInfo>(nameof(ArticleOptInfo.RelationKey))} = {tableMapper.Alias}.{nameof(BaseModel.Id)}
  {SqlCharConst.AND} {nameof(BaseModel.ValidFlag)} = {(int) ValidFlagMenu.UseFul}
) {SqlCharConst.AS} {EntityTools.GetField<ArticleInfoDto>(nameof(ArticleInfoDto.LikeCount))}
"); //点赞数量

      if (pageModel.Result != null && pageModel.Result.ArticleType > 0)
      {
        if (pageModel.Result.ArticleType > 0)
        {
          whereArr.Add($@"
{EntityTools.GetField<ArticleInfo>(nameof(ArticleInfo.ArticleType))} = {pageModel.Result.ArticleType}
");
        }

        if (pageModel.Result.FilterType == 1)
        {
          whereArr.Add($@"
{SqlCharConst.EXISTS} (

	{SqlCharConst.SELECT} * {SqlCharConst.FROM} {EntityTools.GetTableName<ArticleOptInfo>()}
	{SqlCharConst.WHERE} {EntityTools.GetField<ArticleOptInfo>(nameof(ArticleOptInfo.OptionType))} = {(int) ArticleOptMenu.Collect}
	{SqlCharConst.AND} {nameof(BaseModel.ValidFlag)} = {(int) ValidFlagMenu.UseFul}
	{SqlCharConst.AND} {EntityTools.GetField<ArticleOptInfo>(nameof(ArticleOptInfo.ActionUser))} = {userId}
	{SqlCharConst.AND} {EntityTools.GetField<ArticleOptInfo>(nameof(ArticleOptInfo.RelationKey))} = {tableMapper.Alias}.{nameof(BaseModel.Id)}
	
)
");
        }
        else if (pageModel.Result.FilterType == 2)
        {
          whereArr.Add($"{EntityTools.GetField<ArticleInfo>(nameof(ArticleInfo.UserId))} = {userId}");
        }
      }


      //获取连接
      var mysqlConn = appSetting.GetMysqlConn();

      var dbConnection = context.GetConnection(mysqlConn.FlagKey, mysqlConn.ConnStr);

      //采用工具类分页查询
      var pageList = await DapperTools.GetPageList<ArticleInfoDto>(pageModel.PageNo, pageModel.PageSize, dbConnection,
        tableMapper.TableName,
        whereArr, fields, new[]
        {
          SqlCharConst.DefaultOrder
        }, tableMapper.Alias, pageModel.Result);

      //返回结果集
      return ResultModel.GetSuccessModel(string.Empty, pageList);
    }
  }
}