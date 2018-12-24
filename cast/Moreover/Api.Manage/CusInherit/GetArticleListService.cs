using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Api.Manage.Assist.Entity;
using Api.Manage.CusInterface;
using Dapper;
using Model.Article.Entity;
using Model.Common.ConfigModels;
using MySql.Data.MySqlClient;

namespace Api.Manage.CusInherit
{
  public class GetArticleListService : IDeal
  {
    public async Task<object> Run(AcceptParam acceptParam, AppSetting appSetting)
    {
      using (IDbConnection conn = new MySqlConnection(appSetting.ConnectionString["Mysql"]))
      {
        IEnumerable<ArticleInfo> articleInfos = await conn.QueryAsync<ArticleInfo>("select * from article_info");
        return articleInfos.ToList();
      }
    }
  }
}