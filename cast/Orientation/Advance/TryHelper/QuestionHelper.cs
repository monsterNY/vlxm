using System;
using System.Collections.Generic;
using System.Text;

namespace Advance.TryHelper
{
  /// <summary>
  /// @desc : QuestionHelper  
  /// @author : mons
  /// @create : 2019/5/20 15:57:05 
  /// @source : 
  /// </summary>
  public class QuestionHelper:IDisposable
  {

    public static object obj = new object();

    public void Try()
    {

      var test = QuestionHelper.obj;
      //var test2 = ~QuestionHelper.obj;//error ~ 代表运算符 按位求补。

      var empty1 = QuestionHelper.obj.GetHashCode();
      var empty = ~QuestionHelper.obj.GetHashCode();

    }

    public void Dispose()
    {
      GC.SuppressFinalize(this);
    }
  }
}
