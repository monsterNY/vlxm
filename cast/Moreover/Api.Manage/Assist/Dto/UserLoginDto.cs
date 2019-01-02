using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Manage.Assist.Dto
{
  public class UserLoginDto
  {
    public string UserName { get; set; }

    public string LoginPwd { get; set; }

    public string ValidInfo()
    {
      if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(LoginPwd)) return "参数不匹配！";

      return string.Empty;
    }
  }
}