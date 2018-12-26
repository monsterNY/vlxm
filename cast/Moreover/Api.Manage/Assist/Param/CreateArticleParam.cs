using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Manage.Assist.Dto;
using Model.Article.CusAttr;
using Model.Article.Menu;

namespace Api.Manage.Assist.Param
{

  public class CreateArticleParam
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
    /// 发布时间
    /// </summary>
    public DateTime? PublishTime { get; set; }
    /// <summary>
    /// 文章状态
    /// </summary>
    public ArticleStatusMenu Status { get; set; }

    public static explicit operator CreateArticleParam(CreateArticleDto dto)
    {

      var param = new CreateArticleParam()
      {
        Title = dto.Title,
        Author = dto.Author,
        ArticleType = dto.ArticleType,
        Status = dto.Notice?ArticleStatusMenu.Publish:ArticleStatusMenu.NoPublish
      };

      if (dto.Notice)
      {
        param.PublishTime = DateTime.Now;
      }

      return param;

    }

  }
}
