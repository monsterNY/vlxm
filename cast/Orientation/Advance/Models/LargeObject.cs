using System;
using System.Collections.Generic;
using System.Text;

namespace Advance.Models
{
  /// <summary>
  /// @desc : LargeObject  
  /// @author : mons
  /// @create : 2019/5/24 17:26:53 
  /// @source : 
  /// </summary>
  public class LargeObject
  {

    public int InitializedBy { get { return initBy; } }

    int initBy = 0;
    public LargeObject(int initializedBy)
    {
      initBy = initializedBy;
      Console.WriteLine("LargeObject was created on thread id {0}.", initBy);
    }

    public long[] Data = new long[100000000];

  }
}
