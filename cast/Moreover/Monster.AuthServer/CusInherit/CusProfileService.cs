using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Dapper;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Test;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Model.Article.Entity;
using Model.Common.ConfigModels;
using MySql.Data.MySqlClient;

namespace Monster.AuthServer.CusInherit
{
  public class CusProfileService : IProfileService
  {
    /// <summary>
    /// The logger
    /// </summary>
    protected readonly ILogger Logger;

    protected AppSetting AppSetting { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TestUserProfileService"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="optionsMonitor">The config.</param>
    public CusProfileService(ILogger<TestUserProfileService> logger,
      IOptionsMonitor<AppSetting> optionsMonitor)
    {
      Logger = logger;
      AppSetting = optionsMonitor.CurrentValue;
    }

    /// <summary>
    /// 只要有关用户的身份信息单元被请求（例如在令牌创建期间或通过用户信息终点），就会调用此方法
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    public virtual Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
      context.LogProfileRequest(Logger);

      //判断是否有请求Claim信息
      if (context.RequestedClaimTypes.Any())
      {
        using (IDbConnection conn = new MySqlConnection(AppSetting.ConnectionString["Mysql"]))
        {
          //根据用户唯一标识查找用户信息
          var user = conn.QueryFirst<UserInfo>(
            $"select * from article_info WHERE {nameof(UserInfo.RoleCode)} = {context.Subject.GetSubjectId()}");

          if (user != null)
          {
            //调用此方法以后内部会进行过滤，只将用户请求的Claim加入到 context.IssuedClaims 集合中 这样我们的请求方便能正常获取到所需Claim

            context.AddRequestedClaims(new[]
            {
              new Claim(JwtClaimTypes.Name, user.DisplayName),
            });
          }
        }
      }

      context.LogIssuedClaims(Logger);

      return Task.CompletedTask;
    }

    /// <summary>
    /// 验证用户是否有效 例如：token创建或者验证
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    public virtual Task IsActiveAsync(IsActiveContext context)
    {
      Logger.LogDebug("IsActive called from: {caller}", context.Caller);
      using (IDbConnection conn = new MySqlConnection(AppSetting.ConnectionString["Mysql"]))
      {
        var user = conn.QueryFirst<UserInfo>(
          $"select * from article_info WHERE {nameof(UserInfo.RoleCode)} = {context.Subject.GetSubjectId()}");
        context.IsActive = user.ValidFlag == 1;
      }

      return Task.CompletedTask;
    }
  }
}