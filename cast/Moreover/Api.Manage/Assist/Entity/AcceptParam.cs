using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NLog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Api.Manage.Assist.Entity
{
  /// <summary>
  /// 请求参数
  /// </summary>
  public class AcceptParam
  {
    protected Logger logger = LogManager.GetLogger(nameof(AcceptParam));

    /// <summary>
    /// 参数对象
    /// </summary>
    protected object ParamObj;

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

    public T AnalyzeParam<T>() where T : class
    {
      if (ParamObj != null)
      {
        return ParamObj as T;
      }

      if (Param == null)
        return default(T);


      var str = JsonConvert.SerializeObject(Param);

      if (string.IsNullOrWhiteSpace(str))
        return default(T);

      ParamObj = JsonConvert.DeserializeObject<T>(str);

      logger.Info($"传入参数：{str}");

      return ParamObj as T;
    }
  }
}