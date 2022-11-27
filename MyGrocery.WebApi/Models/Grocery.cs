using System;
using System.ComponentModel.DataAnnotations;

namespace MyGrocery.WebApi.Models
{
	public class Grocery
	{
		[Key]
		public int Id { get; set; }
		public string? Name { get; set; }
	}
}

