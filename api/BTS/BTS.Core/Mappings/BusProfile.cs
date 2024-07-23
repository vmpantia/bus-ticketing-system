using AutoMapper;
using BTS.Domain.Models.Entities;
using BTS.Domain.Models.Lites;

namespace BTS.Core.Mappings
{
    public class BusProfile : Profile
    {
        public BusProfile()
        {
            CreateMap<Bus, BusLite>();
        }
    }
}
