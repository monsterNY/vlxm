using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Article.CusAttr
{
  /// <summary>
  /// 字段
  /// </summary>
  [AttributeUsage(AttributeTargets.Property)]
  public class FieldAttribute : Attribute
  {
    public string Name { get; set; }

    public FieldAttribute(string name)
    {
      Name = name;
    }
  }
}