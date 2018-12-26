using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Common.Models
{
  public class PageModel<T>
  {
    public int PageSize { get; set; }
    public int PageNo { get; set; }

    /// <summary>
    /// 总数据行
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// 结果集 -- 用于返回
    /// </summary>
    public T Result { get; set; }
  }
}