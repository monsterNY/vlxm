using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Manage.Assist.Req
{
  public class CreateArticleCommentReq
  {
    public string Content { get; set; }
    public int ContentType { get; set; }
    public int JoinKey { get; set; }
    public long ReplayId { get; set; }
    public int Grade { get; set; }

    public string ValidInfo()
    {
      if (string.IsNullOrWhiteSpace(Content))
      {
        return "评论内容不能为空!";
      }

      if (ContentType < 0 || JoinKey <= 0 || Grade <= 0)
      {
        return "参数异常！";
      }

      return string.Empty;

    }

  }
}
