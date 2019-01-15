using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DapperContext.Const;
using Model.Common.ConfigModels;
using Model.Common.Models;
using Model.Vlxm.Entity;
using Model.Vlxm.Tools;
using NLog;

namespace DapperContext
{
  public class DapperTools
  {
    public static ILogger Logger = LogManager.GetCurrentClassLogger();

    #region common method

    public static string GetWhereSql(IEnumerable<string> whereEnumerable)
    {
      var whereSql = string.Empty;
      if (whereEnumerable != null && whereEnumerable.Any())
        whereSql = $"{SqlCharConst.WHERE} {string.Join($"\n{SqlCharConst.AND} ", whereEnumerable)}";
      return whereSql;
    }

    #endregion

    #region Update

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="conn"></param>
    /// <param name="tableName"></param>
    /// <param name="whereList"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static async Task<int> Edit(IDbConnection conn, string tableName,
      IEnumerable<string> whereList, object param)
    {
      var whereSql = GetWhereSql(whereList);

      return await Edit(conn, tableName, whereSql, param);
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="conn"></param>
    /// <param name="tableName"></param>
    /// <param name="whereSql"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static async Task<int> Edit(IDbConnection conn, string tableName,
      string whereSql, object param)
    {
      var sql = $@"
{SqlCharConst.UPDATE} {tableName}
{SqlCharConst.SET} {string.Join(",", param.GetType().GetProperties().Select(u => $"{u.Name} = @{u.Name}"))}
{whereSql}
";

      Logger.Debug(sql);

      var result = await conn.ExecuteAsync(sql, param);

      return result;
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="conn"></param>
    /// <param name="tableName"></param>
    /// <param name="setEnumerable"></param>
    /// <param name="param"></param>
    /// <param name="whereEnumerable"></param>
    /// <returns></returns>
    public static async Task<int> Edit(IDbConnection conn, string tableName,
      IEnumerable<string> whereEnumerable, IEnumerable<string> setEnumerable, object param = null)
    {
      var whereSql = GetWhereSql(whereEnumerable);

      var sql = $@"
{SqlCharConst.UPDATE} {tableName}
{SqlCharConst.SET} {string.Join(",", setEnumerable)}
{whereSql}
";

      Logger.Debug(sql);

      var result = await conn.ExecuteAsync(sql, param);

      return result;
    }

    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <param name="conn"></param>
    /// <param name="tableName"></param>
    /// <param name="whereArr"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static async Task<bool> IsExists(IDbConnection conn, string tableName,
      IEnumerable<string> whereArr, object param = null)
    {
      var whereSql = GetWhereSql(whereArr);

      var sql = $@"{SqlCharConst.SELECT} {SqlCharConst.EXISTS} (
{SqlCharConst.SELECT} 0 
{SqlCharConst.FROM} {tableName}
{whereSql}
)";

      Logger.Debug($"{nameof(GetItem)}:{sql}");

      var isExists = await conn.ExecuteScalarAsync<bool>(sql, param);

      return isExists;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="conn"></param>
    /// <param name="tableName"></param>
    /// <param name="whereArr"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static async Task<T> SelectSingle<T>(IDbConnection conn, string tableName,
      List<string> whereArr, string fieldName, object param = null)
    {
      var whereSql = GetWhereSql(whereArr);

      var sql = $@"
{SqlCharConst.SELECT} {fieldName} 
{SqlCharConst.FROM} {tableName}
{whereSql}
";

      Logger.Debug($"{nameof(GetItem)}:{sql}");

      var result = await conn.ExecuteScalarAsync<T>(sql, param);

      return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="conn"></param>
    /// <param name="tableName"></param>
    /// <param name="whereArr"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static async Task<T> GetItem<T>(IDbConnection conn, string tableName,
      List<string> whereArr, object param = null) where T : BaseModel
    {
      var whereSql = string.Empty;
      if (whereArr != null && whereArr.Count > 0)
        whereSql = $"{SqlCharConst.WHERE} {string.Join($"\n{SqlCharConst.AND} ", whereArr)}";

      return await GetItem<T>(conn, tableName, whereSql, param);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="conn"></param>
    /// <param name="tableName"></param>
    /// <param name="whereSql"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static async Task<T> GetItem<T>(IDbConnection conn, string tableName,
      string whereSql, object param = null) where T : BaseModel
    {
      var sql = $@"
{SqlCharConst.SELECT} {string.Join(",", EntityTools.GetFields<T>())}
{SqlCharConst.FROM} {EntityTools.GetTableName<T>()}
{whereSql}";

      Logger.Debug($"{nameof(GetItem)}:{sql}");

      return await conn.QueryFirstOrDefaultAsync<T>(sql, param);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="conn"></param>
    /// <param name="tableName"></param>
    /// <param name="whereList"></param>
    /// <param name="fieldList"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static async Task<T> GetItem<T>(IDbConnection conn, string tableName,
      IEnumerable<string> whereList, IEnumerable<string> fieldList, object param = null) where T : BaseModel
    {
      var whereSql = GetWhereSql(whereList);

      var sql = $@"
{SqlCharConst.SELECT} {string.Join(",", fieldList)}
{SqlCharConst.FROM} {EntityTools.GetTableName<T>()}
{whereSql}";

      Logger.Debug($"{nameof(GetItem)}:{sql}");

      return await conn.QueryFirstOrDefaultAsync<T>(sql, param);
    }

