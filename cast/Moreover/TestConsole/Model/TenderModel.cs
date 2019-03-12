using System;
using System.Collections.Generic;
using System.Text;

namespace TestConsole.Model
{
  /// <summary>
  /// @desc : TenderModel  
  /// @author :mons
  /// @create : 2019/3/12 10:08:29 
  /// @source : 
  /// </summary>
  public class TenderModel
  {
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public int ValuationId { get; set; }
    public string TenderIntro { get; set; }
    public string CaseIntro { get; set; }
    public int FinishTime { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime? UpdateAt { get; set; }
  }
}