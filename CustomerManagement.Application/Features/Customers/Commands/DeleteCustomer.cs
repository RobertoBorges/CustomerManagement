using System;
using System.Threading;
using System.Threading.Tasks;
using CustomerManagement.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Application.Features.Customers.Commands
{
    public static class DeleteCustomer
    {
        public class Command : IRequest<bool>
        {
            public string Id { get; set; } = null!;
        }

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly ILogger<Handler> _logger;

            public Handler(ICustomerRepository customerRepository, ILogger<Handler> logger)
            {
                _customerRepository = customerRepository;
                _logger = logger;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    return await _customerRepository.DeleteAsync(request.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while deleting customer with ID: {CustomerId}", request.Id);
                    throw;
                }
            }
        }
    }
}
