using System;
using System.Collections.Generic;
using System.Text;
using Tools.CusAttr;
using Tools.CusMenu;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : FindNumberOfLIS  
  /// @author :mons
  /// @create : 2019/4/23 11:30:10 
  /// @source : 
  /// </summary>
  [Love(QuestionTypes.Dp)]
  [Obsolete]
  public class FindNumberOfLIS
  {
    public int findNumberOfLIS(int[] nums)
    {
      int n = nums.Length, res = 0, max_len = 0;
      int[] len = new int[n], cnt = new int[n];
      for (int i = 0; i < n; i++)
      {
        len[i] = cnt[i] = 1;
        for (int j = 0; j < i; j++)
        {
          if (nums[i] > nums[j])
          {
            if (len[i] == len[j] + 1) cnt[i] += cnt[j];//这里想错了。。。
            else if (len[i] < len[j] + 1)
            {
              len[i] = len[j] + 1;
              cnt[i] = cnt[j];
            }
          }
        }

        if (max_len == len[i]) res += cnt[i];
        if (max_len < len[i])
        {
          max_len = len[i];
          res = cnt[i];
        }
      }

      return res;
    }









    public int Solution(int[] nums)
    {
      //step1:获取递增列表的长度
      var dp = new int[nums.Length];

//      for (int i = 1; i < nums.Length; i++)
//      {
//        for (int j = 0; j < i; j++)
//        {
//          if (nums[i] > nums[j] && dp[j] + 1 > dp[i])
//          {
//            dp[i] = dp[j] + 1;
//          }
//        }
//      }

      //step2 : 获取最长递增列表的数量

      var method = new int[nums.Length]; //到达长度dp[i]的数量

      for (int i = 1; i < nums.Length; i++)
      {
        for (int j = 0; j < i; j++)
        {
          if (nums[i] > nums[j])
          {
            if (dp[j] + 1 == dp[i])
            {
              method[i]++;
            }
            else if (dp[j] + 1 >= dp[i])
            {
              dp[i] = dp[j] + 1;
              method[i] = 1;
            }
          }
        }
      }

      return 0;
    }

    public int Try(int[] nums)
    {
      if (nums.Length == 0) return 0;
      var dp = new int[nums.Length];

      var len = new int[nums.Length];

      Array.Fill(dp, 1);

      for (int i = 1; i < nums.Length; i++)
      {
        for (int j = 0; j < i; j++)
        {
          if (nums[i] > nums[j] && dp[j] + 1 >= dp[i])
          {
            dp[i] = dp[j] + 1;
            len[dp[i]--] = Math.Max(len[j], len[i]);
          }
        }

        len[i]++;
      }

      return 0;
    }
  }
}