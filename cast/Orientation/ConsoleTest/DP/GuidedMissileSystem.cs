using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.DP
{
  /// <summary>
  /// @desc : GuidedMissileSystem  
  /// @author :mons
  /// @create : 2019/4/8 9:55:19 
  /// @source : http://blog.sina.com.cn/s/blog_50eaa92f0100c8t7.html
  /// </summary>
  public class GuidedMissileSystem
  {
    public int Solution(int[] arr)
    {
      int[] dp = new int[arr.Length];

      var count = 0;
      dp[arr.Length - 1] = 1;

      for (int i = arr.Length - 2; i >= 0; i--)
      {
        for (int j = i + 1; j < dp.Length; j++)
        {
          if (arr[i] >= arr[j] && dp[i] < dp[j] + 1)//***dp[i] < dp[j] + 1*** very important
          {
            dp[i] = dp[j] + 1;//so cool
          }
        }

        if (dp[i] > count) count = dp[i];
      }

      return count;
    }
  }
}