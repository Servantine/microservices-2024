using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using WalletServices.Models;
using WalletServices.DTO;
using System.Text;
using WalletServices.Services;

namespace WalletServices.services
{
    public class ProductServices : IProduct
    {
        private readonly HttpClient _httpClient;

        public ProductServices(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri("https://localhost:7071");
        }


        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            using (var client = new HttpClient(handler))
            {
                var response = await _httpClient.GetAsync("/api/products");
                if (response.IsSuccessStatusCode)
                {
                    var results = await response.Content.ReadAsStringAsync();
                    var product = JsonSerializer.Deserialize<IEnumerable<Product>>(results);
                    return product;
                }
                else
                {
                    throw new ArgumentException($"Cannot get products - httpstatus : {response.StatusCode}");
                }
            }
        }

        public async Task<Product> GetByProductId(int id)
        {
            var response = await _httpClient.GetAsync($"/api/products/{id}");
            if (response.IsSuccessStatusCode)
            {
                var product = await response.Content.ReadFromJsonAsync<Product>();
                if (product == null)
                {
                    throw new ArgumentException("Cannot get Product");
                }
                return product;

            }
            else
            {
                throw new ArgumentException($"cannot get Costumers - httpstatus : {response.StatusCode}");
            }
        }
    }
}