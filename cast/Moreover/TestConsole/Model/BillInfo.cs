using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Domain.Model
{
  public class BillInfo:BaseModel
  {

    public decimal Money { get; set; }

    public long UserId { get; set; }

  }
}
