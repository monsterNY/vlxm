using System;
using System.Collections.Generic;
using System.Text;
using ConsoleApp.Domain.Model;

namespace ConsoleApp.Domain.Model
{

  public class UserInfo:BaseModel
  {

    /// <summary>
    /// 账单哦
    /// </summary>
    public List<BillInfo> BillList { get; set; }

    /// <summary>
    /// 钱包哦
    /// </summary>
    public UserMoney Wallet { get; set; }

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
