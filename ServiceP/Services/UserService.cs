using Microsoft.AspNetCore.Mvc;
using ServiceP.Models;
using ServiceP.Repository;

namespace ServiceP.Services
{
    public class UserService : IUser
    {
        private DataContext _dataContext;
        public UserService(DataContext context)
        {
            _dataContext = context;
        }



        public async Task<User> GetByEmail(string email)
        {
            var user = await _dataContext.Users.FirstAsync(y => y.email == email);

            //error handling
            return user;
        }
        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> getAll()
        {
            var users = _dataContext.Providers;

            return users;
        }

        public async Task<User> GetById(int id)
        {
            var user = await _dataContext.Users.FindAsync(id);

            if (user == null)
            {
                throw new Exception("User not found");
            }
            return user;

        }

        public User updateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
