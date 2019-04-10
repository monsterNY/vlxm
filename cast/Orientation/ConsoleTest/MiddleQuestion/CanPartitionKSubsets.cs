using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : CanPartitionKSubsets  
  /// @author :mons
  /// @create : 2019/4/10 15:57:09 
  /// @source : https://leetcode.com/problems/partition-to-k-equal-sum-subsets/
  /// </summary>
  [Obsolete("no imagination")]
  public class CanPartitionKSubsets
  {

    #region otherSolution

    /**
     * Runtime: 100 ms, faster than 68.33% of C# online submissions for Partition to K Equal Sum Subsets.
     * Memory Usage: 21.9 MB, less than 100.00% of C# online submissions for Partition to K Equal Sum Subsets.
     */
    public bool canPartitionKSubsets(int[] nums, int k)
    {
      int sum = 0;
      foreach (int num in nums) sum += num;
      if (k <= 0 || sum % k != 0) return false;
      int[] visited = new int[nums.Length];
      return canPartition(nums, visited, 0, k, 0, 0, sum / k);
    }

    public bool canPartition(int[] nums, int[] visited, int start_index, int k, int cur_sum, int cur_num, int target)
    {
      if (k == 1) return true;
      if (cur_sum == target && cur_num > 0) return canPartition(nums, visited, 0, k - 1, 0, 0, target);
      for (int i = start_index; i < nums.Length; i++)
      {
        if (visited[i] == 0)
        {
          visited[i] = 1;
          if (canPartition(nums, visited, i + 1, k, cur_sum + nums[i], cur_num++, target)) return true;
          visited[i] = 0;
        }
      }
      return false;
    }

    #endregion


    public bool Solution(int[] nums, int k)
    {
      var sum = 0;

      foreach (var num in nums)
      {
        sum += num;
      }

      if (sum % k != 0) return false;

      int target = sum / k;

      bool[] visit = new bool[nums.Length];

      Array.Sort(nums);

      for (int i = nums.Length - 1; i >= 0; i--)
      {
        var item = 0;
        for (int j = nums.Length - 1; j >=0; j--)
        {
          if (visit[j]) continue;

          if (target - item >= nums[j])
          {
            visit[j] = true;
            item += nums[j];
            if (item == target) break;
          }
        }

        if (item > 0 && item != target)
        {
          return false;
        }
      }

      return true;
    }
  }
}