using System;
using System.Threading.Tasks;
using Api.Manage.Assist.CusAttribute;
using Api.Manage.Assist.Entity;
using Api.Manage.Assist.Menu;
using Api.Manage.CusInterface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Model.Common.ConfigModels;
using Model.Common.CusAttr;
using Model.Common.Extension;
using NLog;

namespace Api.Manage.Controllers
{
  [EnableCors("AllowCors")]
  [ApiController]
  [Route("api/[controller]")]
  [NotFoundActionFilter]
  public class HomeController : ControllerBase
  {
    protected AppSetting AppSetting { get; set; }

    protected ILogger Logger = LogManager.GetCurrentClassLogger();

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

          var resultModel = await Run(acceptParam, AppSetting, dealAttribute);

          resultModel.Title = dealAttribute.Description;

          return resultModel;
        }
        else
        {
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
    /// <param name="dealAttribute"></param>
    /// <returns></returns>
    [NonAction]
    public async Task<ResultModel> Run(AcceptParam acceptParam, AppSetting appSetting,
      DealAttribute dealAttribute)
    {
      var instance = Activator.CreateInstance(dealAttribute.DealService) as IDeal;
      Logger.Info($"调用:{dealAttribute.DealService.Name}");

      return await instance.Run(acceptParam, appSetting, HttpContext);
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