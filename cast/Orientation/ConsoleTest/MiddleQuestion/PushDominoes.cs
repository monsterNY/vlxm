using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : PushDominoes  
  /// @author :mons
  /// @create : 2019/4/8 17:32:14 
  /// @source : https://leetcode.com/problems/push-dominoes/
  /// </summary>
  public class PushDominoes
  {
    /**
     * Runtime: 176 ms, faster than 22.86% of C# online submissions for Push Dominoes.
     * Memory Usage: 38.6 MB, less than 100.00% of C# online submissions for Push Dominoes.
     */
    public string Solution(string dominoes)
    {
      bool flag = true;
      while (flag)
      {
        flag = false;
        var arr = dominoes.ToCharArray();
        for (int i = 0; i < arr.Length; i++)
        {
          if (arr[i] == '.')
          {
            if (i == 0)
            {
              if (i + 1 < arr.Length && dominoes[i + 1] == 'L')
              {
                flag = true;
                arr[i] = 'L';
              }
            }
            else
            {
              if (dominoes[i - 1] == 'R' && (i + 1 >= arr.Length || dominoes[i + 1] != 'L'))
              {
                flag = true;
                arr[i] = 'R';
              }
              else if (i + 1 < arr.Length && dominoes[i + 1] == 'L' && dominoes[i - 1] != 'R')
              {
                flag = true;
                arr[i] = 'L';
              }
            }
          }
        }

        dominoes = new string(arr);
      }

      return dominoes;
    }

    /**
     * Runtime: 160 ms, faster than 22.86% of C# online submissions for Push Dominoes.
     * Memory Usage: 38.4 MB, less than 100.00% of C# online submissions for Push Dominoes.
     */
    public string Optimize(string dominoes)
    {
      bool flag = true;
      while (flag)
      {
        flag = false;
        var arr = dominoes.ToCharArray();
        for (int i = 0; i < arr.Length; i++)
        {
          if (arr[i] == '.')
          {
            if ((i == 0 || dominoes[i - 1] != 'R') && i + 1 < arr.Length && dominoes[i + 1] == 'L')
            {
              flag = true;
              arr[i] = 'L';
            }
            else if (i > 0 && dominoes[i - 1] == 'R' && (i + 1 >= arr.Length || dominoes[i + 1] != 'L'))
            {
              flag = true;
              arr[i] = 'R';
            }
          }
        }

        dominoes = new string(arr);
      }

      return dominoes;
    }



  }
}