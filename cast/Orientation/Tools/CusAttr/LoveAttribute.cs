using System;
using System.Collections.Generic;
using System.Text;
using Tools.CusMenu;

namespace Tools.CusAttr
{
  /// <summary>
  /// @desc : LoveAttribute  
  /// @author :mons
  /// @create : 2019/3/29 11:02:17 
  /// @source : I love this
  /// </summary>
  public class LoveAttribute:Attribute
  {
    public LoveTypes Type { get; set; }

    public LoveAttribute(LoveTypes type)
    {
      Type = type;
    }
  }
}
