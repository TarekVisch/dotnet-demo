namespace PlancherExpert.Models
{
	public class OrderModel
	{
		public int Id { get; set; }
		public string ClientId { get; set; }
		public string ProductId { get; set; }
		public float Width { get; set; }
		public float Height { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
