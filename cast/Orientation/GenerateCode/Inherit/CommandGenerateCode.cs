using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using GenerateCode.CusInterface;
using GenerateCode.Domain.Entites;
using Tools.CusTools;

namespace GenerateCode.Inherit
{
  /// <summary>
  /// @desc : CommandGenerateCode  
  /// @author :mons
  /// @create : 2019/3/19 11:12:04 
  /// @source : 
  /// </summary>
  public class CommandGenerateCode
  {
    protected IDbConnection connection;

    protected IDbOperation operation;

    public CommandGenerateCode(IDbConnection connection, IDbOperation operation)
    {
      this.connection = connection;
      this.operation = operation;
    }

    public Dictionary<TableEntity, IEnumerable<FieldEntity>> Work()
    {
      var tableEntities = connection.Query<TableEntity>(operation.GetAllTableSql(connection.Database));

      Dictionary<TableEntity, IEnumerable<FieldEntity>> dictionary =
        new Dictionary<TableEntity, IEnumerable<FieldEntity>>();

      foreach (var item in tableEntities)
      {
        dictionary.Add(item, connection.Query<FieldEntity>(operation.GetAllFieldsSql(connection.Database, item.Name)));
      }


      return dictionary;
    }

    public string GenerateCode(TableEntity entity, IEnumerable<FieldEntity> fieldEntities,
      string classTemplate,
      string propTemplate, Func<string, string> generateTableNameFunc, Func<string, string> generatePropNameFunc)
    {
      classTemplate = classTemplate
        .Replace($"_{nameof(entity.Name)}_", generateTableNameFunc.Invoke(entity.Name))
        .Replace($"_{nameof(entity.CreateTime)}_", entity.CreateTime.ToString("yyyy/MM/dd hh:mm:ss"))
        .Replace($"_{nameof(entity.UpdateTime)}_", entity.UpdateTime?.ToString("yyyy/MM/dd hh:mm:ss"))
        .Replace($"_{nameof(entity.Description)}_", entity.Description);

      StringBuilder propBuilder = new StringBuilder();

      foreach (var item in fieldEntities)
      {
        propBuilder.Append(propTemplate
          .Replace($"_{nameof(item.Name)}_", generatePropNameFunc.Invoke(item.Name))
          .Replace($"_{nameof(item.Description)}_", item.Description)
          .Replace($"_{nameof(item.Type)}_", operation.GetType(item.Type, out var canIsNull))
          .Replace($"_{nameof(item.IsNullAble)}_", item.IsNullAble && canIsNull ? "?" : ""));
      }

      return classTemplate.Replace("{Props}", propBuilder.ToString());
    }
  }
}