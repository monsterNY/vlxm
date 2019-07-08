using System;
using System.Data;
using System.Threading.Tasks;
using Api.Manage.Assist.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Model.Common.ConfigModels;

namespace Api.Manage.Middleware
{
  public class HttpModuleMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly ILogger<HttpModuleMiddleware> _logger;
    private AppSetting AppSetting { get; set; }

    public HttpModuleMiddleware(RequestDelegate next, ILogger<HttpModuleMiddleware> logger,
      IOptionsMonitor<AppSetting> optionsMonitor)
    {
      _next = next;
      _logger = logger;
      AppSetting = optionsMonitor.CurrentValue;
    }

    public async Task Invoke(HttpContext context)
    {
      // Do something with context near the beginning of request processing.

      try
      {
        _logger.LogInformation("开始调用Invoke");
        await _next.Invoke(context);
        _logger.LogInformation("结束调用Invoke");
      }
      catch (Exception e)
      {
        _logger.LogWarning(e, "action 执行异常");
      }
      finally
      {
        var key = AppSetting.GetMysqlConn().FlagKey;
        if (context.Items.ContainsKey(key))
        {
          var dbConnection = context.Items[key] as IDbConnection;

          dbConnection?.Dispose(); //释放资源

          _logger.LogInformation("数据库资源正常释放！");
        }
        else
        {
          _logger.LogInformation("尚未使用数据库资源！");
        }
      }
    }
  }
}