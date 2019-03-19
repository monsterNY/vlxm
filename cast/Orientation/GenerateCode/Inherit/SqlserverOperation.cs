using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenerateCode.CusInterface;

namespace GenerateCode.Inherit
{
  /// <summary>
  /// @desc : SqlserverOperation  
  /// @author :mons
  /// @create : 2019/3/19 15:50:04 
  /// @source :
  ///   https://www.cnblogs.com/atree/p/SQL-Server-sysobjects.html
  ///   https://www.cnblogs.com/qy1234/p/9044275.html
  /// </summary>
  public class SqlserverOperation : IDbOperation
  {
    public string GetAllTableSql(string dbName)
    {
      return $@"
SELECT
  d.name as name,
  f.value as description,
  d.crdate as createTime
FROM
	sysobjects d 
	left join
	 sys.extended_properties f
	on 
	 d.id=f.major_id and f.minor_id=0
WHERE
   d.xtype='U'
";
    }

    public string GetAllFieldsSql(string dbName, string tableName)
    {
      return $@"
SELECT 
    name     = a.name,
    Type       = b.name,
    IsNullable     = case when a.isnullable=1 then 1 else 0 end,
    Description   = isnull(g.[value],'')
FROM 
    syscolumns a
left join 
    systypes b 
on 
    a.xusertype=b.xusertype
inner join 
    sysobjects d 
on 
    a.id=d.id  and d.xtype='U' and  d.name<>'dtproperties'
left join 
sys.extended_properties   g 
on 
    a.id=g.major_id and a.colid=g.minor_id  
where 
    d.name='{tableName}'    --如果只查询指定表,加上此where条件，tablename是要查询的表名；去除where条件查询所有的表信息
order by 
    a.id,a.colorder
";
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