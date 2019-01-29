using System.Data;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using NLog;

namespace Api.Manage.Assist.Extension
{
  public static class HttpContextExt
  {

    public static ILogger Logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// 获取连接对象
    /// </summary>
    /// <param name="context"></param>
    /// <param name="key"></param>
    /// <param name="connStr"></param>
    /// <returns></returns>
    public static IDbConnection GetConnection(this HttpContext context, string key, string connStr)
    {
      IDbConnection conn;
      if (context.Items.ContainsKey(key))
      {
        Logger.Debug("使用缓存中的数据库连接");
        conn = context.Items[key] as IDbConnection;
      }
      else
      {
        Logger.Debug("创建连接对象");
        conn = new MySqlConnection(connStr);
        context.Items.Add(key, conn); //一个上下文共享一个连接
      }

      return conn;
    }
  }
}