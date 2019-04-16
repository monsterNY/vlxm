using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : AsteroidCollision  
  /// @author :mons
  /// @create : 2019/4/16 16:48:24 
  /// @source : https://leetcode.com/problems/asteroid-collision/
  /// </summary>
  public class AsteroidCollision
  {
    /// <summary>
    /// Runtime: 260 ms, faster than 85.88% of C# online submissions for Asteroid Collision.
    /// Memory Usage: 31.5 MB, less than 66.67% of C# online submissions for Asteroid Collision.
    ///
    /// fine.
    /// 
    /// </summary>
    /// <param name="asteroids"></param>
    /// <returns></returns>
    public int[] Solution(int[] asteroids)
    {
      List<int> list = new List<int>();

      for (int i = 1; i < asteroids.Length; i++)
      {
        var item = Math.Abs(asteroids[i]);
        if (asteroids[i] < 0)
        {
          for (int j = i - 1; j >= 0; j--)
          {
            if (asteroids[j] == 0) continue;
            if (asteroids[j] > 0)
            {
              if (asteroids[j] < item)
                asteroids[j] = 0;
              else if (asteroids[j] == item)
              {
                asteroids[j] = 0;
                asteroids[i] = 0;
                break;
              }
              else
              {
                asteroids[i] = 0;
                break;
              }
            }
            else break;
          }
        }
      }

      foreach (var asteroid in asteroids)
      {
        if (asteroid != 0) list.Add(asteroid);
      }

      return list.ToArray();
    }

    /**
     * Runtime: 256 ms, faster than 100.00% of C# online submissions for Asteroid Collision.
     * Memory Usage: 31.5 MB, less than 33.33% of C# online submissions for Asteroid Collision.
     *
     * nice
     */
    public int[] Solution3(int[] asteroids)
    {
      List<int> list = new List<int>();

      for (int i = 0; i < asteroids.Length; i++)
      {
        list.Add(asteroids[i]);
        if (asteroids[i] < 0)
        {
          for (int j = list.Count - 2; j >= 0; j--)
          {
            if (list[j] > 0)
            {
              if (list[j] < -asteroids[i])
              {
                list.RemoveAt(j);
              }
              else if (list[j] == -asteroids[i])
              {
                list.RemoveAt(j);
                list.RemoveAt(list.Count - 1);
                break;
              }
              else
              {
                list.RemoveAt(list.Count - 1);
                break;
              }
            }
            else break;
          }
        }
      }

      return list.ToArray();
    }

    public int[] Solution2(int[] asteroids)
    {
      var len = asteroids.Length;
      for (int i = 1; i < asteroids.Length; i++)
      {
        var item = Math.Abs(asteroids[i]);
        if (asteroids[i] < 0)
        {
          for (int j = i - 1; j >= 0; j--)
          {
            if (asteroids[j] == 0) continue;
            if (asteroids[j] > 0)
            {
              if (asteroids[j] < item)
              {
                asteroids[j] = 0;
                len--;
              }
              else if (asteroids[j] == item)
              {
                asteroids[j] = 0;
                asteroids[i] = 0;
                len -= 2;
                break;
              }
              else
              {
                asteroids[i] = 0;
                len--;
                break;
              }
            }
            else break;
          }
        }
      }

      var arr = new int[len];
      var index = 0;

      foreach (var asteroid in asteroids)
      {
        if (asteroid != 0) arr[index++] = asteroid;
      }

      return arr;
    }
  }
}