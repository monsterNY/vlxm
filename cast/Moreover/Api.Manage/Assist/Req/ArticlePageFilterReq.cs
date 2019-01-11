using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Manage.Assist.Req
{
  public class ArticlePageFilterReq:FilterReq
  {

    public int? ArticleType { get; set; }

    /// <summary>
    /// 筛选类型 0 -- 全部  1 -- 仅自己  2--
    /// </summary>
    public int FilterType { get; set; }

  }
}
