using CustomerManagement.Application.Features.Customers.Commands;
using CustomerManagement.Shared.DTOs;
using FluentValidation;

namespace CustomerManagement.Application.Validators
{
    public class CreateUpdateCustomerDtoValidator : AbstractValidator<CreateUpdateCustomerDto>
    {
        public CreateUpdateCustomerDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(10).WithMessage("{PropertyName} must not exceed 10 characters");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .EmailAddress().WithMessage("Invalid email address")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters");

            RuleFor(x => x.StreetAddress)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters");

            RuleFor(x => x.StateProvince)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters");

            RuleFor(x => x.PostalCode)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(20).WithMessage("{PropertyName} must not exceed 20 characters");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters");

            RuleFor(x => x.CompanyName)
                .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters");

            RuleFor(x => x.CustomerType)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters");

            RuleFor(x => x.RegistrationDate)
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(x => x.PaymentMethod)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters");

            RuleFor(x => x.Notes)
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(20).WithMessage("{PropertyName} must not exceed 20 characters");
        }
    }
    
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomer.Command>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.CustomerDto).SetValidator(new CreateUpdateCustomerDtoValidator());
        }
    }
    
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomer.Command>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Customer ID is required");
            
            RuleFor(x => x.CustomerDto).SetValidator(new CreateUpdateCustomerDtoValidator());
        }
    }
}
