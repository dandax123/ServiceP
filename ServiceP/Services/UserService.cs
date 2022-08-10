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



        public async Task<bool> is_duplicate_email(string email)
        {
            User? user = await _dataContext.Users.FirstOrDefaultAsync(y => y.email == email);
            
            //error handling
            return user != null;
        }

        public async Task DeleteUser(int id)
        {
            User user = await GetById (id);
            _dataContext.Users.Remove(user);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<List<User>> getAll()
        {
            var users = await _dataContext.Users.ToListAsync();

            return users;
        }

        public async Task<User> GetById(int id)
        {
            var user = await _dataContext.Users.FindAsync(id);

            if (user == null)
            {
                throw new MissingException("User not found");
            }
            return user;

        }

        public async  Task<User> FindUserRoleByEmail(string email)
        {
            
            Provider? a = await _dataContext.Providers.FirstOrDefaultAsync(y => y.email == email);
            Console.WriteLine(a);
            if (a != null)
            {
                a.role = "Provider";
                return a;
            }

            Customer? b = await _dataContext.Customers.FirstOrDefaultAsync(y => y.email == email);
            Console.WriteLine(b);
            if (b != null)
            {
                b.role = "Customer";
                return b;
            }
            

            
            throw new AppException("Incorrect Username or password");

        }

        public User updateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
