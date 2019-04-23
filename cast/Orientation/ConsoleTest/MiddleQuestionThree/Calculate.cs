using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : Calculate  
  /// @author :mons
  /// @create : 2019/4/23 16:08:51 
  /// @source : https://leetcode.com/problems/basic-calculator-ii/
  /// </summary>
  public class Calculate
  {
    /**
     * Runtime: 96 ms, faster than 51.57% of C# online submissions for Basic Calculator II.
     *
     * Memory Usage: 23.8 MB, less than 50.00% of C# online submissions for Basic Calculator II.
     */
    public int Solution(string s)
    {
      IList<int> nums = new List<int>();

      IList<int> opt = new List<int>();

      bool prevIsNum = false;

      int sum = 0, num;

      for (int i = 0; i < s.Length; i++)
      {
        if (s[i] == ' ') continue;

        if (s[i] >= '0' && s[i] <= '9')
        {
          if (prevIsNum)
          {
            nums[nums.Count - 1] = nums[nums.Count - 1] * 10 + s[i] - '0';
          }
          else
          {
            nums.Add(s[i] - '0');
          }

          prevIsNum = true;
        }
        else
        {
          if (opt.Count > 0 && nums.Count > 1)
          {
            if (opt[opt.Count - 1] == '*')
            {
              num = nums[nums.Count - 1] * nums[nums.Count - 2];
              nums.RemoveAt(nums.Count - 1);
              nums[nums.Count - 1] = num;
              opt.RemoveAt(opt.Count - 1);
            }
            else if (opt[opt.Count - 1] == '/')
            {
              num = nums[nums.Count - 2] / nums[nums.Count - 1];
              nums.RemoveAt(nums.Count - 1);
              nums[nums.Count - 1] = num;
              opt.RemoveAt(opt.Count - 1);
            }
          }

          opt.Add(s[i]);
          prevIsNum = false;
        }
      }

      if (opt.Count > 0)
      {
        if (opt[opt.Count - 1] == '*')
        {
          num = nums[nums.Count - 1] * nums[nums.Count - 2];
          nums.RemoveAt(nums.Count - 1);
          nums[nums.Count - 1] = num;
          opt.RemoveAt(opt.Count - 1);
        }
        else if (opt[opt.Count - 1] == '/')
        {
          num = nums[nums.Count - 2] / nums[nums.Count - 1];
          nums.RemoveAt(nums.Count - 1);
          nums[nums.Count - 1] = num;
          opt.RemoveAt(opt.Count - 1);
        }
      }

      if (nums.Count > 0)
      {
        sum += nums[0];
        for (int i = 0; i < opt.Count; i++)
        {
          if (opt[i] == '+')
          {
            sum += nums[i + 1];
          }
          else
          {
            sum -= nums[i + 1];
          }
        }
      }

      return sum;
    }

    /**
     * Runtime: 80 ms, faster than 86.16% of C# online submissions for Basic Calculator II.
     * Memory Usage: 23.7 MB, less than 50.00% of C# online submissions for Basic Calculator II.
     */
    public int Clear(string s)//s -- 操作符/非负数
    {
      //保存数字与操作符
      List<int> nums = new List<int>(), optList = new List<int>();

      //上一位是否为数字
      bool prevIsNum = false;

      for (int i = 0; i < s.Length; i++)
      {
        //空格直接跳过
        if (s[i] == ' ') continue;

        if (s[i] >= '0' && s[i] <= '9')
        {
          if (prevIsNum)//数字叠加
            nums[nums.Count - 1] = nums[nums.Count - 1] * 10 + s[i] - '0';
          else
            nums.Add(s[i] - '0');

          prevIsNum = true;
        }
        else
        {
          Helper(optList, nums);
          optList.Add(s[i]);//添加操作符
          prevIsNum = false;
        }
      }

      Helper(optList, nums);//考虑最后一个为操作符的情况。

      if (nums.Count > 0)
      {
        var sum = nums[0];
        for (int i = 0; i < optList.Count; i++)
          if (optList[i] == '+')
            sum += nums[i + 1];
          else
            sum -= nums[i + 1];
        return sum;
      }

      return 0;
    }

    public void Helper(List<int> optList, List<int> numList)
    {
      if (optList.Count > 0 && numList.Count > 1)
      {
        //若上一位操作为 * 或 / 则直接计算结果 移除操作符 和被操作数
        if (optList[optList.Count - 1] == '*')
        {
          numList[numList.Count - 2] *= numList[numList.Count - 1];
          numList.RemoveAt(numList.Count - 1);
          optList.RemoveAt(optList.Count - 1);
        }
        else if (optList[optList.Count - 1] == '/')
        {
          numList[numList.Count - 2] /= numList[numList.Count - 1];
          numList.RemoveAt(numList.Count - 1);
          optList.RemoveAt(optList.Count - 1);
        }
      }
    }
  }
}