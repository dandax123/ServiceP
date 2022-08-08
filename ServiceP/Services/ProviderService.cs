using ServiceP.DTO;
using ServiceP.Models;
using ServiceP.Repository;

namespace ServiceP.Services
{
    public class ProviderService : IProvider
    {
        DataContext _dataContext;
        public ProviderService(DataContext dataContext)
        {
            _dataContext = dataContext;
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

    }
}
