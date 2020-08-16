using SampleAuthApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAuthApi.Security
{
    public interface IUserServices
    {
        User GetByUserName(string userName);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
    public class UserServices : IUserServices
    {
        private readonly ICryptoServices _cryptoServices;
        private List<User> _users;
        public UserServices(ICryptoServices cryptoServices)
        {
            _cryptoServices = cryptoServices;
            _users = new List<User>() 
            {
                new User { UserId = 1, FirstName = "Vasu", LastName = "Potla", Username = "vpotla", PasswordHash = _cryptoServices.GetPasswordHash("test") }
            };
        }

        private IQueryable<User> GetQuery()
        {
            return _users.AsQueryable();
        }
        public IEnumerable<User> GetAll()
        {
            return GetQuery().ToList();
        }

        public User GetById(int id)
        {
            return GetQuery().FirstOrDefault(x => x.UserId == id);
        }

        public User GetByUserName(string userName)
        {
            return GetQuery().FirstOrDefault(x => x.Username == userName);
        }
    }
}
