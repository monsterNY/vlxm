using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Monster.AuthServer
{
  public class Program
  {
    public static void Main(string[] args)
    {
      NLog.Web.NLogBuilder.ConfigureNLog("config/nLog.config");
      CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
      WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>();
  }
}