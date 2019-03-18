using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : ReverseBits  
  /// @author :mons
  /// @create : 2019/3/18 15:52:53 
  /// @source : 
  /// </summary>
  public class ReverseBits
  {
    /**
     * Runtime: 40 ms, faster than 100.00% of C# online submissions for Reverse Bits.
     * Memory Usage: 13.2 MB, less than 12.00% of C# online submissions for Reverse Bits.
     *
     * nice
     *
     */
    public uint Solution(uint num)
    {
      uint result = 0;

      uint[] arr = new uint[32];
      int index = 0;

      while (num > 0)
      {
        arr[index++] = num % 2;
        num /= 2;
      }

      for (int i = 0; i < arr.Length; i++)
      {
        result = result * 2 + arr[i];
      }

      return result;
    }

    public void Check(uint num, uint result)
    {
      var numBit = Convert.ToString(num, 2);

      var resultBit = Convert.ToString(result, 2);

      var numBitReverse = new string(numBit.Reverse().ToArray());

      for (int i = numBitReverse.Length; i < 32; i++)
      {
        numBitReverse += "0";
      }

      for (int i = resultBit.Length; i < 32; i++)
      {
        resultBit = "0" + resultBit;
      }

      if (!numBitReverse.Equals(resultBit))
      {
        throw new Exception();
      }
    }

    /**
     * https://leetcode.com/problems/reverse-bits/discuss/54741/O(1)-bit-operation-C%2B%2B-solution-(8ms)
     *
     * 太秀了
     *
     */
    public uint OtherSolution(uint n)
    {
      n = (n >> 16) | (n << 16);
      n = ((n & 0xff00ff00) >> 8) | ((n & 0x00ff00ff) << 8);
      n = ((n & 0xf0f0f0f0) >> 4) | ((n & 0x0f0f0f0f) << 4);
      n = ((n & 0xcccccccc) >> 2) | ((n & 0x33333333) << 2);
      n = ((n & 0xaaaaaaaa) >> 1) | ((n & 0x55555555) << 1);
      return n;
    }
  }
}