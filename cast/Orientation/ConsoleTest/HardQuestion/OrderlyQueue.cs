using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.HardQuestion
{
  /// <summary>
  /// @desc : OrderlyQueue  
  /// @author : mons
  /// @create : 2019/5/29 17:21:49 
  /// @source : 
  /// </summary>
  public class OrderlyQueue
  {

    //1 <= K <= S.length <= 1000
    //S consists of lowercase letters only.
    public string Solution(string S, int K)
    {

      if (K == 1)
      {

      }
      else
      {

      }

      return new string(S.ToCharArray().OrderBy(u => u).ToArray());
    }
  }
}