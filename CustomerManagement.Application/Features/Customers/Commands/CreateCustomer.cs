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
    public static class CreateCustomer
    {
        public class Command : IRequest<CustomerDto>
        {
            public CreateUpdateCustomerDto CustomerDto { get; set; } = null!;
        }

        public class Handler : IRequestHandler<Command, CustomerDto>
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

            public async Task<CustomerDto> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var customer = _mapper.Map<Customer>(request.CustomerDto);
                    var result = await _customerRepository.AddAsync(customer);
                    return _mapper.Map<CustomerDto>(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while creating customer");
                    throw;
                }
            }
        }
    }
}
