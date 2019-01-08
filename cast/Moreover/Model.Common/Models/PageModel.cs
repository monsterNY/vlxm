using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Common.Models
{
  public class PageModel<T>
  {
    public int PageSize { get; set; } = 10;
    public int PageNo { get; set; } = 1;

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