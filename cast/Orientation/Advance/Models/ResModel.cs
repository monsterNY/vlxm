using System;
using System.Collections.Generic;
using System.Text;

namespace Advance.Models
{
  /// <summary>
  /// @desc : ResModel  资源文件
  /// @author : mons
  /// @create : 2019/5/15 16:58:21 
  /// @source : 
  /// </summary>
  public class ResModel : IDisposable
  {
    public void Dispose()
    {
      Console.WriteLine($"资源已回收~,{this.GetHashCode()}");
    }
  }
}