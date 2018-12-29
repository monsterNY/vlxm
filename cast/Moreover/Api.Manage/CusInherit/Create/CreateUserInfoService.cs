using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Manage.Assist.Dto;
using Api.Manage.Assist.Entity;
using Api.Manage.Assist.Extension;
using Api.Manage.CusInterface;
using Dapper;
using DapperContext;
using DapperContext.Const;
using DapperExtensions;
using Microsoft.AspNetCore.Http;
using Model.Common.ConfigModels;
using Model.Vlxm.Entity;
using Model.Vlxm.Tools;

namespace Api.Manage.CusInherit.Create
{
  public class CreateUserInfoService:IDeal
  {
    public async Task<ResultModel> Run(AcceptParam acceptParam, AppSetting appSetting, HttpContext context)
    {
      var createUserInfoDto = acceptParam.AnalyzeParam<CreateUserInfoDto>();

      string msg;

      if (createUserInfoDto == null)
      {
        return ResultModel.GetNullErrorModel(string.Empty);
      }

      if ((msg = createUserInfoDto.ValidInfo()) != string.Empty)
      {
        return ResultModel.GetParamErrorModel(string.Empty,msg);
      }

      var conn = appSetting.GetMysqlConn(context);

      var userNameField = EntityTools.GetField<UserInfo>(nameof(UserInfo.UserName));

      //查看用户名是否已存在
      var isExists = await DapperTools.IsExists(conn, EntityTools.GetTableName<UserInfo>(), new List<string>()
      {
        $"{userNameField} = @{ userNameField}"
      });

      if (isExists)
      {
        return ResultModel.GetParamErrorModel(string.Empty,"用户名已存在！");
      }

//      Predicates.Field<UserInfo>(u => u.Id > 0, Operator.Eq, true);
//      Predicates.Exists<>()

      var result = await DapperTools.CreateItem(conn, EntityTools.GetTableName<UserInfo>(), createUserInfoDto);

      return ResultModel.GetSuccessModel(string.Empty,result);
    }
  }
}
