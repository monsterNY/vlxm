using System;
using System.Runtime.InteropServices;
using NoSafe.Entities;

namespace NoSafe
{
  class Program
  {
    static void Main(string[] args)
    {

      //      ReadMe();

      UseSpan();

      Console.WriteLine("Have a nice day!");

      Console.ReadKey();
    }

    static unsafe void UseSpan()
    {
      //通过span操作内存类型
      /**
       * why use span?
       *
       * 为了在使用不安全代码和指针的同时，得到.net的安全保障
       *
       * 例如堆栈溢出、内存碎片、栈撕裂等等
       *
       */

      //1.托管内存（managed memory ）
      var managedMemory = new byte[100];
      Span<byte> span = managedMemory;

      //2.栈内存（stack memory ）
      var stackedMemory = stackalloc byte[100];
      span = new Span<byte>(stackedMemory, 100);

      //3.本机内存（native memory ）
      var nativeMemory = Marshal.AllocHGlobal(100);
      var nativeSpan = new Span<byte>(nativeMemory.ToPointer(), 100);
    }

    static void ReadMe()
    {
      //内存类型

      /**
       * 1.托管内存
       *
       * @copy: 很熟悉吧，只需使用new操作符就分配了一块托管堆内存，而且还不用手工释放它，因为它是由垃圾收集器（GC）管理的，
       * GC会智能地决定何时释放它，这就是所谓的托管内存。默认情况下，GC通过复制内存的方式分代管理小对象（size < 85000 bytes），
       * 而专门为大对象（size >= 85000 bytes）开辟大对象堆（LOH），管理大对象时，并不会复制它，而是将其放入一个列表，提供较慢的分配和释放，而且很容易产生内存碎片。
       *
       */
      var instance = new Personal();

      /**
       * 2.栈内存
       *
       * @copy:很简单，使用stackalloc关键字非常快速地就分配好了一块栈内存，也不用手工释放，它会随着当前作用域而释放，
       * 比如方法执行结束时，就自动释放了。栈内存的容量非常小（ ARM、x86 和 x64 计算机，默认堆栈大小为 1 MB），当你使用栈内存的容量大于1M时，
       * 就会报StackOverflowException 异常 ，这通常是致命的，不能被处理，而且会立即干掉整个应用程序，所以栈内存一般用于需要小内存，
       * 但是又不得不快速执行的大量短操作，比如微软使用栈内存来快速地记录ETW事件日志。
       *
       */
      unsafe
      {
        var stackMemory = stackalloc byte[100];
        stackMemory = stackMemory + 5;
      }

      /**
       * 本机内存
       *
       * @copy:通过调用方法Marshal.AllocHGlobal或Marshal.AllocCoTaskMem来分配非托管堆内存，非托管就是垃圾回收器（GC）不可见的意思，
       * 并且还需要手工调用方法Marshal.FreeHGlobal or Marshal.FreeCoTaskMem 释放它，千万不能忘记，不然就内存泄漏了。
       *
       */
      IntPtr nativeMemory0 = default(IntPtr), nativeMemory1 = default(IntPtr);
      try
      {
        nativeMemory0 = Marshal.AllocHGlobal(256);
        nativeMemory1 = Marshal.AllocCoTaskMem(256);
      }
      finally
      {
        Marshal.FreeHGlobal(nativeMemory0);
        Marshal.FreeCoTaskMem(nativeMemory1);
      }

    }

  }
}