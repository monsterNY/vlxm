using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Manage.Assist.Req;

namespace Api.Manage.Assist.Param
{
  public class CreateAttentionParam
  {
    public long UserId { get; set; }
    public long AttentionUser { get; set; }
    public string GroupKey { get; set; }
    public string Description { get; set; }

    public static explicit operator CreateAttentionParam(AttentionUserReq req)
    {
      return new CreateAttentionParam()
      {
        AttentionUser = req.AttentionUser,
        Description = req.Description,
        GroupKey = req.GroupKey
      };
    }

  }
}
