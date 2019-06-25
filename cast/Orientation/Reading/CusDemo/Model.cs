using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reading.CusDemo
{
  /// <summary>
  /// @desc : Model  
  /// @author : mons
  /// @create : 2019/6/25 15:43:04 
  /// @source : 
  /// </summary>
  public class Model<T>
  {
    private T Info { get; set; }

    public List<T> List { get; set; }

    public static implicit operator Model<T>(T t)
    {
      return new Model<T>()
      {
        Info = t
      };
    }

    public static Model<T> operator +(Model<T> item, Model<T> model)
    {
      if (item.List == null) item.List = new List<T>() {item.Info};

      item.List.Add(model.Info);

      return item;
    }

    public static explicit operator List<T>(Model<T> model)
    {
      return model.List;
    }
  }
}