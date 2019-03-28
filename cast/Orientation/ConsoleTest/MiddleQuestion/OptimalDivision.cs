using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : OptimalDivision  
  /// @author :mons
  /// @create : 2019/3/28 14:42:31 
  /// @source : https://leetcode.com/problems/optimal-division/
  /// </summary>
  public class OptimalDivision
  {
    public string Solution(int[] nums)
    {
      bool flag = false;
      StringBuilder builder = new StringBuilder(nums[0].ToString());
      for (int i = 1; i < nums.Length; i++)
      {
        if (i == nums.Length - 1)
        {
          builder.Append($"/{nums[i]}");
          break;
        }
        else if (nums[i] >= nums[i + 1])
        {
          if (!flag)
          {
            builder.Append("/(");
          }
          else
          {
            builder.Append("/");
          }

          builder.Append(nums[i]);
          flag = true;
        }
        else
        {
          builder.Append($"/{nums[i]}");
          if (flag)
            builder.Append(")");

          flag = false;
        }
      }

      if (flag)
        builder.Append(")");

      return builder.ToString();
    }

    public string Try(int[] nums)
    {
      double sum = nums[0];
      double num = nums[1];

      for (int i = 2; i < nums.Length; i++)
      {
        if (i == nums.Length - 1)
        {
          num = num / nums[i];
          break;
        }
        else if (nums[i] > nums[i + 1])
        {
          num = num / nums[i];
        }
        else
        {
          num = num / nums[i++] / nums[i];
        }
      }

      return (sum / num).ToString();
    }

    string OtherSolution(int[] nums)
    {
      string ans = string.Empty;
      if (nums.Length == 0) return ans;
      ans = nums[0].ToString();
      if (nums.Length == 1) return ans;
      if (nums.Length == 2) return ans + "/" + nums[1].ToString();
      ans += "/(" + nums[1].ToString();
      for (int i = 2; i < nums.Length; ++i)
        ans += "/" + nums[i].ToString();
      ans += ")";
      return ans;
    }

    /**
     * 吐血了我。。。
     */
    string SameSolution(int[] nums)
    {
      if (nums.Length == 0) return string.Empty;

      if (nums.Length == 1) return nums[0].ToString();

      if (nums.Length == 2) return $"{nums[0]}/{nums[1]}";

      StringBuilder ans = new StringBuilder(nums[0].ToString());
      ans.Append($"/({nums[1]}");

      for (int i = 2; i < nums.Length; ++i)
        ans.Append($"/{nums[i]}");

      ans.Append(")");

      return ans.ToString();
    }
  }
}