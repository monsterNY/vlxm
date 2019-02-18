using System;
using System.Runtime.InteropServices;
using NoSafe.Entities;

namespace NoSafe
{
  class Program
  {
    static void Main(string[] args)
    {

      //span<T> .net core 2.1,2.2 support

      //ReadMe();

      UseSpan();

      //span的作用：
      //高性能，避免不必要的内存分配和复制。
      //高效率，它可以为任何具有无复制语义的连续内存块提供安全和可编辑的视图，极大地简化了内存操作，即不用为每一种内存类型操作写一个重载方法。
      //内存安全，span内部会自动执行边界检查来确保安全地读写内存，但它并不管理如何释放内存，而且也管理不了，因为所有权不属于它，希望大家要明白这一点

      string contentLength = "Content-Length: 132";
      var length = GetContentLength(contentLength.ToCharArray());
      Console.WriteLine($"Content length: {length}");

      Console.WriteLine("Have a nice day!");

      Console.WriteLine($"{SubString("Hello",1,4)}");

      Console.ReadKey();
    }

    private static string SubString(ReadOnlySpan<char> span,int startIndex,int length)
    {
      var slice = span.Slice(startIndex,length);//类似于substring
      return slice.ToString();
    }

    private static int GetContentLength(ReadOnlySpan<char> span)
    {
      var slice = span.Slice(16);//类似于substring
      return Int32.Parse(slice);
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

      //span就像黑洞一样，能够吸收来自于内存任意区域的数据，实际上，现在，在.Net的世界里，Span就是所有类型内存的抽象化身，
      //表示一段连续的内存，它的API设计和性能就像数组一样，所以我们完全可以像使用数组一样地操作各种内存，真的是太方便了。

      //Span<T> 是一种ref-like type类似引用的结构体；从应用的场景上看，它是高性能的sliceable type可切片类型；
      //综上所诉，Span是一种类似于数组的结构体，但具有创建数组一部分视图，而无需在堆上分配新对象或复制数据的超能力。

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