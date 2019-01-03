using System.Collections.Generic;

namespace Model.Common.ConfigModels
{
  public class AppSetting
  {
//    /// <summary>
//    ///连接字符串
//    /// </summary>
//    public Dictionary<string,string> ConnectionString { get; set; }

    /// <summary>
    /// db 数据库连接
    /// </summary>
    public Dictionary<string, ConnectionParam> DbConnMap { get; set; }

//    /// <summary>
//    /// 身份验证的地址
//    /// </summary>
//    public string AuthorizeUrl { get; set; }

    /// <summary>
    /// 身份验证
    /// </summary>
    public AuthorizeParam Authorize { get; set; }

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