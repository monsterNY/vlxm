using System;
using System.Runtime.InteropServices;
using Study.Entity;

namespace Study
{
  class Program
  {
    static void Main(string[] args)
    {

      //C#操作任何类型的内存

      //1.托管类型
      var instance = new StudyEntity();

      //2.栈内存（stack memory ）
      unsafe
      {
        var stackMemory = stackalloc byte[100];
      }

      //3.本机内存（native memory ）
      IntPtr nativeMemory0 = default(IntPtr), nativeMemory1 = default(IntPtr);
      try
      {
        unsafe
        {
          nativeMemory0 = Marshal.AllocHGlobal(256);
          nativeMemory1 = Marshal.AllocCoTaskMem(256);
        }
      }
      finally
      {
        Marshal.FreeHGlobal(nativeMemory0);
        Marshal.FreeCoTaskMem(nativeMemory1);
      }



      Console.WriteLine("Happy New Year!");

      Console.ReadKey(true);

    }
  }
}
