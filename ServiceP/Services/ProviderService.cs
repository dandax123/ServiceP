using ServiceP.DTO;
using ServiceP.Models;
using ServiceP.Repository;

namespace ServiceP.Services
{
    public class ProviderService : IProvider
    {
        DataContext _dataContext;
        IAuth _myAuthService;
        IUser _myUserService;
        public ProviderService(DataContext dataContext, IAuth myAuthService, IUser myUserService)
        {
            _dataContext = dataContext;
            _myAuthService = myAuthService;
            _myUserService = myUserService; 
        }
        public async Task<Provider> getById(int id)
        {
            Provider? a = await _dataContext.Providers.FindAsync(id);
            if (a == null)
            {
                throw new MissingException("Can't find the user");
            }
            return a;
        }
        public async Task createProvider(Provider user)
        {
            await _dataContext.Providers.AddAsync(user);
            await _dataContext.SaveChangesAsync();

        }
        public async Task<string> RegisterProvider(ProviderRegistrationRequest request)

        {

            if (!AuthService.IsValidEmail(request.email))
            {
                throw new AppException("Invalid email address given. Provide a valid email address");
            }
            bool user_exist = await  _myUserService.is_duplicate_email(request.email);
            if (user_exist)
            {
                throw new AppException("A user with this email already exists!");
            }
            AuthService.createPasswordHash(request.password, out byte[] passwordHash, out byte[] passwordSalt);

            Provider provider = new Provider
            {
                password_hash = passwordHash,
                password_salt = passwordSalt,
                email = request.email,
                first_name = request.first_name,
                services = new List<Service>(),
                brand_name = request.brand_name,
                last_name = request.last_name,
                role = "Provider"
            };
            await createProvider(provider);

            return _myAuthService.createToken(provider);
        }
        public async Task<Provider> getByEmail(string email)
        {

            Provider? a = await _dataContext.Providers.FirstOrDefaultAsync(y => y.email == email);
            if (a == null)
            {
                throw new MissingException("Can't find the user");
            }
            return a;
        }

        public Task<IEnumerable<Provider>> getAllProviders()
        {
            throw new NotImplementedException();
        }
        public async Task updateProvider(int user_id, UserDto a)
        {
            Provider cus = await getById(user_id);
            cus.first_name = a.first_name;
            cus.last_name = a.last_name;

            if (!AuthService.IsValidEmail(a.email))
            {
                throw new AppException("Invalid email provided!");
            }

            cus.email = a.email;

            await _dataContext.SaveChangesAsync();
        }

        public async Task deleteProvider(int id)
        {
            Provider customer = await getById(id);
            _dataContext.Providers.Remove(customer);
            await _dataContext.SaveChangesAsync();
        }

    }
}
