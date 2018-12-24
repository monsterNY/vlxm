using System;
using System.Collections.Generic;
using System.Text;
using Command.RedisHelper.Inteface;
using Newtonsoft.Json;

namespace Command.RedisHelper.CusInhert
{
    public class NewtonsoftDeal:IJsonDeal
    {
        public string Serialize(object obj)
        {

            if (obj != null)
            {
                return JsonConvert.SerializeObject(obj);
            }

            return string.Empty;
        }

        public T Deserialize<T>(string jsonDate) 
        {
            if (string.IsNullOrWhiteSpace(jsonDate))
            {
                return default(T);
            }
            
            return JsonConvert.DeserializeObject<T>(jsonDate);
        }
    }
}
