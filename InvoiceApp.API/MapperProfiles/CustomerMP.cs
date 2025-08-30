using AutoMapper;
using InvoiceApp.API.DTOs.CustomerDTOs;
using InvoiceApp.API.Entities;

namespace InvoiceApp.API.MapperProfiles
{
    public class CustomerMP : Profile
    {
        public CustomerMP()
        {
            CreateMap<Customer, CustomerDTO>()
                .ForMember(dest => dest.CreatedByFullName, opt => opt.MapFrom(src => src.CreatedBy.FullName))
                .ReverseMap();

            CreateMap<Customer, CreateCustomerDTO>().ReverseMap();
            CreateMap<Customer, UpdateCustomerDTO>().ReverseMap();
        }
    }
}
