using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advance.Codewars
{
  /// <summary>
  /// @desc : OpenOrSenior  
  /// @author : mons
  /// @create : 2019/7/18 16:56:24 
  /// @source : 
  /// </summary>
  public class OpenOrSenior
  {
    /**
     * The Western Suburbs Croquet Club has two categories of membership, Senior and Open. They would like your help with an application form that will tell prospective members which category they will be placed.
       
       To be a senior, a member must be at least 55 years old and have a handicap greater than 7. In this croquet club, handicaps range from -2 to +26; the better the player the lower the handicap.
     */

    private const string Open = nameof(Open);
    private const string Senior = nameof(Senior);

    public static IEnumerable<string> Solution(int[][] data)
    {
      List<string> list = new List<string>();

      foreach (var item in data)
      {
        if (item[0] >= 55 && item[1] > 7) list.Add(Senior);
        else list.Add(Open);
      }

      return list;
    }

    public static IEnumerable<string> Solution2(int[][] data)
    {

      return data.Select(item => (item[0] >= 55 && item[1] > 7) ? Senior : Open);
      
    }

  }
}