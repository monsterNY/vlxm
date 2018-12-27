using System;
using Api.Manage.CusInherit;
using Api.Manage.CusInherit.Create;
using Api.Manage.CusInherit.Select;
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
    /// 分页获取文章类型列表
    /// </summary>
    [Deal(typeof(GetArticleTypePageListService), "分页获取文章类型列表")]
    GetArticleTypePageList = 10002,

    /// <summary>
    /// 分页获取文章类型列表
    /// </summary>
    [Deal(typeof(GetArticleTagPageListService), "分页获取文章标签列表")]
    GetArticleTagPageList = 10003,

    /// <summary>
    /// 添加文章
    /// </summary>
    [Deal(typeof(CreateArticleService), "添加文章")]
    InsertArticle = 20001,
  }
}