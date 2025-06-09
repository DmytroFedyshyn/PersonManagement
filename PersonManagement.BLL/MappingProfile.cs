using AutoMapper;
using PersonManagement.BLL.DTOs;
using PersonManagement.DAL.Entities;

namespace PersonManagement.BLL;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PersonCreateDto, Person>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src =>
                string.IsNullOrWhiteSpace(src.City) || string.IsNullOrWhiteSpace(src.AddressLine)
                    ? null
                    : new Address { City = src.City!, AddressLine = src.AddressLine! }
            ));
    }
}
