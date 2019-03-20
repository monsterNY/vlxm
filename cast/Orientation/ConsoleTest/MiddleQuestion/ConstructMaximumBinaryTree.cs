using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.LeetCode;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : ConstructMaximumBinaryTree  
  /// @author :mons
  /// @create : 2019/3/20 14:49:49 
  /// @source : https://leetcode.com/problems/maximum-binary-tree/
  /// </summary>
  public class ConstructMaximumBinaryTree
  {
    /**
     * Runtime: 116 ms, faster than 97.20% of C# online submissions for Maximum Binary Tree.
     * Memory Usage: 27.9 MB, less than 58.33% of C# online submissions for Maximum Binary Tree.
     *
     * oh nice~
     *
     */
    public TreeNode Solution(int[] nums)
    {
      return InTree(nums, 0, nums.Length);
    }

    public TreeNode InTree(int[] nums, int start, int end)
    {
      var maxIndex = GetMaxIndex(nums, start, end);

      if (maxIndex == -1) return null;

      var node = new TreeNode(nums[maxIndex]);

      node.right = InTree(nums, maxIndex + 1, end);
      node.left = InTree(nums, start, maxIndex);

      return node;
    }

    public int GetMaxIndex(int[] arr, int start, int end)
    {
      if (start >= end) return -1;
      var maxIndex = start;

      for (int i = start + 1; i < end; i++)
        if (arr[i] > arr[maxIndex])
          maxIndex = i;

      return maxIndex;
    }

    public TreeNode NotUnderstand(int[] nums)
    {
      List<int> leftList = new List<int>();

      List<int> rightList = new List<int>();

      int max = nums[0];
      var maxIndex = 0;

      for (int i = 1; i < nums.Length; i++)
      {
        if (nums[i] > max)
        {
          max = nums[i];
          rightList.Clear();
          for (; maxIndex < i; maxIndex++)
          {
            leftList.Add(nums[maxIndex]);
          }
        }
        else
        {
          rightList.Add(nums[i]);
        }
      }

      leftList.Sort();
      rightList.Sort();

      TreeNode root = new TreeNode(max);

      TreeNode node = root;

      for (var i = leftList.Count - 1; i >= 0; i--)
      {
        node.left = new TreeNode(leftList[i]);
        node = node.left;
      }

      node = root;

      for (var i = rightList.Count - 1; i >= 0; i--)
      {
        node.right = new TreeNode(rightList[i]);
        node = node.right;
      }

      return root;
    }
  }
}