using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : MergeSortedArr  
  /// @author :mons
  /// @create : 2019/3/15 17:29:35 
  /// @source : 
  /// </summary>
  public class MergeSortedArr
  {
    public void Merge(int[] nums1, int m, int[] nums2, int n)
    {
      var j = 0;
      for (int i = 0; i < m; i++)
      {
        if (nums1[i] > nums2[j])
        {
          var moveLen = 1;
          for (j++; j < n; )
          {
            if (nums1[i] < nums2[j])
            {
              break;
            }

            moveLen++;
            j++;
          }

          for (int k = m + moveLen; k > i; k--)
          {
            nums1[k] = nums1[k - moveLen];
          }

          for (; moveLen > 0; moveLen--, i++)
          {
            nums1[i] = nums2[j - moveLen];
          }
        }
      }
    }
  }
}