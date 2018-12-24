using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Api.Manage.Assist.Entity;
using Api.Manage.Assist.Menu;
using Api.Manage.CusInterface;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Model.Article.Entity;
using Model.Common.ConfigModels;
using Model.Common.CusAttr;
using Model.Common.Extension;
using MySql.Data.MySqlClient;

namespace Api.Manage.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class HomeController : ControllerBase
  {
    protected AppSetting AppSetting { get; set; }

    public HomeController(IOptionsMonitor<AppSetting> optionsMonitor)
    {
      AppSetting = optionsMonitor.CurrentValue;
    }

    /// <summary>
    /// 统一请求地址
    /// </summary>
    /// <param name="acceptParam"> detail in <see cref="AcceptParam"/> </param>
    /// <returns></returns>
    [Route("")]
    [HttpPost]
    public async Task<object> Index([FromBody] AcceptParam acceptParam)
    {
      try
      {
        if (Enum.TryParse(acceptParam.OperationFlag, true, out OperationMenu operationMenu))
        {
          var dealAttribute = operationMenu.GetAttribute<DealAttribute>();

          if (dealAttribute == null)
            return operationMenu.ToString();

          if (dealAttribute.NeedValidSign)
            if (acceptParam.Param != null || !ValidSign(acceptParam))
              return "验签失败！";

          var instance = Activator.CreateInstance(dealAttribute.DealService);

          if (instance is IDeal deal)
            return await Run(acceptParam, AppSetting, deal.Run);
        }

        return acceptParam;
      }
      catch (Exception e)
      {
        return e;
      }
    }

    /// <summary>
    /// 执行操作
    /// </summary>
    /// <param name="acceptParam"></param>
    /// <param name="appSetting"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    [NonAction]
    public async Task<object> Run(AcceptParam acceptParam, AppSetting appSetting,
      Func<AcceptParam, AppSetting, Task<object>> func)
    {
      return await func.Invoke(acceptParam, appSetting);
    }

    /// <summary>
    /// 验证签名
    /// </summary>
    /// <returns></returns>
    public bool ValidSign(AcceptParam acceptParam)
    {
      return true;
    }
  }
}