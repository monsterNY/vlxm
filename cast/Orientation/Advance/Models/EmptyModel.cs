using System;
using System.Collections.Generic;
using System.Text;

namespace Advance.Models
{
  /// <summary>
  /// @desc : EmptyModel  
  /// @author : mons
  /// @create : 2019/7/18 15:37:42 
  /// @source : 
  /// </summary>
  public class EmptyModel
  {
    public override bool Equals(object obj)
    {
      Console.WriteLine(nameof(Equals));
      return base.Equals(obj);
    }
  }
}