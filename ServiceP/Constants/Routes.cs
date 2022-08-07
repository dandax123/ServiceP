namespace ServiceP.Constants
{
    public class Routes
    {
        public const string BaseUrl = "api/";
        public const string Version = "v1/";

        public static class AuthRoutes
        {
            public const string Base = BaseUrl + Version + "auth/";
            public const string SignIn = Base + "signin";
            public const string BuyerSignUp = Base + "signup/seller";
            public const string SellerSignUp = Base + "signup/";
        }

        
        public static class UserRoutes
        {
            public const string Base = BaseUrl + Version + "user/{userId}";
            public const string GetUserByID = Base;
            public const string UpdateUser = Base;
            public const string DeleteUser = Base;
            public const string GetBookings = Base + "/bookings";
            public const string GetServices = Base + "/services";

        }

        public static class ServiceRoutes
        {
            public const string Base = BaseUrl + Version + "services";
            public const string GetAllProducts = Base;
            public const string GetUserProducts = Base + "/user";
            public const string GetProductById = Base + "/{id}";
            public const string CreateProduct = Base;
            public const string UpdateProduct = Base + "/{id}";
            public const string DeleteProduct = Base + "/{id}";
        }

        public static class Bookings
        {
            public const string Base = BaseUrl + Version + "bookings";
            public const string GetAllTransactions = Base;
            public const string GetTransactionById = Base + "/{id}";
            public const string CreateTransaction = Base;
            public const string UpdateTransaction = Base + "/{id}";
            public const string DeleteTransaction = Base + "/{id}";
        }
    }
}
