using AutoMapper;
using BTS.Core.Commands.Models;
using BTS.Domain.Models.Dtos.Driver;
using BTS.Domain.Models.Entities;

namespace BTS.Core.Mappings
{
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<Driver, DriverDto>()
                .ForMember(dst => dst.LastUpdateAt, opt => opt.MapFrom(src => src.UpdatedAt ?? src.CreatedAt))
                .ForMember(dst => dst.LastUpdateBy, opt => opt.MapFrom(src => src.UpdatedBy ?? src.CreatedBy));
            CreateMap<CreateDriverCommand, Driver>()
                .ForMember(dst => dst.Birthdate, opt => opt.MapFrom(src => src.Birthdate.Date));
            CreateMap<UpdateDriverCommand, Driver>()
                .ForMember(dst => dst.Birthdate, opt => opt.MapFrom(src => src.Birthdate.Date));
        }
    }
}
