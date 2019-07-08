using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Advance
{
  /// <summary>
  /// @desc : LookIL  
  /// @author : mons
  /// @create : 2019/7/8 11:50:32 
  /// @source : 
  /// </summary>
  public class LookIL
  {
    public async void Run()
    {
      Task.Run(() => { Console.WriteLine("------------"); });
    }
  }
}