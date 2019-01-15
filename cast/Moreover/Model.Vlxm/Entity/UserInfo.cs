using System;
using System.Collections.Generic;
using System.Text;
using Model.Vlxm.CusAttr;

namespace Model.Vlxm.Entity
{

  [TableMapper("user_info","t_ui",nameof(UserInfo))]
  public class UserInfo:BaseModel
  {

    public string UserName { get; set; }

    public string DisplayName { get; set; }

    public string LoginPwd { get; set; }

    public string RoleCode { get; set; }

    public string Email { get; set; }

    public int LevelId { get; set; }

    public string Channel { get; set; }
    public string Description { get; set; }
    public string FaceImg { get; set; }

  }
}
