using System;
using Api.Manage.Assist.Menu;

namespace Api.Manage.Assist.Entity
{
  /// <summary>
  /// 数据模型
  /// </summary>
  public class ResultModel
  {
    #region const_value

    public static readonly string SUCCESS = "success";
    public static readonly string ERROR = "error";
    public static readonly string MSG_PARAM_ISNULL = "信息填写不完整!";

    #endregion const_value

    /// <summary>
    /// 总行数
    /// </summary>
    public int TotalRow { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 消息
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// 错误码
    /// </summary>
    public int ErrorCode { get; set; }

    /// <summary>
    /// 临时数据信息
    /// </summary>
    public string EmptyStr { get; set; }

    /// <summary>
    /// 数据集
    /// </summary>
    public object Result { get; set; }

    /// <summary>
    /// 成功后跳转地址
    /// </summary>
    public string BackUrl { get; set; }

    /// <summary>
    /// 获取处理成功的结果集
    /// </summary>
    /// <param name="title">标题</param>
    /// <param name="result">结果集</param>
    /// <param name="totalRow">总行数</param>
    /// <returns></returns>
    public static ResultModel GetSuccessModel(object result = null, int totalRow = 0)
    {
      return new ResultModel()
      {
        ErrorCode = (int)ErrorCodeMenu.ERROR_NONE,
        Message = SUCCESS,
        Result = result,
        TotalRow = totalRow
      };
    }
    /// <summary>
    /// 获取处理成功的结果集
    /// </summary>
    /// <param name="title">标题</param>
    /// <param name="result">结果集</param>
    /// <param name="totalRow">总行数</param>
    /// <returns></returns>
    public static ResultModel GetSuccessModel(string title = null, object result = null, int totalRow = 0)
    {
      return new ResultModel()
      {
        ErrorCode = (int) ErrorCodeMenu.ERROR_NONE,
        Message = SUCCESS,
        Result = result,
        Title = title,
        TotalRow = totalRow
      };
    }

    /// <summary>
    /// 获取处理成功的结果集
    /// </summary>
    /// <param name="msg">通知消息</param>
    /// <returns></returns>
    public static ResultModel GetMessageModel(string msg, int errorCode)
    {
      return new ResultModel()
      {
        ErrorCode = errorCode,
        Message = msg
      };
    }

    /// <summary>
    /// 获取处理成功的结果集
    /// </summary>
    /// <param name="backUrl">跳转地址</param>
    /// <returns></returns>
    public static ResultModel GetTargetModel(string backUrl)
    {
      return new ResultModel()
      {
        ErrorCode = (int) ErrorCodeMenu.ERROR_NONE,
        BackUrl = backUrl
      };
    }

    /// <summary>
    /// 获取空异常结果集
    /// </summary>
    /// <param name="title">标题</param>
    /// <returns></returns>
    public static ResultModel GetNullErrorModel(string message = "信息填写不完整！")
    {
      return new ResultModel()
      {
        ErrorCode = (int) ErrorCodeMenu.ERROR_NULL,
        Message = message,
      };
    }

    /// <summary>
    /// 获取空异常结果集
    /// </summary>
    /// <param name="title">标题</param>
    /// <returns></returns>
    public static ResultModel GetNullErrorModel(string title, string message = "信息填写不完整！")
    {
      return new ResultModel()
      {
        ErrorCode = (int) ErrorCodeMenu.ERROR_NULL,
        Message = message,
        Title = title
      };
    }

    /// <summary>
    /// 获取参数异常结果集
    /// </summary>
    /// <param name="title">标题</param>
    /// <param name="result">错误提示</param>
    /// <returns></returns>
    public static ResultModel GetParamErrorModel(string message)
    {
      return new ResultModel()
      {
        ErrorCode = (int)ErrorCodeMenu.ERROR_PARAM,
        Message = message,
      };
    }

    /// <summary>
    /// 获取参数异常结果集
    /// </summary>
    /// <param name="title">标题</param>
    /// <param name="result">错误提示</param>
    /// <returns></returns>
    public static ResultModel GetParamErrorModel(string title, string message)
    {
      return new ResultModel()
      {
        ErrorCode = (int) ErrorCodeMenu.ERROR_PARAM,
        Message = message,
        Title = title
      };
    }

    /// <summary>
    /// 获取处理异常结果集
    /// </summary>
    /// <param name="title">标题</param>
    /// <param name="e">异常对象</param>
    /// <returns></returns>
    public static ResultModel GetDealErrorModel(string title, Exception e)
    {
      return new ResultModel()
      {
        ErrorCode = (int) ErrorCodeMenu.ERROR_DEAL,
        Message = e.ToString(),
        Title = title
      };
    }

    /// <summary>
    /// 获取处理异常结果集
    /// </summary>
    /// <param name="title">标题</param>
    /// <param name="confrim">异常提示信息</param>
    /// <returns></returns>
    public static ResultModel GetDealErrorModel(string title, string confrim)
    {
      return new ResultModel()
      {
        ErrorCode = (int) ErrorCodeMenu.ERROR_DEAL,
        Message = confrim,
        Title = title
      };
    }

    /// <summary>
    /// 获取处理异常结果集
    /// </summary>
    /// <param name="confrim">异常提示信息</param>
    /// <returns></returns>
    public static ResultModel GetDealErrorModel( string confrim)
    {
      return new ResultModel()
      {
        ErrorCode = (int)ErrorCodeMenu.ERROR_DEAL,
        Message = confrim,
      };
    }

    public static ResultModel GetNonLoginModel(string title, string backUrl, string message = "用户未登录或登录超时，请重新登录后进行此操作！")
    {
      return new ResultModel()
      {
        ErrorCode = (int) ErrorCodeMenu.ERROR_DEAL,
        BackUrl = backUrl,
        Message = message
      };
    }

    public static ResultModel GetSignErrorModel(string result = "", string message = "签名信息有误")
    {
      return new ResultModel()
      {
        ErrorCode = (int) ErrorCodeMenu.ERROR_SIGN,
        Message = message,
        Result = result
      };
    }
  }
}