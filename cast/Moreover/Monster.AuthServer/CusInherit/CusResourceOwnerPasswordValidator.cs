using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Dapper;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Model.Article.Entity;
using Model.Common.ConfigModels;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace Monster.AuthServer.CusInherit
{
  public class CusResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
  {
    private readonly ISystemClock _clock;
    protected AppSetting AppSetting { get; set; }

    public CusResourceOwnerPasswordValidator(ISystemClock clock, IOptionsMonitor<AppSetting> optionsMonitor)
    {
      _clock = clock;

      AppSetting = optionsMonitor.CurrentValue;
    }

    public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
      UserInfo clientUserInfo = null;

      using (IDbConnection conn = new MySqlConnection(AppSetting.DbConnMap["Mysql"].ConnStr))
      {
        //根据用户唯一标识查找用户信息
        clientUserInfo = conn.QueryFirst<UserInfo>(
          $"select * from article_info WHERE {nameof(UserInfo.UserName)} = {context.UserName} AND {nameof(UserInfo.LoginPwd)} = {context.Password}");
      }

      //此处使用context.UserName, context.Password 用户名和密码来与数据库的数据做校验
      if (clientUserInfo != null)
      {
//                var user = _users.FindByUsername(context.UserName);

        //验证通过返回结果 
        //subjectId 为用户唯一标识 一般为用户id
        //authenticationMethod 描述自定义授权类型的认证方法 
        //authTime 授权时间
        //claims 需要返回的用户身份信息单元 此处应该根据我们从数据库读取到的用户信息 添加Claims 如果是从数据库中读取角色信息，那么我们应该在此处添加

        context.Result = new GrantValidationResult(clientUserInfo.RoleCode,
          OidcConstants.AuthenticationMethods.Password, _clock.UtcNow.UtcDateTime, new[]
          {
            new Claim(JwtClaimTypes.Id, clientUserInfo.Id.ToString()),
          });

        //                context.Result = new GrantValidationResult(
        ////                    user.SubjectId ?? throw new ArgumentException("Subject ID not set", nameof(user.SubjectId)),
        ////                    OidcConstants.AuthenticationMethods.Password, _clock.UtcNow.UtcDateTime,
        ////                    user.Claims
        //                    );
      }
      else
      {
        //验证失败
        context.Result =
          new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid custom credential");
      }

      return Task.CompletedTask;
    }
  }
}