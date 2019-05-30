using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.HardQuestion
{
  /// <summary>
  /// @desc : RectangleArea  
  /// @author : mons
  /// @create : 2019/5/30 15:42:22 
  /// @source : https://leetcode.com/problems/rectangle-area-ii/
  /// </summary>
  [Obsolete("预留")]
  public class RectangleArea
  {

    public int Solution(int[][] rectangles)
    {
      var none = new[] {0, 0, 0, 0};
      for (int i = 0; i < rectangles.Length; i++)
      {
        var area = rectangles[i];

        for (int j = 0; j < i; j++)
        {
          var compareArea = rectangles[j];

          if (compareArea == none) continue;

          if (area[0] == compareArea[0] && area[3] == compareArea[3])
          {
            if (area[1] > compareArea[1] && area[1] < compareArea[4])
            {
              rectangles[j][4] = Math.Max(area[4], compareArea[4]);

              rectangles[i] = none;
            }

            if (area[1] < compareArea[1])
            {

            }
          }
        }
      }

      return 0;
    }
  }
}