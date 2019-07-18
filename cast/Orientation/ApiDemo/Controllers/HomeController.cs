using System.Web.Http;
using System.Web.Http.Filters;

namespace ApiDemo.Controllers
{
  /// <summary>
  /// 主页
  /// </summary>
  [RoutePrefix("home")]
  public class HomeController : ApiController
  {
    /// <summary>
    /// 默认测试
    /// </summary>
    /// <param name="obj">对象</param>
    /// <returns></returns>
    [Route(""), HttpGet]
    public object Index([FromBody] object obj)
    {
      return "test";
    }
  }
}