using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Api.Manage.Assist.Entity;
using Api.Manage.Assist.Extension;
using Api.Manage.CusInterface;
using Dapper;
using DapperContext.Const;
using Microsoft.AspNetCore.Http;
using Model.Article.Entity;
using Model.Article.Tools;
using Model.Common.ConfigModels;
using MySql.Data.MySqlClient;

namespace Api.Manage.CusInherit
{
  public class GetArticleListService : IDeal
  {
    public async Task<ResultModel> Run(AcceptParam acceptParam, AppSetting appSetting, HttpContext context)
    {
      var mysqlConn = appSetting.GetMysqlConn();

      var dbConnection = context.GetConnection(mysqlConn.FlagKey, mysqlConn.ConnStr);

      IEnumerable<ArticleInfo> articleInfos = await dbConnection.QueryAsync<ArticleInfo>($@"
{SqlCharConst.SELECT} {string.Join(",", EntityTools.GetFields<ArticleInfo>())}
{SqlCharConst.FROM} {EntityTools.GetTableName<ArticleInfo>()}
{SqlCharConst.WHERE} {SqlCharConst.DefaultWhere}
");

      return ResultModel.GetSuccessModel(string.Empty, articleInfos.ToList());
    }
  }
}