using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Dtos.Account
{
    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}