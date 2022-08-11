namespace ServiceP.Constants;


public class Roles
{
    public const string Customer = "Customer";
    public const string Provider = "Provider";
    public const string ProviderOrCustomer = Customer + "," + Provider;
    public const string Admin = Provider + "," + "Admin";

    public static string getRole (string role)
    {
        if (role.Equals("Provider"))
        {
            return Provider;
        }
        if(role.Equals("Customer"))
        {
            return Customer;
        }

        if(role.Equals("Admin"))
        {
            return Admin;
        }

        throw new AppException("Invalid role provided");
    }

}