using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.DoMain.Command
{

  /// <summary>
  /// 图片压缩参数
  /// </summary>
  public class ReduceParam
  {

    /// <summary>
    /// 返回时的key
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// 存储地址-相对路径
    /// </summary>
    public string SaveUrl { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Width { get; set; }


    /// <summary>
    /// 
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// 压缩方式
    /// 对应一个 <see cref="API.Utility.MakeCompressModes" /> 
    /// </summary>
    public int ReduceMode { get; set; }

  }
}