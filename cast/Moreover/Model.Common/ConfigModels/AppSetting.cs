using System.Collections.Generic;

namespace Model.Common.ConfigModels
{
  public class AppSetting
  {

    /// <summary>
    ///连接字符串
    /// </summary>
    public Dictionary<string,string> ConnectionString { get; set; }

    /// <summary>
    /// 公匙
    /// </summary>
    public string PublicKey { get; set; }

    /// <summary>
    /// 私匙
    /// </summary>
    public string PrivateKey { get; set; }

  }
}
