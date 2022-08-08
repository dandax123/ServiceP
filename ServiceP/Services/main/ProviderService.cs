using ServiceP.Models;
using ServiceP.Repository;

namespace ServiceP.Services.main
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
            if(a == null)
            {
                throw new Exception("Could not find");
            }
            return a;
        }
        public async Task<Provider> getByEmail(string email)
        {
           
            Provider? a = await _dataContext.Providers.FirstOrDefaultAsync(y => y.email == email);
            if (a == null)
            {
                throw new Exception("Could not find");
            }
            return a;
        }
    }
}
