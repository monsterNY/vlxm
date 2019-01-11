using System;
using System.Collections.Generic;
using System.Text;
using Model.Vlxm.CusAttr;

namespace Model.Vlxm.Entity
{
  [TableMapper("comment_info","t_ci",nameof(CommentInfo))]
  public class CommentInfo:BaseModel
  {

    public string Content { get; set; }
    public int CommentType { get; set; }
    public int ContentType { get; set; }
    public int JoinKey { get; set; }
    public long ReplyId { get; set; }
    public long ActionUser { get; set; }
    public int Grade { get; set; }

  }
}
