using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Shared.DTOs
{
    public class CreateUpdateCustomerDto
    {
        [Required(ErrorMessage = "Customer ID is required")]
        public string CustomerId { get; set; } = null!;

        [Required(ErrorMessage = "First name is required")]
        [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(100, ErrorMessage = "Last name cannot exceed 100 characters")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Street address is required")]
        public string StreetAddress { get; set; } = null!;

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "State/Province is required")]
        public string StateProvince { get; set; } = null!;

        [Required(ErrorMessage = "Postal code is required")]
        public string PostalCode { get; set; } = null!;

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; } = null!;

        public string? CompanyName { get; set; }

        [Required(ErrorMessage = "Customer type is required")]
        public string CustomerType { get; set; } = null!;

        [Required(ErrorMessage = "Registration date is required")]
        public DateTime RegistrationDate { get; set; }

        [Required(ErrorMessage = "Payment method is required")]
        public string PaymentMethod { get; set; } = null!;

        public string? Notes { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; } = null!;
    }
}
