using AutoMapper;
using BTS.Domain.Models.Dtos.Driver;
using BTS.Domain.Models.Entities;

namespace BTS.Core.Mappings
{
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<Driver, DriverDto>();
        }
    }
}
