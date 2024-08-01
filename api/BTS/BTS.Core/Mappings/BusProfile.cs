using AutoMapper;
using BTS.Core.Commands.Models;
using BTS.Core.Commands.Models.Bus;
using BTS.Core.Commands.Models.Driver;
using BTS.Domain.Extensions;
using BTS.Domain.Models.Dtos.Bus;
using BTS.Domain.Models.Entities;
using BTS.Domain.Models.Enums;
using BTS.Domain.Models.Lites;

namespace BTS.Core.Mappings
{
    public class BusProfile : Profile
    {
        public BusProfile()
        {
            CreateMap<Bus, BusDto>()
                .ForMember(dst => dst.LastUpdateAt, opt => opt.MapFrom(src => src.UpdatedAt ?? src.CreatedAt))
                .ForMember(dst => dst.LastUpdateBy, opt => opt.MapFrom(src => src.UpdatedBy ?? src.CreatedBy));
            CreateMap<Bus, BusLite>();
            CreateMap<CreateBusCommand, Bus>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dst => dst.Status, opt => opt.MapFrom(src => CommonStatus.Active))
                .ForMember(dst => dst.CreatedAt, opt => opt.MapFrom(src => DateTimeExtension.GetCurrentDateTimeOffsetUtc()))
                .ForMember(dst => dst.CreatedBy, opt => opt.MapFrom(src => src.Requestor));
        }
    }
}
