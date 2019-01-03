using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Common.ConfigModels
{
  public class AuthorizeParam
  {

    /// <summary>
    /// 验证地址 运行时不可变
    /// </summary>
    public string Url { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string Client { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Secret { get; set; }

  }
}
