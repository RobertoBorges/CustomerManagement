using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CustomerManagement.Core.Entities;
using CustomerManagement.Core.Interfaces;
using CustomerManagement.Shared.DTOs;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Application.Features.Customers.Commands
{
    public static class UpdateCustomer
    {
        public class Command : IRequest<bool>
        {
            public string CustomerId { get; set; } = null!;
            public CreateUpdateCustomerDto CustomerDto { get; set; } = null!;
        }

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IMapper _mapper;
            private readonly ILogger<Handler> _logger;

            public Handler(ICustomerRepository customerRepository, IMapper mapper, ILogger<Handler> logger)
            {
                _customerRepository = customerRepository;
                _mapper = mapper;
                _logger = logger;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    // Ensure the ID in the DTO matches the ID in the command
                    request.CustomerDto.CustomerId = request.CustomerId;
                    
                    var customer = _mapper.Map<Customer>(request.CustomerDto);
                    return await _customerRepository.UpdateAsync(customer);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while updating customer with ID: {CustomerId}", request.CustomerId);
                    throw;
                }
            }
        }
    }
}
