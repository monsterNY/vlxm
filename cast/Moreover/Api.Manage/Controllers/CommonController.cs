using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Api.Manage.Assist.Utils;
using Command.RedisHelper.CusInhert;
using Command.RedisHelper.Helper;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Model.Article.Entity;
using Model.Common.ConfigModels;
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
    /// 测试db
    /// </summary>
    /// <returns></returns>
    [Route(nameof(TestDb))]
    public ActionResult<object> TestDb()
    {
      using (IDbConnection conn = new MySqlConnection(AppSetting.ConnectionString["Mysql"]))
      {
        IEnumerable<ArticleInfo> articleInfos = conn.Query<ArticleInfo>("select * from article_info");
        return articleInfos.ToList();
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
        new CusRedisHelper(AppSetting.ConnectionString["redis"], "moreover", new NewtonsoftDeal(), 22);

      helper.StringSet(Request.HttpContext.Connection.RemoteIpAddress.ToString() + Request.Headers["User-Agent"],
        strCode, TimeSpan.FromMinutes(3));。

      byte[] imgBytes = validateCode.CreateValidateGraphic(strCode);

      return File(imgBytes, @"image/jpeg");
    }

    #endregion
  }
}