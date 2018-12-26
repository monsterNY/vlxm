using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Manage.Assist.CusAttribute
{
  /// <summary>
  /// 执行拦截
  /// </summary>
  public class NotFoundActionFilterAttribute : ActionFilterAttribute
  {
    public override void OnActionExecuted(ActionExecutedContext context)
    {
      if (context.Result is ObjectResult objectResult && objectResult.Value == null)
      {
        context.Result = new NotFoundResult();
      }
    }
  }

  /// <summary>
  /// 结果集拦截
  /// </summary>
  public class NotFoundResultFilterAttribute : ResultFilterAttribute
  {
    public override void OnResultExecuting(ResultExecutingContext context)
    {
      if (context.Result is ObjectResult objectResult && objectResult.Value == null)
      {
        context.Result = new NotFoundResult();
      }
    }
  }
}