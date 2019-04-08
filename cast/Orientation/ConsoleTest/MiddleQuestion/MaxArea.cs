using System;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : MaxArea  
  /// @author :mons
  /// @create : 2019/4/8 15:16:55 
  /// @source : https://leetcode.com/problems/container-with-most-water/
  /// </summary>
  public class MaxArea
  {
    #region otherSolution

    /**
     * amazing........
     */
    public int OtherSolution(int[] height)
    {
      int i = 0, j = height.Length - 1, water = 0;
      while (i < j)
      {
        water = Math.Max(water, (j - i) * Math.Min(height[i], height[j]));
        if (height[i] < height[j])
          i++;
        else
          j--;
      }

      return water;
    }

    #endregion

    /**
     * Runtime: 828 ms, faster than 39.18% of C# online submissions for Container With Most Water.
     * Memory Usage: 25.5 MB, less than 78.63% of C# online submissions for Container With Most Water.
     */
    public int Simple(int[] height)
    {
      var max = 0;
      for (int i = 0; i < height.Length; i++)
      {
        for (int j = i + 1; j < height.Length; j++)
        {
          max = Math.Max(max, j - i * Math.Min(height[i], height[j]));
        }
      }

      return max;
    }
  }
}