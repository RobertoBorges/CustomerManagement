using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CustomerManagement.Api.Controllers;
using CustomerManagement.Application.Features.Customers.Commands;
using CustomerManagement.Application.Features.Customers.Queries;
using CustomerManagement.Shared.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CustomerManagement.Api.Tests.Controllers
{
    public class CustomersControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly CustomersController _controller;

        public CustomersControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new CustomersController(_mockMediator.Object);
        }

        [Fact]
        public async Task GetCustomers_ReturnsOkResult_WithListOfCustomers()
        {
            // Arrange
            var customers = new List<CustomerDto>
            {
                new CustomerDto { 
                    CustomerId = "CM001", 
                    FirstName = "John", 
                    LastName = "Doe", 
                    Email = "john.doe@example.com",
                    PhoneNumber = "1234567890",
                    StreetAddress = "123 Main St",
                    City = "Anytown",
                    StateProvince = "State",
                    PostalCode = "12345",
                    Country = "Country",
                    CustomerType = "Retail",
                    PaymentMethod = "Credit Card",
                    Status = "Active",
                    RegistrationDate = DateTime.Now
                },
                new CustomerDto { 
                    CustomerId = "CM002", 
                    FirstName = "Jane", 
                    LastName = "Smith", 
                    Email = "jane.smith@example.com",
                    PhoneNumber = "0987654321",
                    StreetAddress = "456 Oak St",
                    City = "Othertown",
                    StateProvince = "State",
                    PostalCode = "67890",
                    Country = "Country",
                    CustomerType = "Business",
                    PaymentMethod = "Bank Transfer",
                    Status = "Active",
                    RegistrationDate = DateTime.Now
                }
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetCustomers.Query>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(customers);

            // Act
            var result = await _controller.GetCustomers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCustomers = Assert.IsType<List<CustomerDto>>(okResult.Value);
            Assert.Equal(2, returnedCustomers.Count);
        }

        [Fact]
        public async Task GetCustomer_ReturnsOkResult_WithCustomer_WhenCustomerExists()
        {
            // Arrange
            var customerId = "CM001";
            var customer = new CustomerDto 
            { 
                CustomerId = customerId, 
                FirstName = "John", 
                LastName = "Doe", 
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                StreetAddress = "123 Main St",
                City = "Anytown",
                StateProvince = "State",
                PostalCode = "12345",
                Country = "Country",
                CustomerType = "Retail",
                PaymentMethod = "Credit Card",
                Status = "Active",
                RegistrationDate = DateTime.Now
            };

            _mockMediator
                .Setup(m => m.Send(It.Is<GetCustomerById.Query>(q => q.CustomerId == customerId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(customer);

            // Act
            var result = await _controller.GetCustomer(customerId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCustomer = Assert.IsType<CustomerDto>(okResult.Value);
            Assert.Equal(customerId, returnedCustomer.CustomerId);
        }

        [Fact]
        public async Task GetCustomer_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            // Arrange
            var customerId = "CM999";
            
            _mockMediator
                .Setup(m => m.Send(It.Is<GetCustomerById.Query>(q => q.CustomerId == customerId), It.IsAny<CancellationToken>()))
                .ReturnsAsync((CustomerDto)null);

            // Act
            var result = await _controller.GetCustomer(customerId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
        
        [Fact]
        public async Task CreateCustomer_ReturnsCreatedAtAction_WithCustomer()
        {
            // Arrange
            var customerDto = new CreateUpdateCustomerDto
            {
                CustomerId = "CM003",
                FirstName = "New",
                LastName = "Customer",
                Email = "new.customer@example.com",
                PhoneNumber = "5556667777",
                StreetAddress = "789 New St",
                City = "Newtown",
                StateProvince = "State",
                PostalCode = "54321",
                Country = "Country",
                CustomerType = "Retail",
                PaymentMethod = "Credit Card",
                Status = "Active",
                RegistrationDate = DateTime.Now
            };

            var createdCustomer = new CustomerDto
            {
                CustomerId = "CM003",
                FirstName = "New",
                LastName = "Customer",
                Email = "new.customer@example.com",
                PhoneNumber = "5556667777",
                StreetAddress = "789 New St",
                City = "Newtown",
                StateProvince = "State",
                PostalCode = "54321",
                Country = "Country",
                CustomerType = "Retail",
                PaymentMethod = "Credit Card",
                Status = "Active",
                RegistrationDate = DateTime.Now
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<CreateCustomer.Command>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(createdCustomer);

            // Act
            var result = await _controller.CreateCustomer(customerDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(CustomersController.GetCustomer), createdAtActionResult.ActionName);
            Assert.Equal("CM003", createdAtActionResult.RouteValues["id"]);
            
            var returnedCustomer = Assert.IsType<CustomerDto>(createdAtActionResult.Value);
            Assert.Equal("CM003", returnedCustomer.CustomerId);
            Assert.Equal("New", returnedCustomer.FirstName);
            Assert.Equal("Customer", returnedCustomer.LastName);
        }

        [Fact]
        public async Task UpdateCustomer_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var customerId = "CM001";
            var customerDto = new CreateUpdateCustomerDto
            {
                CustomerId = customerId,
                FirstName = "Updated",
                LastName = "Customer",
                Email = "updated.customer@example.com",
                PhoneNumber = "1112223333",
                StreetAddress = "101 Update St",
                City = "Updatetown",
                StateProvince = "State",
                PostalCode = "11111",
                Country = "Country",
                CustomerType = "Retail",
                PaymentMethod = "Credit Card",
                Status = "Active",
                RegistrationDate = DateTime.Now
            };

            _mockMediator
                .Setup(m => m.Send(It.Is<UpdateCustomer.Command>(c => c.CustomerId == customerId && c.CustomerDto == customerDto), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateCustomer(customerId, customerDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateCustomer_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            // Arrange
            var customerId = "CM999";
            var customerDto = new CreateUpdateCustomerDto
            {
                CustomerId = customerId,
                FirstName = "Nonexistent",
                LastName = "Customer",
                Email = "nonexistent@example.com",
                PhoneNumber = "9998887777",
                StreetAddress = "999 Missing St",
                City = "Missingtown",
                StateProvince = "State",
                PostalCode = "99999",
                Country = "Country",
                CustomerType = "Retail",
                PaymentMethod = "Credit Card",
                Status = "Active",
                RegistrationDate = DateTime.Now
            };

            _mockMediator
                .Setup(m => m.Send(It.Is<UpdateCustomer.Command>(c => c.CustomerId == customerId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.UpdateCustomer(customerId, customerDto);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteCustomer_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            var customerId = "CM001";
            
            _mockMediator
                .Setup(m => m.Send(It.Is<DeleteCustomer.Command>(c => c.CustomerId == customerId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteCustomer(customerId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteCustomer_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            // Arrange
            var customerId = "CM999";
            
            _mockMediator
                .Setup(m => m.Send(It.Is<DeleteCustomer.Command>(c => c.CustomerId == customerId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteCustomer(customerId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}