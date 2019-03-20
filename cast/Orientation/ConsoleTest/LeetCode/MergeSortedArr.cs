using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : MergeSortedArr  
  /// @author :mons
  /// @create : 2019/3/15 17:29:35 
  /// @source : https://leetcode.com/problems/merge-sorted-array/
  /// </summary>
  public class MergeSortedArr
  {
    public void Merge(int[] nums1, int m, int[] nums2, int n)
    {
      var j = 0;
      var i = 0;
      for (; i < n; i++)
      {
        for (; j < m; j++)
          if (nums1[j] > nums2[i])
          {
            for (var k = ++m - 1; k > j && k < nums1.Length; k--) nums1[k] = nums1[k - 1];

            nums1[j] = nums2[i];
            break;
          }

        if (j == m) break;
      }

      for (; i < n;) nums1[m++] = nums2[i++];

      //failure
      //      var j = 0;
      //      for (int i = 0; i < m; i++)
      //      {
      //        if (nums1[i] > nums2[j])
      //        {
      //          var moveLen = 1;
      //          for (j++; j < n; )
      //          {
      //            if (nums1[i] < nums2[j])
      //            {
      //              break;
      //            }
      //
      //            moveLen++;
      //            j++;
      //          }
      //
      //          for (int k = m + moveLen; k > i; k--)
      //          {
      //            nums1[k] = nums1[k - moveLen];
      //          }
      //
      //          for (; moveLen > 0; moveLen--, i++)
      //          {
      //            nums1[i] = nums2[j - moveLen];
      //          }
      //
      //          m += moveLen;
      //        }
      //      }
    }

    public void CheckResult(int[] arr, int[] oldArr, int[] sortArr)
    {
      var validArr = new int[arr.Length];

      Array.Copy(oldArr, validArr, oldArr.Length);
      Array.Copy(sortArr, 0, validArr, validArr.Length - sortArr.Length, sortArr.Length);
      Array.Sort(validArr);

      for (var i = 0; i < validArr.Length; i++)
        if (validArr[i] != arr[i])
          throw new Exception($@"{nameof(oldArr)}:{JsonConvert.SerializeObject(oldArr)}
{nameof(sortArr)}:{JsonConvert.SerializeObject(sortArr)}
{nameof(arr)}:{JsonConvert.SerializeObject(arr)}
");
    }

    /**
     * Runtime: 248 ms, faster than 89.67% of C# online submissions for Merge Sorted Array.
     * Memory Usage: 28.4 MB, less than 98.36% of C# online submissions for Merge Sorted Array.
     *
     * 虽然可以添加一层循环 一次过滤出所有替换数 从而减少替换次数，但这样就不美观了，那就这样吧~
     *
     */
    public void Optimize(int[] nums1, int m, int[] nums2, int n)
    {
      var j = 0;
      var i = 0;
      for (; i < n; i++)
      {
        for (; j < m; j++)
          if (nums1[j] > nums2[i])
          {
            for (var k = ++m - 1; k > j; k--)
              nums1[k] = nums1[k - 1];

            nums1[j] = nums2[i];
            break;
          }

        if (j == m) break;
      }

      for (; i < n;) nums1[m++] = nums2[i++];
    }

    /**
     * Runtime: 244 ms, faster than 99.60% of C# online submissions for Merge Sorted Array.
     * Memory Usage: 28.4 MB, less than 98.36% of C# online submissions for Merge Sorted Array.
     */
    public void Optimize2(int[] nums1, int m, int[] nums2, int n)
    {
      var j = 0;

      for (int i = 0, moveLen = 0; i < m; i++)
      {
        for (; j < n; j++)
          if (nums1[i] >= nums2[j])
            moveLen++;
          else
            break;

        if (moveLen > 0)
        {
          for (var k = (m += moveLen) - 1; k >= i + moveLen; k--)
            nums1[k] = nums1[k - moveLen];

          for (var k = 1; k <= moveLen; k++)
            nums1[i + moveLen - k] = nums2[j - k];

          moveLen = 0;
        }
      }

      for (; j < n;) nums1[m++] = nums2[j++];
    }
  }
}