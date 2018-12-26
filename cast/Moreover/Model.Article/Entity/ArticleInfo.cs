using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Article.Entity
{

  /// <summary>
  /// 文章信息
  /// </summary>
  public class ArticleInfo:BaseModel
  {

    public string Title { get; set; }

    public string Author { get; set; }

    public int Status { get; set; }

    public DateTime? PublishTime { get; set; }

  }
}
