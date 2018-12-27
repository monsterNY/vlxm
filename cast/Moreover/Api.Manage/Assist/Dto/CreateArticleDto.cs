using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Manage.Assist.Dto
{
  public class CreateArticleDto
  {
    public string Title { get; set; }

    public string Author { get; set; }

    public string[] Category { get; set; }

    public string Description { get; set; }

    public string Content { get; set; }

    public string FaceImg { get; set; }

    public int Status { get; set; }

    public DateTime PublishTime { get; set; }

    public int ArticleType { get; set; }

    public string ValidInfo()
    {
      if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Author) || string.IsNullOrWhiteSpace(FaceImg))
      {
        return "文章基本信息录入不完整！";
      }

      if (Category == null || Category.Length == 0)
      {
        return "未选择文章标签！";
      }

      if (ArticleType <= 0)
      {
        return "未选择文章类目！";
      }

      return String.Empty;
    }
  }
}