using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Manage.Assist.Req
{
  public class UpdateUserReq
  {

    public string DisplayName { get; set; }

    public string Description { get; set; }

    public string FaceImg { get; set; }

    public string ValidInfo()
    {
      if (string.IsNullOrWhiteSpace(DisplayName))
      {
        return "显示名不能为空！";
      }

      if (string.IsNullOrWhiteSpace(FaceImg))
      {
        return "头像不能为空！";
      }

      return string.Empty;

    }

  }
}
