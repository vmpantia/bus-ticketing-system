using AutoMapper;
using BTS.Domain.Models.Entities;

namespace BTS.Core.Mappings
{
    public class RouteLite : Profile
    {
        public RouteLite()
        {
            CreateMap<Route, RouteLite>();
        }
    }
}
