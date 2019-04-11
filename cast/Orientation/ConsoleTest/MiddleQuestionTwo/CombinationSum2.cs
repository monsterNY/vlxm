using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : CombinationSum2  
  /// @author :mons
  /// @create : 2019/4/11 16:23:50 
  /// @source : https://leetcode.com/problems/combination-sum-ii/
  /// </summary>
  public class CombinationSum2
  {
    // bug no...
    public IList<IList<int>> Try2(int[] candidates, int target)
    {
      IList<IList<int>> res = new List<IList<int>>();

      Array.Sort(candidates);

      Dictionary<int, int> dictionary = new Dictionary<int, int>();


      foreach (var item in candidates)
      {
        if (dictionary.ContainsKey(item))
          dictionary[item]++;
        else
          dictionary.Add(item, 1);
      }

      GetList(dictionary.Keys.ToArray(), new List<int>(), res, 0, target, dictionary);

      return res;
    }

    public void GetList(int[] arr, IList<int> build, IList<IList<int>> result, int startIndex, int target,
      Dictionary<int, int> dictionary)
    {
      if (target < 0) return;
      if (target == 0)
      {
        result.Add(build);
      }

      for (; startIndex < arr.Length; startIndex++)
      {
        var list = new List<int>(build);
        for (int i = 0; i < dictionary[arr[i]]; i++)
        {
          list.Add(arr[startIndex]);

          GetList(arr, list, result, startIndex + 1, target - arr[startIndex], dictionary);
        }
      }
    }

    /**
     * Runtime: 264 ms, faster than 66.28% of C# online submissions for Combination Sum II.
     * Memory Usage: 30.8 MB, less than 8.33% of C# online submissions for Combination Sum II.
     *
     * emm....勉强过关
     *  
     */
    public IList<IList<int>> Try(int[] candidates, int target)
    {
      Array.Sort(candidates); //避免每次添加前sort
      return GetList(candidates, new List<int>(), new List<IList<int>>(), 0, target);
    }

    public IList<IList<int>> GetList(int[] arr, List<int> build, IList<IList<int>> result, int startIndex, int target)
    {
      if (target == 0)
      {
        if (!IsExists(result, build))
          result.Add(build);
        return result;
      }

      for (; startIndex < arr.Length; startIndex++)
      {
        if (arr[startIndex] > target) continue;

        var list = new List<int>(build);

        list.Add(arr[startIndex]);

        GetList(arr, list, result, startIndex + 1, target - arr[startIndex]);
      }

      return result;
    }

    //借鉴版
    /**
     * Runtime: 252 ms, faster than 98.06% of C# online submissions for Combination Sum II.
     * Memory Usage: 29.4 MB, less than 66.67% of C# online submissions for Combination Sum II.
     */
    public IList<IList<int>> GetList2(int[] arr, List<int> build, IList<IList<int>> result, int startIndex, int target)
    {
      if (target == 0)
      {
        result.Add(new List<int>(build)); //只在添加时重新构建 great~
        return result;
      }

      for (var index = startIndex; startIndex < arr.Length; startIndex++)
      {

        //(startIndex > index && arr[startIndex] == arr[startIndex - 1]) 直接避免重复
        /**
         * This can avoid the duplicates.
         * Our path array contains some element which picked from cand[0...cur-1].
         * We start from i=cur, now it is i> cur, which means we already tried the elements between cur to i-1 (i-1>=cur).
         * Now we are in candidate[i] and candidate[i]==candidate[i-1]. Now need to try another time.
         *
         * 感谢~
         *
         * explain : 当 startIndex > index 时  我们已经访问了startIndex 到 index  当index == index - 1 时则表示出现了重复的添加
         * emm... 大概就这意思吧.
         *
         */
        if (arr[startIndex] > target || (startIndex > index && arr[startIndex] == arr[startIndex - 1])) continue;

        build.Add(arr[startIndex]);

        GetList(arr, build, result, startIndex + 1, target - arr[startIndex]);

        build.Remove(arr[startIndex]);
      }

      return result;
    }

    //查看是否已存在
    public bool IsExists(IList<IList<int>> list, IList<int> compareList)
    {
      foreach (var item in list)
      {
        if (item.Count != compareList.Count) continue;

        bool same = true;
        for (int i = 0; i < compareList.Count; i++)
        {
          if (item[i] != compareList[i])
          {
            same = false;
            break;
          }
        }

        if (same) return false;
      }

      return true;
    }
  }
}