//    /// <summary>
//    /// 
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    /// <param name="pageNo"></param>
//    /// <param name="pageSize"></param>
//    /// <param name="conn"></param>
//    /// <param name="whereArr"></param>
//    /// <param name="loadNow"></param>
//    /// <returns></returns>
//    public static async Task<PageModel<IEnumerable<T>>> GetPageList<T>(int pageNo, int pageSize, IDbConnection conn,
//      List<string> whereArr, bool loadNow = true) where T : BaseModel
//    {
//      var whereSql = string.Empty;
//      if (whereArr != null && whereArr.Count > 0)
//        whereSql = $"{SqlCharConst.WHERE} {string.Join($"\n{SqlCharConst.AND}", whereArr)}";
//
//      return await GetPageList<T>(pageNo, pageSize, conn, whereSql, loadNow);
//    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="pageNo"></param>
    /// <param name="pageSize"></param>
    /// <param name="conn"></param>
    /// <param name="whereEnumerable"></param>
    /// <param name="param"></param>
    /// <param name="loadNow"></param>
    /// <returns></returns>
    public static Task<PageModel<IEnumerable<T>>> GetPageList<T>(int pageNo, int pageSize, IDbConnection conn,
      IEnumerable<string> whereEnumerable, object param = null, bool loadNow = true) where T : BaseModel
    {
      return GetPageList<T>(pageNo, pageSize, conn, EntityTools.GetTableName<T>(), whereEnumerable,
        EntityTools.GetFields<T>(), new[]
        {
          SqlCharConst.DefaultOrder
        }, nameof(T), param, loadNow);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="pageNo"></param>
    /// <param name="pageSize"></param>
    /// <param name="conn"></param>
    /// <param name="tableName"></param>
    /// <param name="whereEnumerable"></param>
    /// <param name="fieldEnumerable"></param>
    /// <param name="alias"></param>
    /// <param name="param"></param>
    /// <param name="loadNow"></param>
    /// <returns></returns>
    public static async Task<PageModel<IEnumerable<T>>> GetPageList<T>(int pageNo, int pageSize, IDbConnection conn,
      string tableName, IEnumerable<string> whereEnumerable, IEnumerable<string> fieldEnumerable,
      IEnumerable<string> orderEnumerable, string alias, object param = null,
      bool loadNow = true) where T : BaseModel
    {
      var whereSql = GetWhereSql(whereEnumerable);

      var selectCountSql = $@"
{SqlCharConst.SELECT} {SqlCharConst.COUNT}(1) 
{SqlCharConst.FROM} {tableName}
{whereSql}
";

      var count = await conn.QueryFirstAsync<int>(selectCountSql, param);

      Logger.Debug($"{nameof(selectCountSql)}:{selectCountSql}");

      var resultPage = new PageModel<IEnumerable<T>>()
      {
        Count = count,
        PageNo = pageNo,
        PageSize = pageSize
      };

      if ((pageNo - 1) * pageSize <= count)
      {
        var pageListSql = $@"
{SqlCharConst.SELECT} {string.Join(",", fieldEnumerable)}
{SqlCharConst.FROM} {tableName} {SqlCharConst.AS} {alias}
{whereSql}

{SqlCharConst.ORDERBY} {string.Join(",", orderEnumerable)}

{SqlCharConst.LIMIT} {(pageNo - 1) * pageSize},{pageSize}

";

        Logger.Debug($"{nameof(pageListSql)}:{pageListSql}");

        var enumerable = await conn.QueryAsync<T>(pageListSql, param);

        resultPage.Result = enumerable;

        if (loadNow) resultPage.Result = resultPage.Result.ToList();
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
      var sql = $@"
{SqlCharConst.INSERT} {SqlCharConst.INTO} {tableName}
(
  {string.Join(",", EntityTools.GetFields<T>())}
)
{SqlCharConst.VALUES} 
(
  {string.Join(",", EntityTools.GetFields<T>("@"))}
)
";

      Logger.Debug($"{nameof(CreateItem)}:{sql}");

      var result = await conn.ExecuteAsync(sql, param);

      return result;
    }
  }
}