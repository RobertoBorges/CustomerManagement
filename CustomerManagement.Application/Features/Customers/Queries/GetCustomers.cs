using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CustomerManagement.Core.Entities;
using CustomerManagement.Core.Interfaces;
using CustomerManagement.Shared.DTOs;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Application.Features.Customers.Queries
{
    public static class GetCustomers
    {
        public class Query : IRequest<List<CustomerDto>>
        {
        }

        public class Handler : IRequestHandler<Query, List<CustomerDto>>
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

            public async Task<List<CustomerDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var customers = await _customerRepository.GetAllAsync();
                    return _mapper.Map<List<CustomerDto>>(customers);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while getting all customers");
                    throw;
                }
            }
        }
    }
}
