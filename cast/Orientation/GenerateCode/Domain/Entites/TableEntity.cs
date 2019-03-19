using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateCode.Domain.Entites
{
  /// <summary>
  /// @desc : TableEntity  
  /// @author :mons
  /// @create : 2019/3/19 10:56:13 
  /// @source : 
  /// </summary>
  public class TableEntity
  {

    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime? UpdateTime { get; set; }

  }
}
