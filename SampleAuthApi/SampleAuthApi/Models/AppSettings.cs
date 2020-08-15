using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAuthApi.Models
{
    public class AppSettings
    {
        /*Store these in some kind of keyvault or in separate Database from User Passwords with encryption*/
        public string SecretKey { get; set; }
        public string HashSalt { get; set; }
        public string HashKey { get; set; }
    }
}
