using AutoMapper;
using CustomerManagement.Core.Entities;
using CustomerManagement.Shared.DTOs;
using System;

namespace CustomerManagement.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Entity to DTO
            CreateMap<Customer, CustomerDto>();

            // DTO to Entity
            CreateMap<CreateUpdateCustomerDto, Customer>()
                .ForMember(dest => dest.CustomerId, opt => opt.Ignore()) // ID is managed by the repository
                .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => 
                    src.RegistrationDate == default ? DateTime.UtcNow : src.RegistrationDate));
        }
    }
}
