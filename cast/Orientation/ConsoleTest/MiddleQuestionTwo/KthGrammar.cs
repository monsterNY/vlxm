using System;
using System.Collections.Generic;
using System.Text;
using Tools.CusAttr;
using Tools.CusMenu;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : KthGrammar  
  /// @author :mons
  /// @create : 2019/4/17 10:45:30 
  /// @source : https://leetcode.com/problems/k-th-symbol-in-grammar/
  /// </summary>
  [Obsolete("next fix...")]
  [Love(LoveTypes.Question)]
  public class KthGrammar
  {

    public void Try()
    {
      List<int> list = new List<int>() { 0 };

      for (int i = 0; i < 15; i++)
      {
        //        Console.WriteLine(JsonConvert.SerializeObject(list));

        int prev = list[0];
        int count = 1;

        StringBuilder builder = new StringBuilder();

        for (int j = 0; j < list.Count; j++)
        {
          //          if (list[j] == prev)
          //          {
          //            count++;
          //          }
          //          else
          //          {
          //            builder.Append($"<{count}>");
          //            prev = list[j];
          //            count = 1;
          //          }
          if (list[j] == 0)
            builder.Append($"<{j}>");
        }
        //        builder.Append($"<{prev}:{count}>");

        Console.WriteLine(builder.ToString());

        for (int j = 0; j < list.Count; j += 2)
        {
          if (list[j] == 0)
          {
            list.Insert(j + 1, 1);
          }
          else
          {
            list.Insert(j + 1, 0);
          }
        }
      }

    }

    public int Solution(int N, int K)
    {
      return 0;
    }

  }
}
