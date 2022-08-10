using ServiceP.DTO;
using ServiceP.Models;

namespace ServiceP.Repository
{
    public interface IProvider
    {
        Task<Provider> getById(int id);
        Task<Provider> getByEmail(string email);

        Task<IEnumerable<Provider>> getAllProviders();
        Task<string> RegisterProvider(ProviderRegistrationRequest request);



        Task updateProvider(int user_id, UserDto a);

        Task deleteProvider(int user_id);


    }
}
