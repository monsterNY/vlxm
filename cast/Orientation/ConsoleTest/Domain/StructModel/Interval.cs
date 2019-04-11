using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.Domain.StructModel
{
  /// <summary>
  /// @desc : Interval  
  /// @author :mons
  /// @create : 2019/4/11 9:42:37 
  /// @source : 
  /// </summary>
  public class Interval
  {
    public int start;
    public int end;

    public Interval()
    {
      start = 0;
      end = 0;
    }

    public Interval(int s, int e)
    {
      start = s;
      end = e;
    }
  }
}
