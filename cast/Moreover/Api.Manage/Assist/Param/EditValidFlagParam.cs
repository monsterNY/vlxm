using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model.Vlxm.Menu;

namespace Api.Manage.Assist.Param
{
  public class EditValidFlagParam
  {
    public long Id { get; set; }

    public DateTime UpdateTime { get; set; }

    public ValidFlagMenu ValidFlag { get; set; }

  }
}
