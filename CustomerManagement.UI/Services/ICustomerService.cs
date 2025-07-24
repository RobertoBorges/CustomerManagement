using CustomerManagement.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerManagement.UI.Services
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto?> GetCustomerByIdAsync(string id);
        Task<CustomerDto> CreateCustomerAsync(CreateUpdateCustomerDto customerDto);
        Task<bool> UpdateCustomerAsync(string id, CreateUpdateCustomerDto customerDto);
        Task<bool> DeleteCustomerAsync(string id);
    }
}
