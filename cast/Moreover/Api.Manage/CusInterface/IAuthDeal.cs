using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Manage.Assist.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Model.Common.ConfigModels;
using NLog;
using ILogger = NLog.ILogger;

namespace Api.Manage.CusInterface
{
  interface IAuthDeal
  {
    Task<ResultModel> Run(AcceptParam acceptParam, AppSetting appSetting, HttpContext context,long userId);
  }
}