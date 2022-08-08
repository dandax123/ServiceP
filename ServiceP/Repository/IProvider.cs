using ServiceP.DTO;
using ServiceP.Models;

namespace ServiceP.Repository
{
    public interface IProvider
    {
        Task<Provider> getById(int id);
        Task<Provider> getByEmail(string email);

        Task<IEnumerable<Provider>> getAllProviders();

        Task createProvider(Provider user);





    }
}
