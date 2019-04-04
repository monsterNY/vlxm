using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : DecodeString  
  /// @author :mons
  /// @create : 2019/4/4 15:22:24 
  /// @source : https://leetcode.com/problems/decode-string/
  /// </summary>
  public class DecodeString
  {
    /**
     * Runtime: 148 ms, faster than 13.94% of C# online submissions for Decode String.
     * Memory Usage: 20.4 MB, less than 100.00% of C# online submissions for Decode String.
     *
     * ????
     *
     * Runtime: 84 ms, faster than 94.02% of C# online submissions for Decode String.
     * Memory Usage: 20.6 MB, less than 100.00% of C# online submissions for Decode String.
     *
     * 一下快一下慢的。。。怀疑人生
     *
     */
    public string Solution(string s)
    {
      //我的递归~

      //终于等到你，还好我没放弃

      //Finally waited for you, fortunately I did not give up

      //finally wait you,don't give up


      //      s = "3[a]2[bc]", return "aaabcbc".
      //        s = "3[a2[c]]", return "accaccacc".
      //        s = "2[abc]3[cd]ef", return "abcabccdcdcdef".

      StringBuilder builder = new StringBuilder();
      for (int j = 0; j < s.Length; j++)
      {
        if (s[j] >= '0' && s[j] <= '9')
          builder.Append(GetValue(s, ref j, s[j] - '0'));
        else
          builder.Append(s[j]);
      }

      return builder.ToString();
    }

    /**
     * Runtime: 80 ms, faster than 100.00% of C# online submissions for Decode String.
     * Memory Usage: 20.6 MB, less than 94.12% of C# online submissions for Decode String.
     *
     * Runtime: 80 ms, faster than 100.00% of C# online submissions for Decode String.
     * Memory Usage: 20.6 MB, less than 100.00% of C# online submissions for Decode String.
     *
     */
    public StringBuilder GetValue(string s, ref int index, int mutiply)
    {
      if (index == s.Length) return null;

      StringBuilder itemBuild = new StringBuilder();
      bool flag = false;
      for (; index < s.Length; index++)
      {
        if (s[index] >= '0' && s[index] <= '9')
        {
          if (flag)
            itemBuild.Append(GetValue(s, ref index, s[index] - '0'));
          else
            mutiply = mutiply * 10 + s[index] - '0';
        }
        else if (s[index] == '[')
        {
          flag = true;
        }
        else if (s[index] == ']')
        {
          var itemStr = itemBuild.ToString();
          for (int j = 1; j < mutiply; j++)
          {
            itemBuild.Append(itemStr);
          }

          return itemBuild;
        }
        else
        {
          itemBuild.Append(s[index]);
        }
      }

      return itemBuild;
    }

    public StringBuilder GetValue(string s, ref int index)
    {
      if (index == s.Length) return null;

      var num = 0;
      StringBuilder itemBuild = new StringBuilder();
      bool flag = false;
      for (; index < s.Length; index++)
      {
        if (s[index] >= '0' && s[index] <= '9')
        {
          if (flag)
            itemBuild.Append(GetValue(s, ref index));
          else
            num = num * 10 + s[index] - '0';
        }
        else if (s[index] == '[')
        {
          flag = true;
        }
        else if (s[index] == ']')
        {
          var itemStr = itemBuild.ToString();
          for (int j = 1; j < num; j++)
          {
            itemBuild.Append(itemStr);
          }

          return itemBuild;
        }
        else
        {
          itemBuild.Append(s[index]);
          if (num == 0) return itemBuild;
        }
      }

      return itemBuild;
    }
  }
}