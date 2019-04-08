using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Tools.CusAttr;
using Tools.CusMenu;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : NumRescueBoats  
  /// @author :mons
  /// @create : 2019/4/8 16:28:52 
  /// @source : https://leetcode.com/problems/boats-to-save-people/
  /// </summary>
  [Love(LoveTypes.Fix)]
  public class NumRescueBoats
  {

    /**
     * Runtime: 204 ms, faster than 100.00% of C# online submissions for Boats to Save People.
     * Memory Usage: 36 MB, less than 100.00% of C# online submissions for Boats to Save People.
     *
     * 简单但sort效率还是低于非sort~~ yeah
     *
     */
    public int numRescueBoats(int[] people, int limit)
    {
      Array.Sort(people);
      int ans = 0;
      for (int hi = people.Length - 1, lo = 0; hi >= lo; --hi, ++ans)
      { // high end always moves
        if (people[lo] + people[hi] <= limit) { ++lo; } // low end moves only if it can fit in a boat with high end.
      }
      return ans;
    }

    /**
     * Runtime: 172 ms, faster than 100.00% of C# online submissions for Boats to Save People.
     * Memory Usage: 38.5 MB, less than 100.00% of C# online submissions for Boats to Save People.
     *
     * genius,cool,amazing, ha ha ha 
     *
     */
    public int Solution(int[] people, int limit)
    {
      var count = 0;

      var arr = new int[limit];

      foreach (var item in people)
        if (item == limit) count++;
        else arr[item]++;

      for (int i = arr.Length - 1; i > 0; i--)
      {
        if (arr[i] == 0) continue;
        for (int j = limit - i; j > 0; j--)
        {
          if (arr[j] == 0 || i == j) continue;
          if (arr[j] >= arr[i])
          {
            arr[j] -= arr[i];
            count += arr[i];

            arr[i] = 0;
            break;
          }
          else
          {
            count += arr[j];
            arr[i] -= arr[j];
            arr[j] = 0;
          }
        }

        count += i <= limit / 2 ? (arr[i] / 2 + arr[i] % 2) : arr[i];
        arr[i] = 0;
        ShowList(arr);
        Console.WriteLine($"count:{count}");
      }

      return count;
    }

    public void ShowList(int[] arr)
    {
      Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>");
      for (int i = arr.Length - 1; i > 0; i--)
      {
        if (arr[i] == 0) continue;
        Console.Write($"i:{i},num:{arr[i]}");
      }
      Console.WriteLine("\n<<<<<<<<<<<<<<<<<<<<<<<");
    }

  }
}