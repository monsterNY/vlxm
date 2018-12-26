using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DapperContext.Const;
using Model.Article.Entity;
using Model.Article.Tools;
using Model.Common.ConfigModels;
using Model.Common.Models;

namespace DapperContext
{
  public class DapperTools
  {
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="pageNo"></param>
    /// <param name="pageSize"></param>
    /// <param name="conn"></param>
    /// <param name="whereSql"></param>
    /// <param name="loadNow"></param>
    /// <returns></returns>
    public static async Task<PageModel<IEnumerable<T>>> GetPageList<T>(int pageNo, int pageSize, IDbConnection conn,
      string whereSql, bool loadNow = true) where T : BaseModel
    {
      var count = await conn.QueryFirstAsync<int>($@"
{SqlCharConst.SELECT} {SqlCharConst.COUNT}(1) 
{SqlCharConst.FROM} {EntityTools.GetTableName<ArticleInfo>()}
{whereSql}
");

      PageModel<IEnumerable<T>> resultPage = new PageModel<IEnumerable<T>>()
      {
        Count = count,
        PageNo = pageNo,
        PageSize = pageSize
      };

      if ((pageNo - 1) * pageSize <= count)
      {
        IEnumerable<T> enumerable = await conn.QueryAsync<T>($@"
{SqlCharConst.SELECT} {string.Join(",", EntityTools.GetFields<T>())}
{SqlCharConst.FROM} {EntityTools.GetTableName<T>()}
{whereSql}

{SqlCharConst.ORDERBY} {EntityTools.GetField<T>(nameof(BaseModel.Id))} {SqlCharConst.DESC}

{SqlCharConst.LIMIT} {(pageNo - 1) * pageSize},{pageSize}

");

        resultPage.Result = enumerable;

        if (loadNow)
        {
          resultPage.Result = resultPage.Result.ToList();
        }
      }

      return resultPage;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="conn"></param>
    /// <param name="tableName"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static async Task<int> CreateItem<T>(IDbConnection conn, string tableName, T param)
    {
      var result = await conn.ExecuteAsync($@"
{SqlCharConst.INSERT} {SqlCharConst.INTO} {tableName}
(
  {string.Join(",", EntityTools.GetFields<T>())}
)
{SqlCharConst.VALUES} 
(
  {string.Join(",", EntityTools.GetFields<T>("@"))}
)
", param);

      return result;
    }
  }
}