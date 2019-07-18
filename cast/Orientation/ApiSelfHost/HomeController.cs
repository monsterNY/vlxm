using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ApiSelfHost
{
  /// <summary>
  /// @desc : HomeController  
  /// @author : mons
  /// @create : 2019/7/18 11:02:49 
  /// @source : 
  /// </summary>
  [RoutePrefix("empty")]
  public class HomeController : ApiController
  {
    [HttpPatch, Route("")]
    public string Index()
    {
      return "test";
    }
  }
}