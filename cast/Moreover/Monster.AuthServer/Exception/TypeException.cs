using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monster.AuthServer.Exception
{
    public class TypeException:System.Exception
    {
        public TypeException()
        {
        }

        public TypeException(string message) : base(message)
        {
        }
    }
}
