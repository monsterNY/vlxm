using System;
using System.Collections.Generic;
using System.Text;
using Model.Article.CusAttr;

namespace Model.Article.Entity
{

  /// <summary>
  /// 文章信息
  /// </summary>
  [TableName("article_info")]
  public class ArticleInfo:BaseModel
  {

    public string Title { get; set; }

    public string Author { get; set; }

    public int Status { get; set; }

    public int ArticleType { get; set; }

    public DateTime? PublishTime { get; set; }

  }
}
