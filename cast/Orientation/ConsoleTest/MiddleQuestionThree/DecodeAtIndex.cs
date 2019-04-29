using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Tools.CusAttr;
using Tools.CusMenu;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : DecodeAtIndex  
  /// @author : mons
  /// @create : 2019/4/29 9:40:51 
  /// @source : https://leetcode.com/problems/decoded-string-at-index/
  /// </summary>
  [Love(LoveTypes.Question,LoveTypes.Fix)]
  public class DecodeAtIndex
  {
    /**
     * 皇天不负有心人~~~
     *
     * Runtime: 84 ms, faster than 94.44% of C# online submissions for Decoded String at Index.
     * Memory Usage: 20.8 MB, less than 100.00% of C# online submissions for Decoded String at Index.
     *
     */
    public string Solution(string S, int K)
    {
      int num = 0, start = 0, count = 0, nextStart = 0;

      var startList = new List<int>();
      var end = new List<int>();
      var strList = new List<string>();

      bool flag;

      for (int i = 0; i < S.Length; i++)
      {
        flag = false;
        if (S[i] >= '0' && S[i] <= '9')
        {
          num = (num == 0 ? 1 : num) * (S[i] - '0');
        }
        else if (num > 0)
        {
          flag = true;
        }
        else
        {
          count++;
          if (nextStart + count == K) return S[i].ToString();
        }

        if (flag || i == S.Length - 1)
        {
          var str = S.AsSpan(start, count).ToString();

          startList.Add(nextStart + 1);
          end.Add(nextStart + count);
          strList.Add(str);

          if (num > 0)
          {
            startList.Add(1);
            end.Add((nextStart + count) * num);
            strList.Add(null);
          }

          if (K <= end[end.Count - 1] || end[end.Count - 1] < 0) //超过int.maxValue
          {
//            Console.WriteLine(JsonConvert.SerializeObject(startList));
//            Console.WriteLine(JsonConvert.SerializeObject(end));
//            Console.WriteLine(JsonConvert.SerializeObject(strList));

            for (int j = startList.Count - 1; j >= 0; j--)
            {
              if (strList[j] == null) continue;

              if (end[j] > 0)
                K %= end[j];

              if (K == 0) return strList[j][strList[j].Length - 1].ToString();
              if (K >= startList[j])
              {
                return strList[j][(K - startList[j]) % strList[j].Length].ToString();
              }
            }
          }

          start = i;
          num = 0;
          count = 1;
          nextStart = end[end.Count - 1];
        }
      }

      return string.Empty;
    }

    public string Optimize(string S, int K)
    {
      int num = 0, start = 0, count = 0, nextStart = 0;

      var startList = new List<int>();
      var end = new List<int>();
      var strList = new List<string>();

      bool flag;

      for (int i = 0; i < S.Length; i++)
      {
        flag = false;
        if (S[i] >= '0' && S[i] <= '9')
        {
          num = (num == 0 ? 1 : num) * (S[i] - '0');
        }
        else if (num > 0)
        {
          flag = true;
        }
        else
        {
          count++;
          if (nextStart + count == K) return S[i].ToString();
        }

        if (flag || i == S.Length - 1)
        {
          var str = S.AsSpan(start, count).ToString();

          startList.Add(nextStart + 1);
          end.Add(nextStart + count);
          strList.Add(str);

          nextStart = (nextStart + count) * num;

          if (K <= nextStart || nextStart < 0) //超过int.maxValue
          {
            for (int j = startList.Count - 1; j >= 0; j--)
            {
              if (end[j] > 0)
                K %= end[j];

              if (K == 0) return strList[j][strList[j].Length - 1].ToString();
              if (K >= startList[j])
              {
                return strList[j][(K - startList[j]) % strList[j].Length].ToString();
              }
            }
          }

          start = i;
          num = 0;
          count = 1;
        }
      }

      return string.Empty;
    }

    //同样的效率。。。
    public string Optimize2(string S, int K)
    {

      /**
       * 举例:
       *
       * leet2code3 20
       *
       * first:
       *  遍历到2 此时 start = 0 count = 4
       *  则我们将1-4 区间 和对应的str - "leet" 加入列表
       *
       *  end = 8
       *
       * second:
       *  遍历到3 此时 start = 5 count = 4
       *  则我们将(end+1 即9)-(end+count即12) 区间 和对应的str - "code" 加入列表
       *
       *  end = 36
       *
       * 由于K小于end
       *  step1: 遍历列表
       *  step2: k%区间的end 36 % 12 = 0
       *  step3: 验证: k == 0 即刚好为最后一个元素  or k 大于等于 区间的start 即k在区间内 直接返回
       *
       * 重复进行
       *
       * 解释: code包含区间 9-12  1-36中的 9-12 21-24 33 - 36
       *    
       *
       */

      int start = 0, count = 0, nextStart = 0;
      //保存每个节点的区间
      List<int> startList = new List<int>(),end = new List<int>();
      var strList = new List<string>();

      bool prevIsNum = false;

      for (int i = 0; i < S.Length; i++)
      {
        if (S[i] >= '0' && S[i] <= '9')// is num
        {
          if (!prevIsNum)//prev 不是num 则添加此区间
          {
            startList.Add(nextStart + 1);
            end.Add(nextStart + count);
            strList.Add(S.AsSpan(start, count).ToString());
          }

          //更新最大位置
          nextStart = (nextStart + count) * (S[i] - '0');
          start = i + 1;
          count = 0;
          prevIsNum = true;

          if (K <= nextStart || nextStart < 0) //如果最大位置超过k 获取 超过int.maxValue 则在列表中搜索答案
          {
            for (int j = startList.Count - 1; j >= 0; j--)
            {
              K %= end[j];

              var len = strList[j].Length;

              if (K == 0) return strList[j][len - 1].ToString();
              if (K >= startList[j])
                return strList[j][(K - startList[j])].ToString();
            }
          }
        }
        else
        {
          count++;
          if (nextStart + count == K) return S[i].ToString();
          prevIsNum = false;
        }
      }

      return string.Empty;
    }

    //同样的效率。。。
    public string Optimize3(string S, int K)
    {
      int start = 0, count = 0, nextStart = 0;
      //保存每个节点的区间
      List<int> startList = new List<int>(), end = new List<int>(),indexStart = new List<int>();

      bool prevIsNum = false;

      for (int i = 0; i < S.Length; i++)
      {
        if (S[i] >= '0' && S[i] <= '9')// is num
        {
          if (!prevIsNum)//prev 不是num 则添加此区间
          {
            startList.Add(nextStart + 1);
            end.Add(nextStart + count);
            indexStart.Add(start);
          }

          //更新最大位置
          nextStart = (nextStart + count) * (S[i] - '0');
          start = i + 1;
          count = 0;
          prevIsNum = true;

          if (K <= nextStart || nextStart < 0) //如果最大位置超过k 获取 超过int.maxValue 则在列表中搜索答案
          {
            for (int j = startList.Count - 1; j >= 0; j--)
            {
              K %= end[j];

              var len = end[j] - startList[j];

              if (K == 0) return S[indexStart[j] + end[j] - startList[j]].ToString();
              if (K >= startList[j])
                return S[indexStart[j] + K - startList[j]].ToString();
            }
          }
        }
        else
        {
          count++;
          if (nextStart + count == K) return S[i].ToString();
          prevIsNum = false;
        }
      }

      return string.Empty;
    }

    //稍微有些理解错误
    public string Try(string S, int K)
    {
      int num = 0, start = 0, count = 0, nextStart = 0;

      var startList = new List<int>();
      var end = new List<int>();
      var strList = new List<string>();

      bool flag;

      for (int i = 0; i < S.Length; i++)
      {
        flag = false;
        if (S[i] >= '0' && S[i] <= '9')
        {
          num = num * 10 + S[i] - '0';
        }
        else if (num > 0)
        {
          flag = true;
        }
        else
        {
          count++;
          if (nextStart + count == K) return S[i].ToString();
        }

        if (flag || i == S.Length - 1)
        {
          var str = S.AsSpan(start, count).ToString();

          startList.Add(nextStart + 1);
          end.Add(nextStart + count);
          strList.Add(str);

          if (num > 0)
          {
            startList.Add(1);
            end.Add((nextStart + count) * num);
            strList.Add(null);
          }

          if (K <= end[end.Count - 1])
          {
            Console.WriteLine(JsonConvert.SerializeObject(startList));
            Console.WriteLine(JsonConvert.SerializeObject(end));
            Console.WriteLine(JsonConvert.SerializeObject(strList));

            for (int j = startList.Count - 1; j >= 0; j--)
            {
              if (strList[j] == null) continue;

              K %= end[j];

              if (K == 0) return strList[j][strList[j].Length - 1].ToString();
              if (K >= startList[j])
              {
                return strList[j][(K - 1) % strList[j].Length].ToString();
              }
            }
          }

          start = i;
          num = 0;
          count = 1;
          nextStart = end[end.Count - 1];
        }
      }

      return string.Empty;
    }


    public string ShowStr(string S, int K)
    {
      StringBuilder builder = new StringBuilder();

      for (int i = 0; i < S.Length; i++)
      {
        if (S[i] >= '0' && S[i] <= '9')
        {
          var str = builder.ToString();

          builder.Clear();
          for (int j = 0; j < S[i] - '0'; j++)
          {
            builder.Append(str);
          }
        }
        else
        {
          builder.Append(S[i]);
        }
      }

      return builder.ToString();
    }

    public string ShowStr2(string S, int K)
    {
      StringBuilder builder = new StringBuilder();

      int start = 0, count = 0, num = 0;

      for (int i = 0; i < S.Length; i++)
      {
        if (S[i] >= '0' && S[i] <= '9')
        {
          num = num * 10 + S[i] - '0';
          if (i == S.Length - 1)
          {
            var str = builder.ToString() + S.AsSpan(start, count).ToString();

            builder.Clear();
            for (int j = 0; j < num; j++)
            {
              builder.Append(str);
            }
          }
        }
        else
        {
          if (num > 0)
          {
            var str = builder.ToString() + S.AsSpan(start, count).ToString();

            builder.Clear();

            for (int j = 0; j < num; j++)
            {
              builder.Append(str);
            }

            start = i;
            num = 0;
            count = 0;
          }
          else
          {
            count++;
          }
        }
      }

      return builder.ToString();
    }
  }
}