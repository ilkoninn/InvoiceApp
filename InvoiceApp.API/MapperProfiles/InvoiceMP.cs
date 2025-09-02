using AutoMapper;
using InvoiceApp.API.DTOs.InvoiceDTOs;
using InvoiceApp.API.Entities;

namespace InvoiceApp.API.MapperProfiles
{
    public class InvoiceMP : Profile
    {
        public InvoiceMP()
        {
            CreateMap<Invoice, InvoiceDTO>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Title))
                .ForMember(dest => dest.InvoiceLines, opt => opt.MapFrom(src => src.InvoiceLines))
                .ReverseMap();

            CreateMap<InvoiceLine, InvoiceLineDTO>().ReverseMap();

            CreateMap<Invoice, CreateInvoiceDTO>().ReverseMap();
            CreateMap<InvoiceLine, CreateInvoiceLineDTO>().ReverseMap();

            CreateMap<Invoice, UpdateInvoiceDTO>().ReverseMap();
            CreateMap<InvoiceLine, UpdateInvoiceLineDTO>().ReverseMap();
        }
    }

}
