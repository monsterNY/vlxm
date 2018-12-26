using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Manage.Assist.Dto
{
  public class CreateArticleDto
  {
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 作者
    /// </summary>
    public string Author { get; set; }

    /// <summary>
    /// 文章类型
    /// </summary>
    public long ArticleType { get; set; }

    /// <summary>
    /// 是否立即发布
    /// </summary>
    public bool Notice { get; set; }

    public string ValidInfo()
    {
      if (string.IsNullOrWhiteSpace(Title))
      {
        return "文章标题不能为空！";
      }

      if (string.IsNullOrWhiteSpace(Author))
      {
        return "文章作者不能为空！";
      }

      if (ArticleType <= 0)
      {
        return "请选择文章类型！";
      }

      return String.Empty;
    }
  }
}