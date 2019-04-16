using System;
using System.Collections.Generic;
using System.Text;
using Tools.CusAttr;
using Tools.CusMenu;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : BrokenCalc  
  /// @author :mons
  /// @create : 2019/4/16 10:40:09 
  /// @source : https://leetcode.com/problems/broken-calculator/
  /// </summary>
  [Obsolete]
  [Love(LoveTypes.Question, LoveTypes.Fix)]
  public class BrokenCalc
  {
    /**
     *
     * On a broken calculator that has a number showing on its display, we can perform two operations:
* Double: Multiply the number on the display by 2, or;
* Decrement: Subtract 1 from the number on the display.
* Initially, the calculator is displaying the number X.
* 
* Return the minimum number of operations needed to display the number Y.
     *
     * analysis :
     *  condition: 初始数-x,目标数-y
     *  operation: double -> x *= 2  or decrement -> x--;
     *  need: 完成目标的最小操作数
     *  key: 是选择 double 还是 decrement emmm .... / no imagination
     *
     */

    /**
     * y->x
     * 太秀了吧。。。
     */
    public int OtherSolution(int X, int Y)
    {
      int res = 0;
      while (Y > X)
      {
        Y = Y % 2 > 0 ? Y + 1 : Y / 2;
        res++;
        Console.WriteLine($"x:{X},y:{Y},res:{res}");
      }

      return res + X - Y;
    }

    //x->y
    public int brokenCalc(int X, int Y)
    {
      int multiple = 1, res = 0;
      while (X * multiple < Y)
      {
        multiple <<= 1;
        res++;
      }

      int diff = X * multiple - Y;
      while (diff > 0)
      {
        res += diff / multiple;
        diff -= diff / multiple * multiple;
        multiple >>= 1;
      }

      return res;
    }

    public int Solution(int x, int y)
    {
      var count = 0;

      while (x != y)
      {
        if (x > y)
        {
          x--; //初始数大于y的情况只能通过递减达到目标
        }
        else
        {
          if (y > x * 4)
          {
            if (y % 2 != 0) count++;
            y /= 2;
          }
          else
          {
            if (y > x * 2)
            {
              return count;
            }
          }
        }

        count++;
      }

      return count;
    }

    private int minCount;
  }
}