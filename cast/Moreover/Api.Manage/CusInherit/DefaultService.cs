using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Manage.Assist.Entity;
using Api.Manage.CusInterface;
using Microsoft.AspNetCore.Http;
using Model.Common.ConfigModels;
using NLog;

namespace Api.Manage.CusInherit
{
  public class DefaultService :IDeal
  {
    public Task<ResultModel> Run(AcceptParam acceptParam, AppSetting appSetting, HttpContext context)
    {
      return Task.Run((() =>
      {

        return ResultModel.GetSuccessModel("测试Api");
        
      }));
    }
  }
}
