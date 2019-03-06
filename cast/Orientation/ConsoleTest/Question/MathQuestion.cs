using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.Question
{


  /// <summary>
  /// @source:https://q.cnblogs.com/q/113504/
  /// </summary>
  public class MathQuestion
  {

    /**
     * 假设有起始值2000，结束值15000，通过
    * 第2个数=2000 x（1+key）
    * 第3个数=第2个数 x（1+key）
    * 第4个数=第3个数 x（1+key）
    * 最终第1300个数=15000
    * 求这个key
     */

    public double GetKey(double startNum, double endNum)
    {

      //startNum * (1+key)^1300-1 = endNum

      //1+key = （1300-1）√endNum/startNum

      //key = （1300-1）√endNum/startNum - 1

//      var key = Math.Pow(endNum / startNum, 1d / 1300d);

      return 0;

    }

  }
}
