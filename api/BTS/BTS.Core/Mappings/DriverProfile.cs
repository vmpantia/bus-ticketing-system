using AutoMapper;
using BTS.Domain.Models.Dtos.Driver;
using BTS.Domain.Models.Entities;

namespace BTS.Core.Mappings
{
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<Driver, DriverDto>()
                .ForMember(dst => dst.BusId, opt => opt.MapFrom(src => src.Bus.Id))
                .ForMember(dst => dst.BusNo, opt => opt.MapFrom(src => src.Bus.BusNo))
                .ForMember(dst => dst.BusPlateNo, opt => opt.MapFrom(src => src.Bus.PlateNo));
        }
    }
}
