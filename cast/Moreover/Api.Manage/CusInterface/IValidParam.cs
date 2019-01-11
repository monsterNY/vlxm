using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Manage.CusInterface
{
  public interface IValidParam
  {

    /// <summary>
    /// 验证参数
    /// 正常返回：string.Empty
    /// </summary>
    /// <returns></returns>
    string ValidInfo();

  }
}
