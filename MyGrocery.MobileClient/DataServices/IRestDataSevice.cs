using System;
using MyGrocery.MobileClient.Models;

namespace MyGrocery.MobileClient.DataServices
{
	public interface IRestDataSevice
	{
		Task<List<Grocery>> GetGroceriesAsync();
		Task AddGroceryAsync(Grocery grocery);
		Task DeleteGroceryAsync(int id);
		Task UpdateGroceryAsync(Grocery grocery); 
		
	}
}

