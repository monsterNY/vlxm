using System;
using System.Collections.Generic;
using System.Text;
using Model.Vlxm.CusAttr;

namespace Model.Vlxm.Entity
{
  [TableName("comment_info")]
  public class CommentInfo:BaseModel
  {

    public string Content { get; set; }
    public int CommentType { get; set; }
    public int ContentType { get; set; }
    public int JoinKey { get; set; }
    public long ReplayId { get; set; }
    public long ActionUser { get; set; }
    public int Grade { get; set; }

  }
}
