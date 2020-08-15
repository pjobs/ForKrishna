using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using SampleAuthApi.Models;
using Microsoft.Extensions.Options;
using BCrypt.Net;

namespace SampleAuthApi.Security
{
    public class HashingService
    {
        private readonly AppSettings _appSettings;
        public HashingService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public static string GetPasswordHash(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password, hashType: HashType.SHA384);
        }
    }
}
