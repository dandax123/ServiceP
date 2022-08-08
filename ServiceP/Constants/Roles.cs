namespace ServiceP.Constants;


public class Roles
{
    public const string Customer = "Customer";
    public const string Provider = Customer + "," + "Provider";
    public const string Admin = Provider + "," + "Admin";

}