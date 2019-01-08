using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Manage.Assist.Req;
using Model.Vlxm.Menu;

namespace Api.Manage.Assist.Param
{
  public class CreateCommentParam
  {

    public string Content { get; set; }
    public CommentTypeMenu CommentType { get; set; }
    public int ContentType { get; set; }
    public int JoinKey { get; set; }
    public long ReplayId { get; set; }
    public long ActionUser { get; set; }
    public int Grade { get; set; }

    public static explicit operator CreateCommentParam(CreateArticleCommentReq req)
    {

      return new CreateCommentParam()
      {
        Content = req.Content,
        ReplayId = req.ReplayId,
        CommentType = CommentTypeMenu.Article,
        Grade = req.Grade,
        JoinKey = req.JoinKey,
        ContentType = req.ContentType,
      };

    }

  }
}
