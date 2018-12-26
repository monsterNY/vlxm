using System;
using System.Collections.Generic;
using System.Text;

namespace Command.RedisHelper.Inteface
{
    public interface IJsonDeal
    {

        string Serialize(object obj);

        T Deserialize<T>(string jsonDate);

    }
}
