using ServiceP.Models;

namespace ServiceP.DTO
{
    public class UserDto
    {
        public string first_name { get; set; }
        public string last_name   { get; set; }
        public string email { get; set; }

        public static UserDescribeDto User2UserDescribeDTO(User a)
        {
            return new UserDescribeDto { first_name  = a.first_name, last_name = a.last_name, role = a.role,  email = a.email };
        }

    }

    public class UserDescribeDto: UserDto
    {

        public string role { get; set; }
    }
}
