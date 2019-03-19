using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Api.Manage.Assist.Utils;
using Command.RedisHelper.CusInhert;
using Command.RedisHelper.Helper;
using DapperContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Model.Common.ConfigModels;
using Model.Vlxm.Entity;
using Model.Vlxm.Tools;
using MySql.Data.MySqlClient;

namespace Api.Manage.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class CommonController : ControllerBase
  {
    public AppSetting AppSetting;

    public CommonController(IOptionsMonitor<AppSetting> optionsMonitor)
    {
      AppSetting = optionsMonitor.CurrentValue;
    }

    /// <summary>
    /// 查看配置
    /// </summary>
    /// <returns></returns>
    [Route(nameof(GetAppSetting))]
    public ActionResult<object> GetAppSetting()
    {
      return AppSetting;
    }

    /// <summary>
    /// 测试授权
    /// </summary>
    /// <returns></returns>
    [Route(nameof(TestAuth))]
    [Authorize]
    public object TestAuth()
    {
      return new JsonResult(from c in User.Claims select new {c.Type, c.Value});
    }

    /// <summary>
    /// 测试db
    /// </summary>
    /// <returns></returns>
    [Route(nameof(TestDb))]
    public async Task<object> TestDb()
    {
      using (IDbConnection conn = new MySqlConnection(AppSetting.DbConnMap["Mysql"].ConnStr))
      {
        var param = new UserInfo()
        {
          UserName = "monster",
          LoginPwd = "monster"
        };

        var whereList = new List<string>()
        {
          $"{nameof(UserInfo.UserName)} = @{nameof(UserInfo.UserName)}",
          $"{nameof(UserInfo.LoginPwd)} = @{nameof(UserInfo.LoginPwd)}"
        };

        //根据用户唯一标识查找用户信息
        var clientUserInfo =
          await DapperTools.GetItem<UserInfo>(conn, EntityTools.GetTableName<UserInfo>(), whereList, param);

        return clientUserInfo;

        //        try
        //        {
        //
        //          var content =
        //            "<p><span style=\"font-size:30px\"><span style=\"font-family:Impact, serif\">🤣</span></span></p><p><span style=\"font-size:30px\"><span style=\"font-family:Impact, serif\">真逗呢</span></span></p><p><span style=\"font-size:30px\"><span style=\"font-family:Impact, serif\">哈哈哈</span></span></p>";
        //          var param = new {content};
        //
        //          var result = conn.Execute($@"INSERT INTO article_info
        //        ( title, author, category, content,articleType)
        //
        //      VALUES( '', '', '', @content,0)",param);
        //          return result;
        //
        //        }
        //        catch (Exception e)
        //        {
        //          return JsonConvert.SerializeObject(e);
        //        }


        //        IEnumerable<ArticleInfo> articleInfos = conn.Query<ArticleInfo>(
        //          $@"
        //{SqlCharConst.SELECT} {string.Join(",", EntityTools.GetFields<ArticleInfo>())} 
        //{SqlCharConst.FROM} {EntityTools.GetTableName<ArticleInfo>()}
        //{SqlCharConst.WHERE} {SqlCharConst.DefaultWhere}
        //");
      }
    }

    #region 验证码

    /// <summary>
    /// 验证码
    /// </summary>
    /// <returns></returns>
    [Route(nameof(ShowVCode))]
    public ActionResult ShowVCode()
    {
      ValidateCode validateCode = new ValidateCode();
      string strCode = validateCode.CreateValidateCode(4);

      //验证码放到redis
      //            Session["VCode"] = strCode;
      CusRedisHelper helper =
        new CusRedisHelper(AppSetting.DbConnMap["redis"].ConnStr, "moreover", new NewtonsoftDeal(), 22);

      helper.StringSet(Request.HttpContext.Connection.RemoteIpAddress.ToString() + Request.Headers["User-Agent"],
        strCode, TimeSpan.FromMinutes(3));

      byte[] imgBytes = validateCode.CreateValidateGraphic(strCode);

      return File(imgBytes, @"image/jpeg");
    }

    #endregion
  }
}