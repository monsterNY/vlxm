using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Api.Manage.Assist.Dto;
using Api.Manage.Assist.Entity;
using Api.Manage.Assist.Extension;
using Api.Manage.Assist.Req;
using Api.Manage.CusInterface;
using Dapper;
using DapperContext;
using DapperContext.Const;
using Microsoft.AspNetCore.Http;
using Model.Common.ConfigModels;
using Model.Common.Models;
using Model.Vlxm.CusAttr;
using Model.Vlxm.Entity;
using Model.Vlxm.Menu;
using Model.Vlxm.Tools;
using NLog;

namespace Api.Manage.CusInherit.Attention
{
  public class GetAttentionPageListAuthService : IAuthDeal
  {
    protected ILogger Logger = LogManager.GetCurrentClassLogger();

    public async Task<ResultModel> Run(AcceptParam acceptParam, AppSetting appSetting, HttpContext context, long userId)
    {
      var req = acceptParam.AnalyzeParam<PageModel<AttentionPageFilterReq>>();

      if (req == null || req.PageNo == 0 || req.Result == null)
      {
        return ResultModel.GetParamErrorModel();
      }

      var conn = appSetting.GetMysqlConn(context);
      var attentionMapper = typeof(AttentionInfo).GetCustomAttribute<TableMapperAttribute>();
      var userMapper = typeof(UserInfo).GetCustomAttribute<TableMapperAttribute>();

      var joinRelation = string.Empty;

      var whereList = new List<string>()
      {
        $"{attentionMapper.Alias}.{nameof(BaseModel.ValidFlag)}={(int) ValidFlagMenu.UseFul}"
      };

      if (req.Result.FilterType == 1)
      {
        whereList.Add(
          $"{attentionMapper.Alias}.{EntityTools.GetField<AttentionInfo>(nameof(AttentionInfo.AttentionUser))}={userId}");
        joinRelation = $"{attentionMapper.Alias}.userId = {userMapper.Alias}.id";
      }
      else
      {
        whereList.Add(
          $"{attentionMapper.Alias}.{EntityTools.GetField<AttentionInfo>(nameof(AttentionInfo.UserId))}={userId}");
        joinRelation = $"{attentionMapper.Alias}.attentionUser = {userMapper.Alias}.id";
      }

      var whereSql = DapperTools.GetWhereSql(whereList);

      var selectCountSql = $@"
{SqlCharConst.SELECT} {SqlCharConst.COUNT}(1) 
{SqlCharConst.FROM} {attentionMapper.TableName} {SqlCharConst.AS} {attentionMapper.Alias}
{whereSql}
";

      Logger.Debug(selectCountSql);

      var count = await conn.QueryFirstAsync<int>(selectCountSql);

      var resultPage = new PageModel<IEnumerable<AttentionInfoDto>>()
      {
        Count = count,
        PageNo = req.PageNo,
        PageSize = req.PageSize
      };

      if ((req.PageNo - 1) * req.PageSize <= count)
      {
        var fieldList = new List<string>();
        fieldList.AddRange(EntityTools.GetFields<AttentionInfoDto>($"{attentionMapper.Alias}."
          , null
          , (propertyInfo => propertyInfo.Name != nameof(AttentionInfoDto.UserInfo))));
        fieldList.AddRange(EntityTools.GetFields<UserInfoDto>($"{userMapper.Alias}."));

        var pageSql = $@"
{SqlCharConst.SELECT} 
	{string.Join(',', fieldList)}

{SqlCharConst.FROM} 
  {attentionMapper.TableName} {SqlCharConst.AS} {attentionMapper.Alias}
	{SqlCharConst.INNER} {SqlCharConst.JOIN} 
    {userMapper.TableName} {SqlCharConst.AS} {userMapper.Alias}
 	  {SqlCharConst.ON} {joinRelation}

{DapperTools.GetWhereSql(whereList)}

{SqlCharConst.ORDERBY}

  {attentionMapper.Alias}.{nameof(BaseModel.UpdateTime)} {SqlCharConst.DESC},
  {attentionMapper.Alias}.{nameof(BaseModel.CreateTime)} {SqlCharConst.DESC}

{SqlCharConst.LIMIT} {(req.PageNo - 1) * req.PageSize},{req.PageSize}
";

        Logger.Debug(pageSql);

        var info = await conn.QueryAsync<AttentionInfoDto, UserInfoDto, AttentionInfoDto>(pageSql,
          (attention, user) =>
          {
            attention.UserInfo = user;
            return attention;
          }
          //The field we should split and read the second object from (default: "Id")
          //当select中的列出现 不同表的重复列名 需要使用在此配置
          //,splitOn: "Name,BookID"
        );

        resultPage.Result = info.ToList();

//        return ResultModel.GetSuccessModel(info);
      }
      //      var result = await DapperTools.GetPageList<AttentionInfo>(req.PageNo, req.PageSize, conn, whereList);

      //      return ResultModel.GetSuccessModel(result);

      return ResultModel.GetSuccessModel(resultPage);
    }
  }
}