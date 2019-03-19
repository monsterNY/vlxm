using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateCode.CusInterface
{
  /// <summary>
  /// @desc : IDbOperation  
  /// @author :mons
  /// @create : 2019/3/19 10:33:56 
  /// @source : https://blog.csdn.net/u012643122/article/details/44039155
  /// </summary>
  public interface IDbOperation : IDbOperation<IDbConnection>
  {
  }

  public interface IDbOperation<out T> where T : IDbConnection
  {
    /// <summary>
    /// 查询表名
    /// </summary>
    /// <returns></returns>
    string GetAllTableSql(string dbName);

    /// <summary>
    /// 获取所有字段和类型
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    string GetAllFieldsSql(string dbName, string tableName);

    /// <summary>
    /// dbType映射到程序的type
    /// </summary>
    /// <param name="dbType"></param>
    /// <returns></returns>
    string GetType(string dbType,out bool canIsNull);

  }
}