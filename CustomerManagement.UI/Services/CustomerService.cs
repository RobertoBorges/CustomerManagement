using CustomerManagement.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CustomerManagement.UI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<CustomerDto>> GetAllCustomersAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<CustomerDto>>("api/customers");
            return response ?? new List<CustomerDto>();
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(string id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<CustomerDto>($"api/customers/{id}");
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task<CustomerDto> CreateCustomerAsync(CreateUpdateCustomerDto customerDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/customers", customerDto);
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CustomerDto>(content, _jsonOptions) ?? 
                   throw new InvalidOperationException("Failed to deserialize the response.");
        }

        public async Task<bool> UpdateCustomerAsync(string id, CreateUpdateCustomerDto customerDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/customers/{id}", customerDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCustomerAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"api/customers/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
