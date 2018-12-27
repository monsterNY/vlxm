using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Vlxm.CusAttr
{
  /// <summary>
  /// 表名
  /// </summary>
  [AttributeUsage(AttributeTargets.Class)]
  public class TableNameAttribute : Attribute
  {
    public string TableName { get; set; }

    public TableNameAttribute(string tableName)
    {
      TableName = tableName;
    }
  }
}