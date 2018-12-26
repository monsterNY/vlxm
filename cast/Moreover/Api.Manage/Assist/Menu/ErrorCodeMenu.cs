using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Manage.Assist.Menu
{

  /// <summary>
  /// 常用错误码
  /// </summary>
  public enum ErrorCodeMenu
  {
    /// <summary>
    /// 无异常
    /// </summary>
    ERROR_NONE = 0,

    /// <summary>
    /// 参数为空异常
    /// </summary>
    ERROR_NULL = 1,

    /// <summary>
    /// 参数传递有误异常
    /// </summary>
    ERROR_PARAM = 2,

    /// <summary>
    /// 处理异常
    /// </summary>
    ERROR_DEAL = 3,

    /// <summary>
    /// 用户未登录
    /// </summary>
    ERROR_NONLOGIN = 4,

    /// <summary>
    /// 签名信息有误
    /// </summary>
    ERROR_SIGN = 5,
  }
}