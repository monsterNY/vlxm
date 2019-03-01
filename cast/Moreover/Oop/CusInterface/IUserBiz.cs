using System;
using System.Collections.Generic;
using System.Text;

namespace Oop.CusInterface
{

  /// <summary>
  /// 一个处理用户的业务
  /// </summary>
  public interface IUserBiz
  {

    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="displayName"></param>
    /// <param name="loginPwd"></param>
    /// <returns></returns>
    int Register(string userName,string displayName,string loginPwd);

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="name"></param>
    /// <param name="pwd"></param>
    /// <returns></returns>
    int Login(string name, string pwd);

    /// <summary>
    /// 修改用户信息
    /// </summary>
    /// <param name="id"></param>
    /// <param name="viaSrc"></param>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    bool EditInfo(int id,string viaSrc, string name, string description);

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="id"></param>
    /// <param name="oldPwd"></param>
    /// <param name="pwd"></param>
    /// <returns></returns>
    bool EditPwd(long id, string oldPwd, string pwd);

  }
}
