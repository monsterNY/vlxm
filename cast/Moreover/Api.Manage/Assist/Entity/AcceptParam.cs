using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Manage.Assist.Entity
{
  /// <summary>
  /// 请求参数
  /// </summary>
  public class AcceptParam
  {
    /// <summary>
    /// 版本号
    /// </summary>
    public double Version { get; set; }

    /// <summary>
    /// 操作标识
    /// </summary>
    public string OperationFlag { get; set; }

    /// <summary>
    /// 传递参数
    /// </summary>
    public dynamic Param { get; set; }

    /// <summary>
    /// 签名
    /// </summary>
    public string Sign { get; set; }
    
  }
}
