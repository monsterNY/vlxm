using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Article.Entity
{
  public class UserInfo : BaseModel
  {
    public string UserName { get; set; }
    public string DisplayName { get; set; }
    public string LoginPwd { get; set; }
    public string Channel { get; set; }
    public string RoleCode { get; set; }
  }
}