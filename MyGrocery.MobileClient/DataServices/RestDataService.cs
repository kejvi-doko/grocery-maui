using System;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using MyGrocery.MobileClient.Models;

namespace MyGrocery.MobileClient.DataServices
{
	public class RestDataService:IRestDataSevice
	{
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public RestDataService()
		{
            _httpClient = new HttpClient();
            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5290" : "http://localhost:5290";
            _url = $"{_baseAddress}/api";
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

        }

        private bool HasInternet => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public async Task AddGroceryAsync(Grocery grocery)
        {
            if (!HasInternet)
            {
                Debug.WriteLine("---> No internet access ...");
                return;
            }

            try
            {
                string jsonGrocrey = JsonSerializer.Serialize<Grocery>(grocery,_jsonSerializerOptions);
                StringContent content = new StringContent(jsonGrocrey, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/grocery", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Grocery created");
                    return;
                }
                else
                {
                    Debug.WriteLine("---> Non Http 2XX");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}");
            }
            return;
        }

        public async Task DeleteGroceryAsync(int id)
        {
            if (!HasInternet)
            {
                Debug.WriteLine("---> No internet access ...");
                return;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/grocery/{id}");
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Grocery deleted");
                    return;
                }
                else
                {
                    Debug.WriteLine("---> Non Http 2XX");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}");
            }
            return;
        }

        public async Task<List<Grocery>> GetGroceriesAsync()
        {
            List<Grocery> groceries = new List<Grocery>();
            if (!HasInternet)
            {
                Debug.WriteLine("---> No internet access ...");
                return groceries;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/grocery");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    groceries = JsonSerializer.Deserialize<List<Grocery>>(content, _jsonSerializerOptions); 

                }
                else
                {
                    Debug.WriteLine("---> Non Http 2XX");
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"{ex.Message}");
            }
            return groceries;
        }

        public async Task UpdateGroceryAsync(Grocery grocery)
        {
            if (!HasInternet)
            {
                Debug.WriteLine("---> No internet access ...");
                return;
            }

            try
            {
                string jsonGrocery = JsonSerializer.Serialize<Grocery>(grocery);
                StringContent content = new StringContent(jsonGrocery, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PutAsync($"{_url}/grocery/{grocery.Id}",content);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Grocery updated");
                    return;
                }
                else
                {
                    Debug.WriteLine("---> Non Http 2XX");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}");
            }
            return;
        }
    }
}

