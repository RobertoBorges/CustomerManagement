using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerManagement.Core.Entities;

namespace CustomerManagement.Core.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(string id);
        Task<IEnumerable<Customer>> GetByNameAsync(string searchTerm);
        Task<IEnumerable<Customer>> GetByStatusAsync(string status);
        Task<IEnumerable<Customer>> GetByCustomerTypeAsync(string customerType);
        Task<Customer> AddAsync(Customer customer);
        Task<bool> UpdateAsync(Customer customer);
        Task<bool> DeleteAsync(string id);
    }
}
