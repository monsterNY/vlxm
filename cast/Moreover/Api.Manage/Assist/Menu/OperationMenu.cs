using System;
using Api.Manage.CusInherit;
using Api.Manage.CusInherit.Article;
using Api.Manage.CusInherit.Comment;
using Api.Manage.CusInherit.Create;
using Api.Manage.CusInherit.Select;
using Api.Manage.CusInherit.User;
using Model.Common.CusAttr;

namespace Api.Manage.Assist.Menu
{

  /// <summary>
  /// 公用操作
  /// <see cref="DealAttribute"/>
  /// </summary>
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
    /// 分页获取文章评论列表
    /// </summary>
    [Deal(typeof(GetArticleCommentPageListService), "分页获取文章评论列表")]
    GetArticleCommentPageList = 10004,

    /// <summary>
    /// 分页获取文章评论回复列表
    /// </summary>
    [Deal(typeof(GetReplyCommentPageListService), "分页获取文章评论回复列表")]
    GetReplyCommentPageList = 10005,

    /// <summary>
    /// 添加文章
    /// </summary>
    [Deal(typeof(CreateArticleService), "添加文章")]
    InsertArticle = 20001,
    /// <summary>
    /// 用户注册
    /// </summary>
    [Deal(typeof(CreateUserInfoService), "用户注册")]
    CreateUserInfo = 20002,


    /// <summary>
    /// 获取文章详情
    /// </summary>
    [Deal(typeof(GetArticleDetailService), "获取文章详情")]
    GetArticleDetail = 30001,

    /// <summary>
    /// 用户登录
    /// </summary>
    [Deal(typeof(AuthLoginService), "用户登录")]
    UserLogin = 40001,

    /// <summary>
    /// 添加作品页面访问量
    /// </summary>
    [Deal(typeof(AddPageViewService), "添加作品页面访问量")]
    AddArticlePv = 50001,

  }
}