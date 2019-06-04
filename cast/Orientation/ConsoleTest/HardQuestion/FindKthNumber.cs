using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ConsoleTest.HardQuestion
{
  /// <summary>
  /// @desc : FindKthNumber  
  /// @author : mons
  /// @create : 2019/6/4 15:44:12 
  /// @source : https://leetcode.com/problems/kth-smallest-number-in-multiplication-table/
  /// </summary>
  public class FindKthNumber
  {
    //Memory Limit
    public int Simple(int m, int n, int k)
    {
      var list = new List<int>();

      for (int l = 1; l <= m; l++)
      {
        for (int o = 1; o <= n; o++)
        {
          Console.Write($"{l * o}\t");
          list.Add(l * o);
        }

        Console.WriteLine();
      }

      list.Sort();

      Console.WriteLine();

      for (int i = 0; i < list.Count; i++)
        Console.Write($"[i:{i},value:{list[i]}]\t");

      Console.WriteLine();

      return list[k - 1];
    }

    /*
     * think:
     *
     * first: we get a table :
     *    x x x x ...
     *    x x x x
     *    x x x x
     *    .
     *    .
     *    .
     *
     * set i = 1,j = 1, nextMax = {i = 2, j = 2}
     *
     * begin change i->m j->n
     *
     *  if(m>=n) we change i++
     *  else we change j++
     *
     *  when 走到尽头即 i==m || j==n 时我们转换i,j查看是否能继续走
     *    由于m>=n时i++,m<n时j++ 所以我们永远走的都是较长的那条线所以我们不需要转换到另一边走
     *
     *  如果不能则将i,j = nextMax
     *  else 继续走
     *
     *  在走的过程中减少k
     *
     *  当k=0时返回i*j
     *
     *  在走的过程中我们应验证i*j是否小于nextMax
     *    if true : 继续走
     *    else 将i,j = nextMax nextMax = 当前的i,j
     *  
     *
     */

    //bug
    public int Try(int m, int n, int k)
    {
      int i = 1, j = 1, nextI = 2, nextJ = 2, maxLen = Math.Max(m, n);
      bool flag = m >= n;
      while (k-- > 1)
      {
        if (i == maxLen || j == maxLen)
        {
          i = nextI;
          j = nextJ;
          nextI = nextJ = Math.Min(i, j) + 1;
        }

        if (flag)
          i++;
        else
          j++;

        if (i * j > nextI * nextJ)
        {
          var prevI = i;
          var prevJ = j;
          i = nextI;
          j = nextJ;
          nextI = prevI;
          nextJ = prevJ;
        }
        else if (i != j && i <= n && j <= m)
          k--;

        Console.WriteLine($"i:{i},j:{j},nextI:{nextI},nextJ:{nextJ},k:{k},value:{i * j}");
      }

      return i * j;
    }

    public int Solution(int m, int n, int k)
    {
      int i = 1, j = 1, maxLen = Math.Max(m, n);

      Dictionary<int, int[]> dictionary = new Dictionary<int, int[]>()
      {
        {4, new[] {2, 2}}
      };

      bool flag = m >= n;
      while (k-- > 1)
      {
        if (i == maxLen || j == maxLen)
        {
          var prevSum = i * j;
          var minValue = int.MaxValue;
          foreach (var item in dictionary)
          {
            if (item.Key <= prevSum) continue;
            if (item.Key < minValue)
            {
              minValue = item.Key;
              i = item.Value[0];
              j = item.Value[1];
            }
          }

          dictionary.Remove(minValue);

          var max = Math.Max(i, j) + 1;

          dictionary.Add(max * max, new[] {max, max});
        }

        if (flag)
          i++;
        else
          j++;

        var key = 0;
        var prevI = i;
        var prevJ = j;

        foreach (var item in dictionary)
        {
          if (i * j > item.Key)
          {
            i = item.Value[0];
            j = item.Value[1];
            key = item.Key;
            break;
          }
        }

        if (key > 0)
        {
          dictionary.Remove(key);
          if (!dictionary.ContainsKey(prevJ * prevI))
            dictionary.Add(prevJ * prevI, new[] {prevI, prevJ});
          var min = Math.Min(prevJ, prevI) + 1;
          if (!dictionary.ContainsKey(min * min))
            dictionary.Add(min * min, new[] {min, min});
        }
        else if (i != j && i <= n && j <= m)
          k--;

        Console.WriteLine($"i:{i},j:{j},k:{k},value:{i * j},map:{JsonConvert.SerializeObject(dictionary)}");
      }

      return i * j;
    }
  }
}