using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.HardQuestion
{
  /// <summary>
  /// @desc : CountOfAtoms  
  /// @author : mons
  /// @create : 2019/5/31 14:54:23 
  /// @source : https://leetcode.com/problems/number-of-atoms/
  /// </summary>
  public class CountOfAtoms
  {
    public string Solution(string formula)
    {
      var i = formula.Length - 1;

      return string.Concat(
        Helper(formula, ref i, new Dictionary<string, int>(), 1)
          .OrderBy(u => u.Key).Select(u => u.Key + (u.Value == 1 ? "" : u.Value.ToString())));
    }

    [Obsolete("Try")]
    public void Helper(string str, ref int index, List<string> list)
    {
      StringBuilder builder = new StringBuilder();

      for (; index < str.Length; index++)
      {
        if (str[index] == '(')
        {
          index++;
          Helper(str, ref index, list);
        }
        else if (str[index] == ')')
        {
        }
        else if (str[index] >= '0' && str[index] <= '9')
        {
        }
        else if (str[index] >= 'A' && str[index] <= 'Z')
        {
          if (builder.Length == 0)
            builder.Append(str[index]);
          else
            Helper(str, ref index, list);
        }
        else if (str[index] >= 'a' && str[index] <= 'z')
        {
          builder.Append(str[index]);
        }
      }

      if (builder.Length != 0)
        list.Add(builder.ToString());
    }

    /**
     * Runtime: 128 ms, faster than 6.25% of C# online submissions for Number of Atoms.
     * Memory Usage: 21.6 MB, less than 62.96% of C# online submissions for Number of Atoms.
     *
     * Runtime: 100 ms, faster than 84.38% of C# online submissions for Number of Atoms.
     * Memory Usage: 21.9 MB, less than 29.63% of C# online submissions for Number of Atoms.
     *
     *  测试有毒...
     *
     *  反过来观察会更简单~
     *
     */
    public Dictionary<string, int> Helper(string str, ref int i, Dictionary<string, int> dictionary, int count)
    {
      string builder = string.Empty;

      var num = 0;
      bool hasNum = false;

      for (; i >= 0; i--)
      {
        if (str[i] >= '0' && str[i] <= '9')
        {
          num = (str[i] - '0') * (hasNum ? 10 : 1) + num;
          hasNum = true;
        }
        else if (str[i] == '(')
        {
          return dictionary;
        }
        else if (str[i] == ')')
        {
          i--; //下标调整。
          foreach (var item in Helper(str, ref i, new Dictionary<string, int>(), num * count)) //递归求值
          {
            if (dictionary.ContainsKey(item.Key)) dictionary[item.Key] += item.Value;
            else dictionary.Add(item.Key, item.Value);
          }

          num = 0;
          hasNum = false;
        }
        else if (str[i] >= 'A' && str[i] <= 'Z')
        {
          builder = str[i] + builder;

          num = (num == 0) ? 1 : num;

          if (dictionary.ContainsKey(builder))
            dictionary[builder] += count * num;
          else
            dictionary.Add(builder, count * num);

          hasNum = false;
          num = 0;
          builder = string.Empty;
        }
        else if (str[i] >= 'a' && str[i] <= 'z')
        {
          builder += str[i];
        }
      }

      return dictionary;
    }
  }
}