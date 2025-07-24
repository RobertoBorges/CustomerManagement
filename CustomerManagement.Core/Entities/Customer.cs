using System;

namespace CustomerManagement.Core.Entities
{
    public class Customer
    {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string StreetAddress { get; set; } = null!;
        public string City { get; set; } = null!;
        public string StateProvince { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string? CompanyName { get; set; }
        public string CustomerType { get; set; } = null!;
        public DateTime RegistrationDate { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public string? Notes { get; set; }
        public string Status { get; set; } = null!;
        
        // Derived properties
        public string FullName => $"{FirstName} {LastName}";
        public string FullAddress => $"{StreetAddress}, {City}, {StateProvince} {PostalCode}, {Country}";
        public bool IsActive => Status.Equals("Active", StringComparison.OrdinalIgnoreCase);
    }
}
