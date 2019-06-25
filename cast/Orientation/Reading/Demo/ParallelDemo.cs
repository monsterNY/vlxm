using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reading.Demo
{
  /// <summary>
  /// @desc : ParallelDemo  
  /// @author : mons
  /// @create : 2019/6/20 14:12:08 
  /// @source : clr via C# p632
  /// </summary>
  public class ParallelDemo
  {
    /// <summary>
    /// 计算一个目录中所有文件的字节长度
    /// </summary>
    /// <param name="path"></param>
    /// <param name="searchPattern"></param>
    /// <param name="searchOption"></param>
    /// <returns></returns>
    public static long DirectoryBytes(string path, string searchPattern, SearchOption searchOption)
    {
      var files = Directory.EnumerateFiles(path, searchPattern, searchOption);

      long masterTotal = 0;

      var parallelLoopResult = Parallel.ForEach<string, long>(files,
        (() =>
        {
          //localInit:每个任务开始之前调用一次

          //每个任务开始之前，总计数都初始化为0
          return 0; //将taskLocalTotal初始值设为0
        }),
        ((file, loopState, index, taskLocalTotal) =>
        {
          //body: 每个工作项调用一次

          long fileLenght = 0;

          FileStream fs = null;

          try
          {
            fs = File.OpenRead(file);
            fileLenght = fs.Length;
          }
          catch (IOException e)
          {
            /* 忽略拒绝访问的任何文件 */
          }
          finally
          {
            if (fs != null) fs.Dispose();
          }

          return taskLocalTotal + fileLenght;
        }),
        (taskLocalTotal =>
        {
          //localFinally: 每个任务完成时调用一次，即使body引发一个未处理的异常，也会调用
          Interlocked.Add(ref masterTotal, taskLocalTotal);
        })
      );

      //任务区别与任务项

      return masterTotal;
    }
  }
}