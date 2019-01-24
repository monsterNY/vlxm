using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Manage.Assist.Middle;
using Newtonsoft.Json;

namespace Api.Manage.Assist.Dto
{
  public class ArticleSingleDto
  {

    public string Title { get; set; }
    public string Author { get; set; }

    [JsonConverter(typeof(SplitConvert))]
    public string Category { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
    public string FaceImg { get; set; }
    public int ArticleType { get; set; }
    public int Status { get; set; }

    public DateTime? PublishTime { get; set; }
    public int PageView { get; set; }

  }
}
