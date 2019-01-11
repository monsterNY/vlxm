using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Manage.Assist.Req
{
  public class ArticlePageFilterAuthReq:FilterReq
  {

    /// <summary>
    /// 文章类型
    /// </summary>
    public int ArticleType { get; set; }

    /// <summary>
    /// 筛选类型 1--收藏 2--仅自己
    /// </summary>
    public int FilterType { get; set; }


  }
}
