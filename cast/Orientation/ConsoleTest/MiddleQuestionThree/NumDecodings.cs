using System;
using System.Collections.Generic;
using System.Text;
using Tools.CusAttr;
using Tools.CusMenu;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : NumDecodings  
  /// @author : mons
  /// @create : 2019/4/29 15:26:14 
  /// @source : https://leetcode.com/problems/decode-ways/
  /// </summary>
  [Love(LoveTypes.Fix)]
  public class NumDecodings
  {

    /**
     * Runtime: 72 ms, faster than 99.66% of C# online submissions for Decode Ways.
     * Memory Usage: 20.5 MB, less than 72.41% of C# online submissions for Decode Ways.
     *
     * 感觉还好  就是0有点坑
     *
     */
    public int Solution(string s)
    {

      /**
       *
       * step1: 找出规律
       *    单字母 - 1
       *    双子符可组成 26 - 3 (由此可见无法组成26的部分无须考虑)
       *    3个字符  count:5
       *    4个字符  count:8
       *    5个字符  count:13
       *    推论: n个字符 count:(n-1)个字符 + (n-2)个字符
       *
       *    细节:由于至少有两个字符，所以n-1 和 n-2 初始都为 1
       *    从 1开始验证
       *
       * step2: 排除0[这部分是真的坑。。。]
       *
       *    情况一 : 当前为0
       *      由于情况一与情况二同时验证，所以当前为0的情况下，前一个一定不满足条件，直接返回0
       *
       *    情况二 : 下一个为0
       *      则当前必须为 1 或 2 否则返回0
       *      跳过2个位置
       *
       *    step1的验证 在前一个为0的情况下无效。
       *
       * step3: 计算结果
       *    将各个部分的count相乘获取结果
       *
       */

      if (s.Length == 0) return 0;

      int count = 1, itemCount = 1, prev = 1;

      for (int i = 0; i < s.Length; i++)
      {
        if (s[i] == '0')
          return 0;

        if (i + 1 < s.Length && s[i + 1] == '0')
        {
          if (s[i] <= '2' && s[i] >= '0')
          {
            i++;
            continue;
          }

          return 0;
        }

        if (i > 0 && s[i - 1] != '0')
        {
          if ((s[i - 1] - '0') * 10 + s[i] - '0' <= 26)
          {
            var temp = itemCount;
            itemCount += prev;
            prev = temp;
          }
          else if (itemCount > 0)
          {
            count *= itemCount;
            itemCount = 1;
            prev = 1;
          }
        }
      }

      return itemCount > 0 ? count * itemCount : count;
    }
  }
}