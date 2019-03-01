using System;
using System.Collections.Generic;
using System.Text;
using Oop.CusInterface;

namespace Oop.Single
{

  /// <summary>
  /// 一个错误的用户业务示例
  /// </summary>
  public class ErrorUserBiz:IUserBiz
  {
    public int Register(string userName, string displayName, string loginPwd)
    {
      Console.WriteLine("注册了一个用户");

      var userId = new Random().Next(10000);

      //涉及账户钱包
      Console.WriteLine("为用户创建账户信息");
      
      //采用自动登录
      Console.WriteLine("将注册的结果作为登录信息进行缓存");

      return userId;
    }

    public int Login(string name, string pwd)
    {
      Console.WriteLine("进行登录校验，并返回登录结果");

      //涉及好友通讯
      Console.WriteLine("查看关注我的好友/粉丝");

      Console.WriteLine("给好友发送通知:xxx已上线");

      return new Random().Next(2);
    }

    public bool EditInfo(int id, string viaSrc, string name, string description)
    {
      Console.WriteLine("修改用户信息成功！");

      //涉及缓存机制
      Console.WriteLine("用户信息更改，刷新在线缓存！");

      return true;
    }

    public bool EditPwd(long id, string oldPwd, string pwd)
    {
      Console.WriteLine("修改密码成功！");

      //涉及登录缓存
      Console.WriteLine("用户修改密码，清除登录信息，重新登录！");

      return true;
    }

    //...其他业务

    /**
     * 从此可以看出，用户业务内部又牵扯了许多其他业务，在维护或更改用户业务的同时也需要考虑到其他的业务变更
     *
     * 积少成多，或许以后就只有上帝知道要怎么弄了。。。
     *
     */


  }
}
