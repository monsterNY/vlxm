using System;
using Api.Manage.Assist.Req;
using Model.Vlxm.Menu;

namespace Api.Manage.Assist.Param
{
  public class CreateArticleParam
  {
    public long UserId { get; set; }
    public string Title { get; set; }

    public string Author { get; set; }

    public string Category { get; set; }

    public string Description { get; set; }

    public string Content { get; set; }

    public string FaceImg { get; set; }

    public int Status { get; set; }

    public int ArticleType { get; set; }

    public DateTime PublishTime { get; set; }

    public static explicit operator CreateArticleParam(CreateArticleReq dto)
    {
      var param = new CreateArticleParam()
      {
        Title = dto.Title,
        Author = dto.Author,
        Category = string.Join(",",dto.Category),
        Description = dto.Description,
        Content = dto.Content,
        FaceImg = dto.FaceImg,
        Status = dto.Status,
        ArticleType = dto.ArticleType,
        PublishTime = dto.PublishTime
      };
      
      return param;
    }
  }
}