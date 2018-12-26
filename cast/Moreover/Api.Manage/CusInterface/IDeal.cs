using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Manage.Assist.Entity;
using Microsoft.AspNetCore.Http;
using Model.Common.ConfigModels;

namespace Api.Manage.CusInterface
{
  interface IDeal
  {
    Task<ResultModel> Run(AcceptParam acceptParam, AppSetting appSetting, HttpContext context);
  }
}