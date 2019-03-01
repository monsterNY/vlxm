using System;
using System.Collections.Generic;
using System.Text;
using Oop.CusInterface;

namespace Oop.Single
{
  /// <summary>
  /// 
  /// </summary>
  public class UserBiz : IUserBiz
  {
    public int Register(string userName, string displayName, string loginPwd)
    {
      Console.WriteLine("注册了一个用户");
      return new Random().Next(10000);
    }

    public int Login(string name, string pwd)
    {
      Console.WriteLine("进行登录校验，并返回登录结果");
      return new Random().Next(2);
    }

    public bool EditInfo(int id, string viaSrc, string name, string description)
    {
      Console.WriteLine("修改用户信息成功！");
      return true;
    }

    public bool EditPwd(long id, string oldPwd, string pwd)
    {
      Console.WriteLine("修改密码成功！");
      return true;
    }
  }
}