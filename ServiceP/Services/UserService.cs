using Microsoft.AspNetCore.Mvc;
using ServiceP.DTO;
using ServiceP.Models;
using ServiceP.Repository;

namespace ServiceP.Services
{
    public class UserService : IUser
    {
        private DataContext _dataContext;
        private IAuth _myAuthService;
        public UserService(DataContext context , IAuth myAuthService)
        {
            _dataContext = context;
            _myAuthService = myAuthService;
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

        public async Task<List<UserDescribeDto>> getAll()
        {
            var users = await _dataContext.Users.ToListAsync();
            return users.ConvertAll(new Converter<User, UserDescribeDto>(UserDto.User2UserDescribeDTO));


        }


        public async Task<List<UserDto>> getAllProviders()
        {
            var users = await _dataContext.Users.Where( y=> y.role == "Provider").ToListAsync();

            return users.ConvertAll(new Converter<User, UserDto>(UserDto.User2UserDTO));

        }

  


        public async Task<string> login(LoginRequest request)
        {
            if (request.email.Equals("admin@admin.com") && request.password.Equals("admin"))
            {
                User admin_user = new User
                {
                    email = request.email,
                    role = "Admin",
                    userId = 100
                };
                return _myAuthService.createToken(admin_user);
            }

            User a = await FindUserRoleByEmail(request.email);
            var correct_password =  AuthService.verifyPasswordHash(request.password, a.password_hash, a.password_salt);
            if (!correct_password)
            {
                throw new AppException("Incorrect username or password!");
            }
            return _myAuthService.createToken(a);
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
            
            User? a = await _dataContext.Users.FirstOrDefaultAsync(y => y.email == email);
            if (a != null)
            {
                return a;
            }            

            
            throw new AppException("Incorrect Username or password");

        }

  
        public async Task updateUser(int user_id, UserDto a)
        {
            User cus = await GetById(user_id);
            cus.first_name = a.first_name;
            cus.last_name = a.last_name;

            if (!AuthService.IsValidEmail(a.email))
            {
                throw new AppException("Invalid email provided!");
            }

            cus.email = a.email;

            await _dataContext.SaveChangesAsync();
        }

        public async Task deleteUser(int id)
        {
            User customer = await GetById(id);
            _dataContext.Users.Remove(customer);
            await _dataContext.SaveChangesAsync();
        }

        public async Task createUser(User user)
        {
            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();

        }

        public async Task<string> RegisterProvider(ProviderRegistrationRequest request)

        {

            if (!AuthService.IsValidEmail(request.email))
            {
                throw new AppException("Invalid email address given. Provide a valid email address");
            }
            bool user_exist = await is_duplicate_email(request.email);
            if (user_exist)
            {
                throw new AppException("A user with this email already exists!");
            }
            AuthService.createPasswordHash(request.password, out byte[] passwordHash, out byte[] passwordSalt);

            User provider = new User
            {
                password_hash = passwordHash,
                password_salt = passwordSalt,
                email = request.email,
                first_name = request.first_name,
                services = new List<Service>(),
                brand_name = request.brand_name,
                last_name = request.last_name,
                role = "Provider",
                bookings = new List<Booking>()
            };
            await createUser(provider);

            return _myAuthService.createToken(provider);
        }

        public async Task<string> RegisterCustomer(BaseRegistrationRequest request)
        {

            if (!AuthService.IsValidEmail(request.email))
            {
                throw new AppException("Invalid email address given. Provide a valid email address");
            }
            bool user_exist = await is_duplicate_email(request.email);
            if (user_exist)
            {
                throw new AppException("A user with this email already exists");
            }
            AuthService.createPasswordHash(request.password, out byte[] passwordHash, out byte[] passwordSalt);
            User customer = new User
            {
                password_hash = passwordHash,
                password_salt = passwordSalt,
                email = request.email,
                first_name = request.first_name,
                bookings = new List<Booking>(),
                last_name = request.last_name,
                role = "Customer",
                services = new List<Service>(),
                brand_name = String.Empty
            };
            await createUser(customer);

            return _myAuthService.createToken(customer);
        }
    }
}
