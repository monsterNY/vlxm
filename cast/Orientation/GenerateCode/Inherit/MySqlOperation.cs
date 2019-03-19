using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenerateCode.CusInterface;
using MySql.Data.MySqlClient;

namespace GenerateCode.Inherit
{
  /// <summary>
  /// @desc : MySqlOperation  
  /// @author :mons
  /// @create : 2019/3/19 10:45:59 
  /// @source : 
  /// </summary>
  public class MySqlOperation : IDbOperation
  {
    public string GetAllTableSql(string dbName)
    {
      return $@"
SELECT 
  table_name as name,
  table_comment as description,
  create_time as createTime
FROM 
  information_schema.tables 
WHERE 
  table_type = 'BASE TABLE' AND table_schema = '{dbName}'; ";
    }

    public string GetAllFieldsSql(string dbName, string tableName)
    {
      return $@"
SELECT
  column_name as name,
  data_type as Type , 
  (
	  SELECT 1 WHERE is_nullable = 'YES'
  ) as IsNullable ,
  column_comment as  Description
FROM
  information_schema.columns 
WHERE
  table_schema='{dbName}' 
  and table_name='{tableName}';";
    }

    public string GetType(string dbType,out bool canIsNull)
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