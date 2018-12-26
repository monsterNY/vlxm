using Api.Manage.CusInherit;
using Api.Manage.CusInherit.Create;
using Model.Common.CusAttr;

namespace Api.Manage.Assist.Menu
{
  public enum OperationMenu
  {
    /// <summary>
    /// 默认
    /// </summary>
    [Deal(typeof(GetArticleListService), "获取文章列表")]
    NOTFOUND = 0,

    /// <summary>
    /// 获取文章列表
    /// </summary>
    [Deal(typeof(GetArticleListService), "获取文章列表")]
    GetArticleList = 1001,

    /// <summary>
    /// 分页获取文章列表
    /// </summary>
    [Deal(typeof(GetArticlePageListService), "分页获取文章列表")]
    GetArticlePageList = 10001,

    /// <summary>
    /// 添加文章
    /// </summary>
    [Deal(typeof(CreateArticleService), "添加文章")]
    InsertArticle = 20001,

  }
}