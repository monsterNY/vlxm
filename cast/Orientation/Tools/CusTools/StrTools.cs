using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.CusTools
{
  /// <summary>
  /// @desc : StrTools  
  /// @author : mons
  /// @create : 2019/5/14 14:44:03 
  /// @source : 
  /// </summary>
  public static class StrTools
  {
    public static string HeadUpper(this string str)
    {
      if (string.IsNullOrWhiteSpace(str))
        return str;

      if (str[0] >= 97 && str[0] < 97 + 26)
        return $"{(char) (str[0] - 32)}{str.Substring(1)}";

      return str;
    }
  }
}