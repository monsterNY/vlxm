using System;
using Api.Manage.CusInherit;
using Api.Manage.CusInherit.Article;
using Api.Manage.CusInherit.Article.Action;
using Api.Manage.CusInherit.Attention;
using Api.Manage.CusInherit.Comment;
using Api.Manage.CusInherit.Create;
using Api.Manage.CusInherit.Select;
using Api.Manage.CusInherit.User;
using Model.Common.CusAttr;

namespace Api.Manage.Assist.Menu
{
  /// <summary>
  /// 需要授权的操作
  /// <see cref="AuthDealAttribute"/>
  /// </summary>
  public enum AuthOperationMenu
  {

    /// <summary>
    /// 分页获取文章列表
    /// </summary>
    [AuthDeal(typeof(GetArticlePageListAuthService), "分页获取文章列表")]
    GetArticlePageList = 10001,

    /// <summary>
    /// 添加文章
    /// </summary>
    [AuthDeal(typeof(CreateArticleAuthService), "添加文章")]
    InsertArticle = 20001,
    /// <summary>
    /// 添加文章评论
    /// </summary>
    [AuthDeal(typeof(CreateArticleCommentAuthService), "添加文章评论")]
    InsertArticleComment = 20002,

    /// <summary>
    /// 获取用户详情信息
    /// </summary>
    [AuthDeal(typeof(GetUserDetailAuthService), "获取用户详情信息")]
    GetUserDetail = 30001,

    /// <summary>
    /// 查询文章已操作数量
    /// </summary>
    [AuthDeal(typeof(SelectActionAuthService), "查询文章已操作数量")]
    SelectAction = 30002,

    /// <summary>
    /// 删除文章
    /// </summary>
    [AuthDeal(typeof(RemoveArticleAuthService), "删除文章")]
    RemoveArticle = 40001,

    /// <summary>
    /// 修改用户信息
    /// </summary>
    [AuthDeal(typeof(UpdateUserInfoAuthService), "修改用户信息'")]
    UpdateUserInfo = 50001,

    /// <summary>
    /// 文章操作
    /// </summary>
    [AuthDeal(typeof(SingleActionAuthService), "文章操作")]
    SingleAction = 60001,

    /// <summary>
    /// 关注用户
    /// </summary>
    [AuthDeal(typeof(AttentionUserAuthService), "关注用户")]
    AttentionUser = 60002,

    /// <summary>
    /// 取消关注用户
    /// </summary>
    [AuthDeal(typeof(CancelAttentionUserAuthService), "取消关注用户")]
    CancelAttentionUser = 60003,

    /// <summary>
    /// 查看是否已关注
    /// </summary>
    [AuthDeal(typeof(SearchIsExistsAttentionAuthService), "查看是否已关注")]
    SearchIsExistsAttention = 60004,



  }
}