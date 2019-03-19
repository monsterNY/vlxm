using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenerateCode.CusInterface;

namespace GenerateCode.Inherit
{
  /// <summary>
  /// @desc : OracleOperation  
  /// @author :mons
  /// @create : 2019/3/19 16:33:43 
  /// @source : 
  /// </summary>
  public class OracleOperation:IDbOperation
  {
    public string GetAllTableSql(string dbName)
    {
      throw new NotImplementedException();
    }

    public string GetAllFieldsSql(string dbName, string tableName)
    {
      throw new NotImplementedException();
    }

    public string GetType(string dbType, out bool canIsNull)
    {
      canIsNull = true;

      switch (dbType.Trim().ToLower())
      {
        case "char":
        case "varchar":
        case "nchar":
        case "nvarchar":
        case "text":
        case "ntext":
          canIsNull = false;
          return "string";
        case "datetime":
        case "smalldatetime":
        case "timestamp":
          return nameof(DateTime);
        case "bigint":
          return "long";
        case "binary":
        case "image":
        case "varbinary":
          canIsNull = false;
          return "byte[]";
        case "int":
          return "int";
        case "smallint":
          return "short";
        case "variant":
          return "Object";
        case "bit":
          return "bool";
        case "decimal":
        case "money":
        case "smallmoney":
        case "double":
        case "numeric":
          return "decimal";
        case "float":
        case "real":
          return "float";
        case "tinyint":
          return "byte";
        case "uniqueidentifier":
          return "System.Guid";
        default:
          canIsNull = false;
          return "object";
      }
    }
  }
}
