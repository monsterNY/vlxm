using System;
using System.Collections.Generic;
using System.Text;
using Model.Vlxm.Entity;
using Model.Vlxm.Menu;
using Model.Vlxm.Tools;

namespace DapperContext.Const
{
  public class SqlCharConst
  {
    public const string AS = nameof(AS);

    public const string ASC = nameof(ASC);

    public const string DESC = nameof(DESC);

    public const string INSERT = nameof(INSERT);

    public const string SET = nameof(SET);

    public const string UPDATE = nameof(UPDATE);

    public const string INTO = nameof(INTO);

    public const string VALUES = nameof(VALUES);

    public const string EXISTS = nameof(EXISTS);

    public const string SELECT = nameof(SELECT);

    public const string COUNT = nameof(COUNT);

    public const string FROM = nameof(FROM);

    public const string WHERE = nameof(WHERE);

    public const string AND = nameof(AND);

    public const string LIKE = nameof(LIKE);

    public const string LIMIT = nameof(LIMIT);

    public const string JOIN = nameof(JOIN);

    public const string INNER = nameof(INNER);

    public const string ON = nameof(ON);

    public const string ORDERBY = "ORDER BY";

    public static string DefaultWhere = $"ValidFlag = {(int) ValidFlagMenu.UseFul}";

    public static string DefaultOrder = $"{nameof(BaseModel.Id)} {SqlCharConst.DESC}";

  }
}