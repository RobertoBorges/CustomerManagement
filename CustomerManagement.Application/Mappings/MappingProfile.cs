using AutoMapper;
using CustomerManagement.Core.Entities;
using CustomerManagement.Shared.DTOs;

namespace CustomerManagement.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Customer -> CustomerDto
            CreateMap<Customer, CustomerDto>();
            
            // CreateUpdateCustomerDto -> Customer
            CreateMap<CreateUpdateCustomerDto, Customer>();
            
            // CustomerDto -> CreateUpdateCustomerDto
            CreateMap<CustomerDto, CreateUpdateCustomerDto>();
        }
    }
}
