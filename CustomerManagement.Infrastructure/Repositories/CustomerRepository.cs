using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagement.Core.Entities;
using CustomerManagement.Core.Interfaces;
using CustomerManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CustomerRepository> _logger;

        public CustomerRepository(AppDbContext context, ILogger<CustomerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            try
            {
                return await _context.Customers.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all customers");
                throw;
            }
        }

        public async Task<Customer?> GetByIdAsync(string customerId)
        {
            try
            {
                return await _context.Customers.FindAsync(customerId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customer with ID: {CustomerId}", customerId);
                throw;
            }
        }

        public async Task<IEnumerable<Customer>> GetByNameAsync(string searchTerm)
        {
            try
            {
                return await _context.Customers
                    .Where(c => c.FirstName.Contains(searchTerm) || c.LastName.Contains(searchTerm))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching customers with term: {SearchTerm}", searchTerm);
                throw;
            }
        }

        public async Task<IEnumerable<Customer>> GetByStatusAsync(string status)
        {
            try
            {
                return await _context.Customers
                    .Where(c => c.Status.Equals(status, StringComparison.OrdinalIgnoreCase))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customers with status: {Status}", status);
                throw;
            }
        }

        public async Task<IEnumerable<Customer>> GetByCustomerTypeAsync(string customerType)
        {
            try
            {
                return await _context.Customers
                    .Where(c => c.CustomerType.Equals(customerType, StringComparison.OrdinalIgnoreCase))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting customers with type: {CustomerType}", customerType);
                throw;
            }
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            try
            {
                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();
                return customer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding customer: {CustomerId}", customer.CustomerId);
                throw;
            }
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            try
            {
                var existingCustomer = await _context.Customers.FindAsync(customer.CustomerId);
                if (existingCustomer == null)
                {
                    return false;
                }

                _context.Entry(existingCustomer).CurrentValues.SetValues(customer);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating customer: {CustomerId}", customer.CustomerId);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(id);
                if (customer == null)
                {
                    return false;
                }

                _context.Customers.Remove(customer);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting customer: {CustomerId}", id);
                throw;
            }
        }
    }
}
