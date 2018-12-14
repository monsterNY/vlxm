using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace MySpringCore.AOP.Proxy
{

  /// <summary>
  ///
  /// 《静态代理》
  /// 
  /// 通过代理类
  /// 重新一个方法,指定到需要使用的特定方法
  /// 使用时,无须考虑使用物的复杂性 -- 代理中,只存在需要使用的特定方法或成员
  /// 使用时,无须考虑使用物的局限性 -- 代理中,使用物的局限性可在代理中进行特殊处理
  /// ....
  /// </summary>
  public class ProxyImage:IMedia
  {

    private String filename;
    private IMedia image;

    public ProxyImage(String filename)
    {
      this.filename = filename;
    }

    /// <summary>
    /// 需要应用的方法
    /// </summary>
    public void Display()
    {
      if (image == null)
        image = new RealImage(filename);
      image.Display();
    }

    /// <summary>
    /// 不需要使用的方法
    /// </summary>
    public void OtherMethod()
    {
      Console.WriteLine(nameof(OtherMethod));
    }

  }
}
