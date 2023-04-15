﻿using System;
namespace Web.Models
{
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public decimal Discount { get; set; }
		public string Description { get; set; }
		public int Quantity { get; set; }
		public string PhotoUrl { get; set; }
		public string SeoUrl { get; set; }
		public int CategoryId { get; set; }
		public Category Category { get; set; }
	}
}
