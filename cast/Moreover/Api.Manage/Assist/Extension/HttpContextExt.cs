using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;

namespace Api.Manage.Assist.Extension
{
  public static class HttpContextExt
  {
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
        conn = context.Items[key] as IDbConnection;
      }
      else
      {
        conn = new MySqlConnection(connStr);
        context.Items.Add(key, conn); //一个上下文共享一个连接
      }

      return conn;
    }
  }
}