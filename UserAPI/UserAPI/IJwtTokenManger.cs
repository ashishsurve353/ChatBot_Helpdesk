using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI
{
    public interface IJwtTokenManger
    {
        string Authenticate(string UserName, string Password);
        string Generate(string UserName, string Password);
    }
}
