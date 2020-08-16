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
    public interface ICryptoServices
    {
        string GetPasswordHash(string password);
        bool VerifyPassword(string password, string hash);
    }
    public class CryptoServices : ICryptoServices
    {
        private readonly AppSettings _appSettings;

        private const HashType _hashType = HashType.SHA384;
        public CryptoServices(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string GetPasswordHash(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password, hashType: _hashType);
        }

        public bool VerifyPassword(string password,string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash, true, _hashType);
        }
    }
}
