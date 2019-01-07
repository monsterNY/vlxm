using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Manage.Assist.Req
{
  public class CreateUserInfoReq
  {

    public string UserName { get; set; }

    public string DisplayName { get; set; }

    public string LoginPwd { get; set; }

    public string RoleCode { get; set; }

    public string Email { get; set; }

    public int LevelId { get; set; }

    public string Channel { get; set; }

    public string ValidInfo()
    {

      if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(DisplayName) ||
          string.IsNullOrWhiteSpace(LoginPwd)
          || string.IsNullOrWhiteSpace(Email))
      {
        return "用户基本信息不完整！";
      }

      if (LoginPwd.Length < 6 || LoginPwd.Length > 16)
      {
        return $"密码长度为{6}至{16}位";
      }

      if (!string.IsNullOrWhiteSpace(Channel))
      {
        //渠道不为空 则获取相应权限码
      }
      else
      {
        Channel = "default";
        RoleCode = "";
        LevelId = 1;//默认级别
      }

      //密码加密

      return string.Empty;

    }

  }
}
