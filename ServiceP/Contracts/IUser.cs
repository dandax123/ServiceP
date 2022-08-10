using Microsoft.AspNetCore.Mvc;
using ServiceP.Models;
namespace ServiceP.Repository;

public interface IUser
{
    Task<List<User>> getAll();
    Task<User> GetById(int userId);
    Task<bool> is_duplicate_email(string email);
    Task<User> FindUserRoleByEmail(string email);

    Task DeleteUser(int id);

    User updateUser(User user);
    

}
