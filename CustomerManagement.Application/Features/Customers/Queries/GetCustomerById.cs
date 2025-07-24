using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CustomerManagement.Core.Interfaces;
using CustomerManagement.Shared.DTOs;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Application.Features.Customers.Queries
{
    public static class GetCustomerById
    {
        public class Query : IRequest<CustomerDto?>
        {
            public string Id { get; set; } = null!;
        }

        public class Handler : IRequestHandler<Query, CustomerDto?>
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

            public async Task<CustomerDto?> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var customer = await _customerRepository.GetByIdAsync(request.Id);
                    return customer != null ? _mapper.Map<CustomerDto>(customer) : null;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while getting customer with ID: {CustomerId}", request.Id);
                    throw;
                }
            }
        }
    }
}
