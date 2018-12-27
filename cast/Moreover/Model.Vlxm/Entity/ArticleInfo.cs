using System;
using Model.Vlxm.CusAttr;

namespace Model.Vlxm.Entity
{
  /// <summary>
  /// 文章信息
  /// </summary>
  [TableName("article_info")]
  public class ArticleInfo : BaseModel
  {
    public string Title { get; set; }

    public long UserId { get; set; }

    public string Author { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
    public string FaceImg { get; set; }

    public int Status { get; set; }

    public int ArticleType { get; set; }

    public DateTime? PublishTime { get; set; }
  }
}