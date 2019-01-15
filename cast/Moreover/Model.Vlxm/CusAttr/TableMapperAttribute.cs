using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Vlxm.CusAttr
{
  /// <summary>
  /// 表名
  /// </summary>
  [AttributeUsage(AttributeTargets.Class)]
  public class TableMapperAttribute : Attribute
  {
    public string TableName { get; set; }

    public string Prefix { get; set; }

    public string Alias { get; set; }
    
    public TableMapperAttribute(string tableName, string prefix, string @alias)
    {
      TableName = tableName;
      Prefix = prefix;
      Alias = alias;
    }
  }
}