using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Article.Entity
{

  /// <summary>
  /// 文章信息
  /// </summary>
  public class ArticleInfo
  {

    public long Id { get; set; }

    public string Title { get; set; }

    public string Author { get; set; }

    public int Status { get; set; }

    public DateTime? PublishTime { get; set; }

    public DateTime? CreateTime { get; set; }

    public DateTime? UpdateTime { get; set; }

    public int ValidFlag { get; set; }

  }
}
