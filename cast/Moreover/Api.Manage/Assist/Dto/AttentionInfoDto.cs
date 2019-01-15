using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model.Vlxm.CusAttr;

namespace Api.Manage.Assist.Dto
{
  public class AttentionInfoDto
  {

    public long Id { get; set; }
    public long UserId { get; set; }
    public long AttentionUser { get; set; }
    public string GroupKey { get; set; }
    public string Description { get; set; }
    public UserInfoDto UserInfo { get; set; }

  }
}
