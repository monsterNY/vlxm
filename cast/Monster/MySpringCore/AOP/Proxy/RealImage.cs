using System;
using System.Collections.Generic;
using System.Text;

namespace MySpringCore.AOP.Proxy
{
  /// <summary>
  /// 需要使用的东西(数据库、计算机资源...)
  /// 当我们使用这些东西时,需要考虑此物的复杂性,即需要了解这些东西的大概才能使用,还要考虑此物的局限性、等等
  /// </summary>
  public class RealImage : IMedia
  {

    private String filename;
    public RealImage(String filename)
    {
      this.filename = filename;
      LoadImageFromDisk();
    }

    private void LoadImageFromDisk()
    {
      Console.WriteLine("Loading   " + filename);
    }

    public void Display()
    {
      Console.WriteLine("Displaying " + filename);
    }
    
  }
}
