using SampleAuthApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAuthApi.Security
{
    public interface IUserServices
    {
        IQueryable<User> GetQueryable();
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
    public class UserServices : IUserServices
    {
        private List<User> _users = new List<User>
        {
            new User { UserId = 1, FirstName = "Vasu", LastName = "Potla", Username = "vpotla", PasswordHash = HashingHelper.GetPasswordHash("test") }
        };

        public IQueryable<User> GetQueryable()
        {
            return _users.AsQueryable();
        }
        public IEnumerable<User> GetAll()
        {
            return _users.ToList();
        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(x => x.UserId == id);
        }



    }
}
