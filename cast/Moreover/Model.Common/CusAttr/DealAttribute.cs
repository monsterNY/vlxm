using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Common.CusAttr
{
  /// <summary>
  /// 处理标识
  /// </summary>
  public class DealAttribute : Attribute
  {
    public DealAttribute(Type dealService, string description)
    {
      DealService = dealService;
      Description = description;
    }
    
    /// <summary>
    /// 处理Service
    /// </summary>
    public Type DealService { get; set; }

    /// <summary>
    /// 描述信息
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 是否要验证签名
    /// </summary>
    public bool NeedValidSign { get; set; } = false;
  }
}