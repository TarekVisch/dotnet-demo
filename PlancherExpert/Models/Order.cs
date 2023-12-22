namespace PlancherExpert.Models
{
	public class Order
	{
		public int Id { get; set; }
		public string ClientId { get; set; }
		public string FloorCoverId { get; set; }
		public float Width { get; set; }
		public float Height { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
