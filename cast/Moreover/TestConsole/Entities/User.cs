using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestConsole.Entities
{
  public class User
  {

    [Required(ErrorMessage = "用户名不能为空")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "登录密码不能为空")]
    public string LoginPwd { get; set; }

  }
}
