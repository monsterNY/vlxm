using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Manage.Assist.Entity;
using Api.Manage.CusInterface;
using Microsoft.AspNetCore.Http;
using Model.Common.ConfigModels;
using Model.Vlxm.Menu;

namespace Api.Manage.Assist.Req
{
  public class SingleActionArticleReq
  { 

    /// <summary>
    /// 
    /// </summary>
    public int ArticleId { get; set; }

    /// <summary>
    ///
    /// <see cref="ArticleOptMenu"/>
    /// </summary>
    public string ActionKey { get; set; }

    public string ValidInfo()
    {

      if (ArticleId <= 0 || string.IsNullOrWhiteSpace(ActionKey))
      {
        return "参数异常！";
      }

      return string.Empty;

    }

  }
}
