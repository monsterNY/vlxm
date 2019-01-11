using Api.Manage.CusInterface;

namespace Api.Manage.Assist.Req
{
  public class AttentionUserReq:IValidParam
  {

    public long AttentionUser { get; set; }
    public string GroupKey { get; set; }
    public string Description { get; set; }

    public string ValidInfo()
    {
      if (AttentionUser <= 0)
      {
        return "参数错误！";
      }

      if (string.IsNullOrWhiteSpace(GroupKey))
      {
        return "分组名不能为空！";
      }

      return string.Empty;

    }

  }
}
