using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateCode.Tools
{
  /// <summary>
  /// @desc : FileTools  
  /// @author :mons
  /// @create : 2019/3/19 15:08:34 
  /// @source : 
  /// </summary>
  public class FileTools
  {
    /// <summary>
    /// 写入文件
    /// </summary>
    /// <param name="folder"></param>
    /// <param name="fileName"></param>
    /// <param name="context"></param>
    public static async void InFile(string folder, string fileName, string context)
    {
      FileInfo fi = new FileInfo($"{folder}/{fileName}");

      if (!Directory.Exists(folder))
        Directory.CreateDirectory(folder);

      using (StreamWriter sw = new StreamWriter($"{folder}/{fileName}", false))
      {
        await sw.WriteAsync(context);
      }
    }
  }
}