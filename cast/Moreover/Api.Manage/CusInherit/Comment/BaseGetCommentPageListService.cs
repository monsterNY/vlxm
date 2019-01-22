using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Api.Manage.Assist.Dto;
using Api.Manage.Assist.Entity;
using Api.Manage.Assist.Extension;
using Api.Manage.CusInterface;
using DapperContext;
using DapperContext.Const;
using Microsoft.AspNetCore.Http;
using Model.Common.ConfigModels;
using Model.Common.Models;
using Model.Vlxm.CusAttr;
using Model.Vlxm.Entity;
using Model.Vlxm.Menu;
using Model.Vlxm.Tools;

namespace Api.Manage.CusInherit.Comment
{
  public abstract class BaseGetCommentPageListService : IDeal
  {
    public abstract IEnumerable<string> GetWhereEnumerable(AcceptParam acceptParam, out PageModel<object> page,
      out string msg);

    public abstract IEnumerable<string> GetFieldEnumerable(TableMapperAttribute tableMapper);

    public abstract IEnumerable<string> GetOrderEnumerable(TableMapperAttribute tableMapper);

    public async Task<ResultModel> Run(AcceptParam acceptParam, AppSetting appSetting, HttpContext context)
    {
      var whereEnumerable = GetWhereEnumerable(acceptParam, out var page, out var msg);

      if (msg != string.Empty) return ResultModel.GetParamErrorModel(msg);

      var conn = appSetting.GetMysqlConn(context);

      var tableMapper = typeof(CommentInfo).GetCustomAttribute<TableMapperAttribute>();

      var fieldList = GetFieldEnumerable(tableMapper).ToList();

      var orderEnumerable = GetOrderEnumerable(tableMapper);

      #region 查询用户信息

      fieldList.Add($@"
(
  {SqlCharConst.SELECT} {EntityTools.GetField<UserInfo>(nameof(UserInfo.DisplayName))}
  {SqlCharConst.FROM} {EntityTools.GetTableName<UserInfo>()}
  {SqlCharConst.WHERE} {nameof(BaseModel.Id)} = {tableMapper.Alias}.{EntityTools.GetField<CommentInfo>(nameof(CommentInfo.ActionUser))}
)
 {SqlCharConst.AS} {EntityTools.GetField<CommentDto>(nameof(CommentDto.NickName))}
"); //查询用户名

      fieldList.Add($@"
(
  {SqlCharConst.SELECT} {EntityTools.GetField<UserInfo>(nameof(UserInfo.FaceImg))}
  {SqlCharConst.FROM} {EntityTools.GetTableName<UserInfo>()}
  {SqlCharConst.WHERE} {nameof(BaseModel.Id)} = {tableMapper.Alias}.{EntityTools.GetField<CommentInfo>(nameof(CommentInfo.ActionUser))}
)
 {SqlCharConst.AS} {EntityTools.GetField<CommentDto>(nameof(CommentDto.FaceImg))}
"); //查询用户头像

      #endregion

      var pageList = await DapperTools.GetPageList<CommentDto>(page.PageNo, page.PageSize, conn, tableMapper.TableName,
        whereEnumerable, fieldList, orderEnumerable, tableMapper.Alias, page.Result);

      return ResultModel.GetSuccessModel(pageList);
    }
  }
}