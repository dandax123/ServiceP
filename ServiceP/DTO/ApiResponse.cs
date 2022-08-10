namespace ServiceP.DTO
{
    public class ApiError
    {
        public string error_message { get; set;  }
    }

    public class ApiSuccess
    {
        public string success_message { get; set; }
        public ApiSuccess(string  _sucess_message)
        {
            this.success_message = _sucess_message;

        }
    }
}
