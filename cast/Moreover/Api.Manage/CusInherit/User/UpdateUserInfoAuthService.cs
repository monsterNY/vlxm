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
using Microsoft.AspNetCore.Http;
using Model.Common.ConfigModels;
using Model.Vlxm.Entity;
using Model.Vlxm.Tools;

namespace Api.Manage.CusInherit.User
{
  public class UpdateUserInfoAuthService : IAuthDeal
  {
    public async Task<ResultModel> Run(AcceptParam acceptParam, AppSetting appSetting, HttpContext context, long userId)
    {
      var updateUserDto = acceptParam.AnalyzeParam<UpdateUserDto>();

      if (updateUserDto == null) return ResultModel.GetNullErrorModel();

      var msg = updateUserDto.ValidInfo();

      if (msg != string.Empty) return ResultModel.GetParamErrorModel(msg);

      var dbConnection = appSetting.GetMysqlConn(context);

      var result = await DapperTools.Edit(dbConnection, EntityTools.GetTableName<UserInfo>(), new List<string>()
      {
        $"{nameof(BaseModel.Id)} = {userId}"
      }, updateUserDto);

//      var result = await dbConnection.ExecuteAsync($@"
//{SqlCharConst.UPDATE} {EntityTools.GetTableName<UserInfo>()}
//{SqlCharConst.SET} {string.Join(",", updateUserDto.GetType().GetProperties().Select(u => $"{u.Name} = @{u.Name}"))}
//{SqlCharConst.WHERE} {nameof(BaseModel.Id)} = {userId}
//",updateUserDto);

      return ResultModel.GetSuccessModel(result);

    }
  }
}