using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : CanVisitAllRooms  
  /// @author :mons
  /// @create : 2019/3/26 11:52:43 
  /// @source : https://leetcode.com/problems/keys-and-rooms/
  /// </summary>
  public class CanVisitAllRooms
  {

    /**
     * Runtime: 100 ms, faster than 100.00% of C# online submissions for Keys and Rooms.
     * Memory Usage: 24 MB, less than 92.86% of C# online submissions for Keys and Rooms.
     *
     * nice~
     *
     */
    public bool Solution(IList<IList<int>> rooms)
    {
      bool[] arr = new bool[rooms.Count];
      arr[0] = true;

      InRoom(rooms, rooms[0], arr);

      for (int i = 0; i < arr.Length; i++)
        if (!arr[i]) return false;

      return true;
    }

    public void InRoom(IList<IList<int>> rooms, IList<int> room, bool[] arr)
    {
      for (int i = 0; i < room.Count; i++)
      {
        if (arr[room[i]]) continue;
        arr[room[i]] = true;
        InRoom(rooms, rooms[room[i]], arr);
      }
    }

  }
}