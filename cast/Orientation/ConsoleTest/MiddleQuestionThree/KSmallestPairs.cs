using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : KSmallestPairs  
  /// @author :mons
  /// @create : 2019/4/22 14:52:57 
  /// @source : https://leetcode.com/problems/find-k-pairs-with-smallest-sums/
  /// </summary>
  public class KSmallestPairs
  {
    public IList<int[]> Solution(int[] nums1, int[] nums2, int k)
    {
      return null;
    }

    /**
     * Runtime: 360 ms, faster than 40.28% of C# online submissions for Find K Pairs with Smallest Sums.
     * Memory Usage: 65.5 MB, less than 16.67% of C# online submissions for Find K Pairs with Smallest Sums.
     *
     * stm
     *
     */
    public IList<int[]> Simple(int[] nums1, int[] nums2, int k)
    {
      IList<int[]> list = new List<int[]>();

      foreach (var num in nums1)
      {
        foreach (var num2 in nums2)
        {
          list.Add(new[] {num, num2});
        }
      }

      return list.OrderBy(u => u[0] + u[1]).Take(k).ToList();
    }

    //bug
    public IList<int[]> Try(int[] nums1, int[] nums2, int k)
    {
      IList<int[]> list = new List<int[]>();

      //bug 死循环
//      var flag = new bool[nums1.Length][];
//
//      for (int l = 0; l < flag.Length; l++)
//      {
//        flag[l] = new bool[nums2.Length];
//      }

      int[] target = new int[nums1.Length + 1], target2 = new int[nums2.Length + 1];
      Array.Fill(target, -1);
      Array.Fill(target2, -1);

      int prevI = nums1.Length - 1, prevJ = nums2.Length - 1;

      int i = 0, j = 0;
      while (k-- > 0 && i < nums1.Length && j < nums2.Length)
      {
        list.Add(new[] {nums1[i], nums2[j]});

        target[i] = j;
        target2[j] = i;

        Console.WriteLine($"i:{i},j:{j},num1:{nums1[i]},num2:{nums2[j]}");

        if (i == nums1.Length - 1)
        {
          j++;
          i = target2[j] + 1;
        }
        else if (j == nums2.Length - 1)
        {
          i++;
          j = target[i] + 1;
        }
        else if (nums1[i + 1] + nums2[target[i + 1] + 1] < nums1[prevI] + nums2[prevJ])
        {
          i++;
          j = target[i] + 1;
        }
        else
        {
          i = prevI;
          j = prevJ;
        }


        //        else if (nums1[i + 1] + nums2[target[i + 1] + 1] < nums1[target2[j + 1] + 1] + nums2[j + 1])
        //        {
        //          i++;
        //          j = target[i] + 1;
        //        }
        //        else
        //        {
        //          j++;
        //          i = target2[j] + 1;
        //        }

        //        else if (nums1[i + 1] < nums2[j + 1])
        //        {
        //          
        //          i++;
        //          j = target[i] + 1;
        //        }
        //        else
        //        {
        //          j++;
        //          i = target2[j] + 1;
        //        }
      }

      return list;
    }
  }
}