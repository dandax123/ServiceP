using Microsoft.AspNetCore.Mvc;
using ServiceP.DTO;
using ServiceP.Models;
using ServiceP.Repository;

namespace ServiceP.Services
{
    public class CustomerService : ICustomer
    {
        DataContext _dataContext;
        IAuth _myAuthService;
        IUser _myUserService;
        public CustomerService(DataContext data, IAuth myAuthService, IUser myUserService)
        {
            _dataContext = data;
            _myAuthService = myAuthService;
            _myUserService = myUserService; 
        }
        public async Task createCustomer(Customer user)
        {
            await _dataContext.Customers.AddAsync(user);
            await _dataContext.SaveChangesAsync();
        }
        public async Task<string> RegisterCustomer(BaseRegistrationRequest request)
        {
            
            if (!AuthService.IsValidEmail(request.email))
            {
                throw new AppException("Invalid email address given. Provide a valid email address");
            }
            bool user_exist = await _myUserService.is_duplicate_email(request.email);
            if (user_exist)
            {
                throw new AppException("A user with this email already exists");
            }
            AuthService.createPasswordHash(request.password, out byte[] passwordHash, out byte[] passwordSalt);
            Customer customer = new Customer
            {
                password_hash = passwordHash,
                password_salt = passwordSalt,
                email = request.email,
                first_name = request.first_name,
                bookings = new List<Booking>(),
                last_name = request.last_name,
                role = "Customer"
            };
            await createCustomer(customer);

            return _myAuthService.createToken(customer);
        }
        public async Task<Customer> getByEmail(string email)
        {
            Customer? a = await _dataContext.Customers.FirstOrDefaultAsync(y => y.email == email);
            if (a == null)
            {
                throw new MissingException("Can't find the user");
            }
            return a;
        }

        public async Task<Customer> getById(int id)
        {
            Customer? a = await _dataContext.Customers.FindAsync(id);
            if (a == null)
            {
                throw new MissingException("Can't find the user");
            }
            return a;
        }

        public async Task updateCustomer(int user_id, UserDto a)
        {
            Customer cus = await getById(user_id);
            cus.first_name = a.first_name;
            cus.last_name = a.last_name;
            
            if(!AuthService.IsValidEmail(a.email))
            {
                throw new AppException("Invalid email provided!");
            }

            cus.email = a.email;

            await _dataContext.SaveChangesAsync();
        }

        public async Task deleteCustomer(int id)
        {
            Customer customer = await getById(id);
            _dataContext.Customers.Remove(customer);
            await _dataContext.SaveChangesAsync();
        }
    }
}
