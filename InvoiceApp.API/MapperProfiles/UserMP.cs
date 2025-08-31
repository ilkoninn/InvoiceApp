using AutoMapper;
using InvoiceApp.API.DTOs.AuthDTOs;
using InvoiceApp.API.Entities.Indenties;

namespace InvoiceApp.API.MapperProfiles
{
    public class UserMP : Profile
    {
        public UserMP()
        {
            CreateMap<RegisterDTO, User>().ReverseMap();
        }
    }
}
