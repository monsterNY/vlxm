using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Dapper;
using DapperContext.CusAttr;

namespace DapperContext.Middleware
{
  /// <summary>
  /// Uses the Name value of the ColumnAttribute specified, otherwise maps as usual.
  /// </summary>
  /// <typeparam name="T">The type of the object that this mapper applies to.</typeparam>
  public class TableAttributeTypeMapper<T> : FallbackTypeMapper
  {
    public TableAttributeTypeMapper()
      : base(new SqlMapper.ITypeMap[]
      {
        new CustomPropertyTypeMap(
          typeof(T),
          (type, columnName) =>
          {
            var key = columnName.Substring(columnName.IndexOf("_") + 1).Replace("_", "");
            return type.GetProperties().FirstOrDefault(prop =>
              prop.Name.Equals(key, StringComparison.CurrentCultureIgnoreCase));
          }
        ),
        new DefaultTypeMap(typeof(T))
      })
    {
    }
  }
}