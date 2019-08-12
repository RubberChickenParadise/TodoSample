using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.Models;

namespace TodoAPI.ComplexResponses
{
    public class LoginResponse
    {
        public string SessionToken { get; set; }
        public User User { get; set; }
    }
}
