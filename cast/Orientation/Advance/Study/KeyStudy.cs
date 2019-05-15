using System;
using System.Collections.Generic;
using System.Text;

namespace Advance.Study
{
  /// <summary>
  /// @desc : KeyStudy  18个不常见的C#关键字，您使用过几个？
  /// @author : mons
  /// @create : 2019/5/15 14:23:29 
  /// @source : https://www.cnblogs.com/zhuqil/archive/2010/04/09/UnCommon-Csharp-keywords-A-Look.html
  /// </summary>
  public class KeyStudy
  {
    [Obsolete]
    public int paramLength(__arglist)
    {//不支持Vararg调用约定
      return 0;
    }

    [Obsolete]
    public void Test()
    {
      paramLength(__arglist(1, 2, 3));
    }
  }
}