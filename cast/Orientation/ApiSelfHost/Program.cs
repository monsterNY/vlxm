using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.SelfHost;

namespace ApiSelfHost
{
  class Program:ActionFilterAttribute
  {
    static void Main(string[] args)
    {

      var port = "5050";
      var config = new HttpSelfHostConfiguration(string.Format("http://localhost:{0}", port ?? "8080"));

      //      config.Routes.MapHttpRoute(
      //        "API Default", "api/{controller}/{id}",
      //        new {id = RouteParameter.Optional});

      config.MapHttpAttributeRoutes(); // 支持路由特性设置，与支持http谓词无关

      config.Routes.MapHttpRoute(
        name: "API Default",
        routeTemplate: "{controller}/{id}",
        defaults: new {id = RouteParameter.Optional}
      );

      using (HttpSelfHostServer server = new HttpSelfHostServer(config))
      {
        server.OpenAsync().Wait();
        Console.WriteLine("Press ESC to quit.");
        while (
          !Console.ReadKey().Key.Equals(ConsoleKey.Escape))
        {
        }
      }
    }
  }
}