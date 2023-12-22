﻿namespace PlancherExpert.Models
{
	public class ProductModel
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public float Price { get; set; }
		public float Installation { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		public ProductModel() { }
	}
}