using AutoMapper;
using BTS.Core.Commands.Models.User;
using BTS.Domain.Extensions;
using BTS.Domain.Models.Dtos.User;
using BTS.Domain.Models.Entities;
using BTS.Domain.Models.Enums;

namespace BTS.Core.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dst => dst.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dst => dst.LastUpdateAt, opt => opt.MapFrom(src => src.UpdatedAt ?? src.CreatedAt))
                .ForMember(dst => dst.LastUpdateBy, opt => opt.MapFrom(src => src.UpdatedBy ?? src.CreatedBy));
            CreateMap<CreateUserCommand, User>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dst => dst.IsEmailConfirmed, opt => opt.MapFrom(src => false))
                .ForMember(dst => dst.Status, opt => opt.MapFrom(src => CommonStatus.Active))
                .ForMember(dst => dst.CreatedAt, opt => opt.MapFrom(src => DateTimeExtension.GetCurrentDateTimeOffsetUtc()))
                .ForMember(dst => dst.CreatedBy, opt => opt.MapFrom(src => src.Requestor));
        }
    }
}
