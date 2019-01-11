using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model.Vlxm.CusAttr;
using Model.Vlxm.Entity;
using MySqlX.XDevAPI.Relational;

namespace Api.Manage.Assist.Dto
{
  public class CommentDto:CommentInfo
  {
    public string NickName { get; set; }

    public string FaceImg { get; set; }

    public string ReplyCount { get; set; }

  }
}
