using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reading.Ref
{
  /// <summary>
  /// @desc : SimpleSpinLock  
  /// @author : mons
  /// @create : 2019/6/25 11:57:48 
  /// @source : clr via C# p682
  /// </summary>
  public class SimpleSpinLock
  {

    private int m_ResourceInUse; // 0-false 1-true

    public void Enter()
    {
      while (true)
      {

        // 总是将资源设为"正在使用"(1)
        // 只有从“未使用” 变成 “正在使用” 才会返回
        if(Interlocked.Exchange(ref m_ResourceInUse,1) == 0)return;

      }
    }

    public void Leave()
    {
      // 将资源标记为 “未使用”
      Volatile.Write(ref m_ResourceInUse,0);
    }

  }
}
