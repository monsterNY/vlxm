using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateCode.Domain.Entites
{
  /// <summary>
  /// @desc : FieldEntity  
  /// @author :mons
  /// @create : 2019/3/19 10:57:40 
  /// @source : 
  /// </summary>
  public class FieldEntity
  {

    public string Name { get; set; }

    public string Description { get; set; }

    public string Type { get; set; }

    public bool IsNullAble { get; set; }

  }
}
