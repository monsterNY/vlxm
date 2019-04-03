using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : MaxProduct  
  /// @author :mons
  /// @create : 2019/4/3 14:14:43 
  /// @source : https://leetcode.com/problems/maximum-product-of-word-lengths/
  /// </summary>
  [Description("operation char more efficient~")]
  public class MaxProduct
  {

    /**
     * Runtime: 108 ms, faster than 100.00% of C# online submissions for Maximum Product of Word Lengths.
     * Memory Usage: 26.3 MB, less than 33.33% of C# online submissions for Maximum Product of Word Lengths.
     *
     * cool,amazing
     *
     */
    public static int maxProduct(String[] words)
    {
      if (words == null || words.Length == 0)
        return 0;
      int len = words.Length;
      int[] value = new int[len];
      for (int i = 0; i < len; i++)
      {
        String tmp = words[i];
        value[i] = 0;
        for (int j = 0; j < tmp.Length; j++)
        {
          value[i] |= 1 << (tmp[j] - 'a');//???
        }
      }
      int maxProduct = 0;
      for (int i = 0; i < len; i++)
      for (int j = i + 1; j < len; j++)
      {
        if ((value[i] & value[j]) == 0 && (words[i].Length * words[j].Length > maxProduct))
          maxProduct = words[i].Length * words[j].Length;
      }
      return maxProduct;
    }

    public int Optimize(string[] words)
    {
      //ISet<int> set = new HashSet<int>();//更耗时
      bool noCommand;
      bool[] flag;

      var max = 0;

      for (int i = 0; i < words.Length; i++)
      {
        flag = new bool[26];
        //set.Clear();

        for (int j = 0; j < words[i].Length; j++)
        {
          flag[words[i][j] - 97] = true;
          //set.Add(words[i][j]);
        }

        for (int j = i + 1; j < words.Length; j++)
        {
          noCommand = true;

          if (words[i].Length * words[j].Length <= max) continue;

          for (int k = 0; k < words[j].Length; k++)
          {
            //if (set.Contains(words[j][k]))
            if (flag[words[j][k] - 97])
            {
              noCommand = false;
              break;
            }
          }

          if (noCommand)
            max = words[i].Length * words[j].Length;
        }
      }

      return max;
    }

    /**
     * Runtime: 128 ms, faster than 88.16% of C# online submissions for Maximum Product of Word Lengths.
     * Memory Usage: 26.2 MB, less than 50.00% of C# online submissions for Maximum Product of Word Lengths.
     */
    public int Solution(string[] words)
    {
      bool[] flag = new bool[26];
      bool noCommand;

      var max = 0;

      for (int i = 0; i < words.Length; i++)
      {
        Array.Fill(flag, false);

        for (int j = 0; j < words[i].Length; j++)
        {
          flag[words[i][j] - 97] = true;
        }

        for (int j = i + 1; j < words.Length; j++)
        {
          noCommand = true;

          if (words[i].Length * words[j].Length <= max) continue;

          for (int k = 0; k < words[j].Length; k++)
          {
            if (flag[words[j][k] - 97])
            {
              noCommand = false;
              break;
            }
          }

          if (noCommand)
            max = words[i].Length * words[j].Length;
        }
      }

      return max;
    }
  }
}