using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Advance.Study
{
  /// <summary>
  /// @desc : Volatile  
  /// @author : mons
  /// @create : 2019/5/20 12:01:38 
  /// @source : 
  /// </summary>
  public class VolatileDemo
  {

    public volatile int num;

    public void Run()
    {
      
      num = 100;

      for (int i = 0; i < 30; i++)
      {
        Task.Run((() =>
        {
          for (int j = 0; j < 1000; j++)
          {
            num++;
            Interlocked.Increment(ref num);
          }
        }));
      }

    }

  }
}
