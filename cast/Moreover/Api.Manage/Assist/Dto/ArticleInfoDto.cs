using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model.Vlxm.Entity;

namespace Api.Manage.Assist.Dto
{
  public class ArticleInfoDto : ArticleInfo
  {

    /// <summary>
    /// 评论数量
    /// </summary>
    public int CommentCount { get; set; }

    /// <summary>
    /// 点赞数量
    /// </summary>
    public int LikeCount { get; set; }

  }
}
