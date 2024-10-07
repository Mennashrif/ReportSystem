
namespace ReportSystem.Application.Features.Users.Queries.LoggedInUser
{
    public class UserDTO
    {
		public int Id { get; set; }
        public string Email { get; set; } = "";
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? JobTitle { get; set; }
        public string? Birthdate { get; set; }

    }
}
