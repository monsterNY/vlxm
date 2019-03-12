using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using DapperContext.CusAttr;

namespace DapperContext.Middleware
{
  /// <summary>
  /// Uses the Name value of the ColumnAttribute specified, otherwise maps as usual.
  /// </summary>
  /// <typeparam name="T">The type of the object that this mapper applies to.</typeparam>
  public class ColumnAttributeTypeMapper<T> : FallbackTypeMapper
  {
    public ColumnAttributeTypeMapper()
      : base(new SqlMapper.ITypeMap[]
      {
        new CustomPropertyTypeMap(
          typeof(T),
          (type, columnName) =>
            type.GetProperties().FirstOrDefault(prop =>
              prop.GetCustomAttributes(false)
                .OfType<ColumnMappingAttribute>()
                .Any(attr => attr.Name == columnName)
            )
        ),
        new DefaultTypeMap(typeof(T))
      })
    {
    }
  }
}