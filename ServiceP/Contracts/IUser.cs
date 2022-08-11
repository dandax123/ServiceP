using Microsoft.AspNetCore.Mvc;
using ServiceP.DTO;
using ServiceP.Models;
namespace ServiceP.Repository;

public interface IUser
{
    Task<List<UserDescribeDto>> getAll();
    Task<User> GetById(int userId);
    Task<bool> is_duplicate_email(string email);
    Task<User> FindUserRoleByEmail(string email);


    Task<List<UserDto>> getAllProviders();
    Task deleteUser(int id);
    Task createUser(User user);

    Task<string> RegisterProvider(ProviderRegistrationRequest request);

    Task<string> RegisterCustomer(BaseRegistrationRequest request);

    Task updateUser(int user_id, UserDto a);
    Task<string> login(LoginRequest request);


}
