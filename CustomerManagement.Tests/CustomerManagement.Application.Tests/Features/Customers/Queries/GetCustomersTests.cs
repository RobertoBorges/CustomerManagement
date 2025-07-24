using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CustomerManagement.Application.Features.Customers.Queries;
using CustomerManagement.Core.Entities;
using CustomerManagement.Core.Interfaces;
using CustomerManagement.Shared.DTOs;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CustomerManagement.Application.Tests.Features.Customers.Queries
{
    public class GetCustomersTests
    {
        private readonly Mock<ICustomerRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<GetCustomers.Handler>> _mockLogger;
        private readonly GetCustomers.Handler _handler;

        public GetCustomersTests()
        {
            _mockRepository = new Mock<ICustomerRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<GetCustomers.Handler>>();
            _handler = new GetCustomers.Handler(_mockRepository.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnAllCustomers()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer
                {
                    CustomerId = "CM001",
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com"
                },
                new Customer
                {
                    CustomerId = "CM002",
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com"
                }
            };

            var customerDtos = new List<CustomerDto>
            {
                new CustomerDto
                {
                    CustomerId = "CM001",
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com"
                },
                new CustomerDto
                {
                    CustomerId = "CM002",
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com"
                }
            };

            _mockRepository.Setup(r => r.GetAllAsync())
                .ReturnsAsync(customers);
            
            _mockMapper.Setup(m => m.Map<List<CustomerDto>>(It.IsAny<List<Customer>>()))
                .Returns(customerDtos);

            // Act
            var result = await _handler.Handle(new GetCustomers.Query(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}
