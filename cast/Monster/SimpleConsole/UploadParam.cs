using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.DoMain.Command
{
  public class UploadParam
  {

    /// <summary>
    /// 文件上传最大大小 (mb)
    /// </summary>
    public int MaxSize { get; set; }

    /// <summary>
    /// 存储地址
    /// </summary>
    public string SaveUrl { get; set; }

    /// <summary>
    /// 返回时的key
    /// </summary>
    public string Key { get; set; }
    /// <summary>
    /// 缩略图配置
    /// </summary>
    public IEnumerable<ReduceParam> ReduceParams { get; set; }

  }
}