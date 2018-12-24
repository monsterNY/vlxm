using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Manage.CusInherit;
using Model.Common.CusAttr;

namespace Api.Manage.Assist.Menu
{
  public enum OperationMenu
  {
    /// <summary>
    /// 默认
    /// </summary>
    NOTFOUND = 0,

    /// <summary>
    /// 获取文章列表
    /// </summary>
    [Deal(DealService = typeof(GetArticleListService), Description = "获取文章列表")]
    GetArticleList = 1001
  }
}