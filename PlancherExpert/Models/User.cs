namespace PlancherExpert.Models
{
	public class User
	{
		public string Id { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Role { get; set; } = "user";

		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		public User() { }
	}
}
