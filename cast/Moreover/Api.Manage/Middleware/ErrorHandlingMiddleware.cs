using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Manage.Assist.Entity;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;

namespace Api.Manage.Middleware
{
  public class ErrorHandlingMiddleware
  {
    protected ILogger logger = LogManager.GetCurrentClassLogger();

    protected JsonSerializerSettings jsonSetting = new JsonSerializerSettings()
    {
      ContractResolver = new CamelCasePropertyNamesContractResolver()
    };

    private readonly RequestDelegate next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
      this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
      try
      {
        await next(context);
      }
      catch (Exception ex)
      {
        logger.Error(ex);
        var statusCode = context.Response.StatusCode;
        if (ex is ArgumentException)
        {
          statusCode = 200;
        }

        await HandleExceptionAsync(context, statusCode, ex.Message);
      }
      finally
      {
        var statusCode = context.Response.StatusCode;
        var msg = "";
        if (statusCode == 401)
        {
          msg = "未授权";
        }
        else if (statusCode == 404)
        {
          msg = "未找到服务";
        }
        else if (statusCode == 502)
        {
          msg = "请求错误";
        }
        else if (statusCode != 200)
        {
          msg = "未知错误";
        }
        //304 允许资源缓存
        if (statusCode != 304 && !string.IsNullOrWhiteSpace(msg))
        {
          await HandleExceptionAsync(context, statusCode, msg);
        }
      }
    }

    private Task HandleExceptionAsync(HttpContext context, int statusCode, string msg)
    {
//      var data = new { code = statusCode.ToString(), is_success = false, msg = msg };

      var data = new ResultModel()
      {
        ErrorCode = statusCode,
        Message = msg,
      };

      var result = JsonConvert.SerializeObject(data, jsonSetting);
      context.Response.ContentType = "application/json;charset=utf-8";
      context.Response.StatusCode = statusCode;
      return context.Response.WriteAsync(result);
    }
  }
}