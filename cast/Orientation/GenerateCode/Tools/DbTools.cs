using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenerateCode.CusInterface;
using GenerateCode.Domain.Menu;
using GenerateCode.Inherit;
using MySql.Data.MySqlClient;

namespace GenerateCode.Tools
{
  /// <summary>
  /// @desc : GetDbConnection  
  /// @author :mons
  /// @create : 2019/3/19 10:20:18 
  /// @source : 
  /// </summary>
  public class DbTools
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="connStr"></param>
    /// <param name="type"></param>
    /// <returns> if type not exists then the return is null </returns>
    public static IDbConnection GetConnection(string connStr, DbTypes type)
    {
      IDbConnection conn = null;

      switch (type)
      {
        case DbTypes.Mysql:
          conn = new MySqlConnection(connStr);
          break;
        case DbTypes.Oracle:
//#pragma warning disable 618
          conn = new OracleConnection(connStr);
//#pragma warning restore 618.
          break;
        case DbTypes.Sqlserver:
          conn = new SqlConnection(connStr);
          break;
      }

      return conn;
    }

    public static IDbOperation GetOperation(DbTypes type)
    {
      IDbOperation operation = null;

      switch (type)
      {
        case DbTypes.Mysql:
          operation = new MySqlOperation();
          break;
        case DbTypes.Oracle:
          break;
        case DbTypes.Sqlserver:
          operation = new SqlserverOperation();
          break;
      }

      return operation;
    }
  }
}