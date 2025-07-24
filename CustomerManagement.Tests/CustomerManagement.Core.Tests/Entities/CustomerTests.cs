using System;
using Xunit;
using CustomerManagement.Core.Entities;

namespace CustomerManagement.Core.Tests.Entities
{
    public class CustomerTests
    {
        [Fact]
        public void Customer_PropertiesShouldBeSetCorrectly()
        {
            // Arrange & Act
            var customer = new Customer
            {
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
                RegistrationDate = new DateTime(2023, 1, 1)
            };

            // Assert
            Assert.Equal("CM001", customer.CustomerId);
            Assert.Equal("John", customer.FirstName);
            Assert.Equal("Doe", customer.LastName);
            Assert.Equal("john.doe@example.com", customer.Email);
            Assert.Equal("1234567890", customer.PhoneNumber);
            Assert.Equal("123 Main St", customer.StreetAddress);
            Assert.Equal("Anytown", customer.City);
            Assert.Equal("State", customer.StateProvince);
            Assert.Equal("12345", customer.PostalCode);
            Assert.Equal("Country", customer.Country);
            Assert.Equal("Retail", customer.CustomerType);
            Assert.Equal("Credit Card", customer.PaymentMethod);
            Assert.Equal("Active", customer.Status);
            Assert.Equal(new DateTime(2023, 1, 1), customer.RegistrationDate);
        }

        [Fact]
        public void Customer_FullName_ShouldCombineFirstAndLastName()
        {
            // Arrange
            var customer = new Customer
            {
                FirstName = "John",
                LastName = "Doe"
            };

            // Act & Assert
            Assert.Equal("John Doe", customer.FullName);
        }

        [Fact]
        public void Customer_FullAddress_ShouldCombineAddressComponents()
        {
            // Arrange
            var customer = new Customer
            {
                StreetAddress = "123 Main St",
                City = "Anytown",
                StateProvince = "State",
                PostalCode = "12345",
                Country = "Country"
            };

            // Act & Assert
            Assert.Equal("123 Main St, Anytown, State 12345, Country", customer.FullAddress);
        }

        [Fact]
        public void Customer_IsActive_ShouldReturnTrueForActiveStatus()
        {
            // Arrange
            var activeCustomer = new Customer { Status = "Active" };
            var inactiveCustomer = new Customer { Status = "Inactive" };

            // Act & Assert
            Assert.True(activeCustomer.IsActive);
            Assert.False(inactiveCustomer.IsActive);
        }
    }
}