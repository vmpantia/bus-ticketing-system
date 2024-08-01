using AutoMapper;
using BTS.Core.Commands.Models.Driver;
using BTS.Domain.Extensions;
using BTS.Domain.Models.Dtos.Driver;
using BTS.Domain.Models.Entities;
using BTS.Domain.Models.Enums;
using BTS.Domain.Models.Lites;

namespace BTS.Core.Mappings
{
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<Driver, DriverDto>()
                .ForMember(dst => dst.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dst => dst.LastUpdateAt, opt => opt.MapFrom(src => src.UpdatedAt ?? src.CreatedAt))
                .ForMember(dst => dst.LastUpdateBy, opt => opt.MapFrom(src => src.UpdatedBy ?? src.CreatedBy));
            CreateMap<Driver, DriverLite>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => string.Concat(src.FirstName, " ", src.LastName)));
            CreateMap<CreateDriverCommand, Driver>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dst => dst.Status, opt => opt.MapFrom(src => CommonStatus.Active))
                .ForMember(dst => dst.Birthdate, opt => opt.MapFrom(src => src.Birthdate.Date))
                .ForMember(dst => dst.CreatedAt, opt => opt.MapFrom(src => DateTimeExtension.GetCurrentDateTimeOffsetUtc()))
                .ForMember(dst => dst.CreatedBy, opt => opt.MapFrom(src => src.Requestor));
            CreateMap<UpdateDriverCommand, Driver>()
                .ForMember(dst => dst.Birthdate, opt => opt.MapFrom(src => src.Birthdate.Date))
                .ForMember(dst => dst.UpdatedAt, opt => opt.MapFrom(src => DateTimeExtension.GetCurrentDateTimeOffsetUtc()))
                .ForMember(dst => dst.UpdatedBy, opt => opt.MapFrom(src => src.Requestor));
        }
    }
}
