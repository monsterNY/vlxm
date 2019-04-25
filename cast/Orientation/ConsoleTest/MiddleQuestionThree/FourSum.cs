using System;
using System.Collections.Generic;
using System.Text;
using Tools.CusAttr;
using Tools.CusMenu;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : FourSum  
  /// @author : mons
  /// @create : 2019/4/25 14:24:21 
  /// @source : https://leetcode.com/problems/4sum/
  /// </summary>
  [Question(QuestionTypes.Dp)]
  [Obsolete]
  public class FourSum
  {


    public IList<IList<int>> Solution(int[] nums, int target)
    {

      Dictionary<int,int> dictionary = new Dictionary<int, int>();

      foreach (var num in nums)
      {

        if(dictionary.ContainsKey(target-num))

        if (dictionary.ContainsKey(num)) dictionary[num]++;
        else dictionary.Add(num,1);

      }

      return null;
    }

    public IList<IList<int>> Try(int[] nums, int target)
    {
      IList<IList<int>> res = new List<IList<int>>();

      Array.Sort(nums);

      Helper(nums, target, new List<int>(), new bool[nums.Length], res, 0);

      return res;
    }

    //Time Limit
    public void Helper(int[] nums, int target, IList<int> list, bool[] visited, IList<IList<int>> res, int index)
    {
      if (target == 0 && list.Count == 4)
      {
        res.Add(new List<int>(list));
        return;
      }

      var i = index;

      for (; index < nums.Length; index++)
      {
        if (visited[index] || (index > 1 && nums[index] == nums[index - 1])) continue;

        if (Math.Abs(target - nums[index]) <= Math.Abs(target))
        {
          list.Add(nums[index]);
          visited[index] = true;

          Helper(nums, target - nums[index], list, visited, res, index + 1);

          visited[index] = false;
          list.Remove(nums[index]);
        }
      }
    }
  }
}