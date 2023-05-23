using System;
using System.ComponentModel;

namespace Web.Models
{
	public class Product
	{
		public int Id { get; set; }
		[DisplayName("Mehsul adi")]
		public string Name { get; set; }
		public decimal Price { get; set; }
		public decimal Discount { get; set; }
		public string Description { get; set; }
		public int Quantity { get; set; }
		public string PhotoUrl { get; set; }
		public string CoverPhoto { get; set; }
		public string SeoUrl { get; set; }
		public int CategoryId { get; set; }
		public Category Category { get; set; }
		public bool IsSlider { get; set; }
	}
}

