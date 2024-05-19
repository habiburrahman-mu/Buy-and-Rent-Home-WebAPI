using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Models;

namespace Application.MappingProfiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<City, CityDto>().ReverseMap();

        CreateMap<City, CityUpdateDto>().ReverseMap();

        CreateMap<Property, PropertyListDto>()
            .ForMember(d => d.City, opt => opt.MapFrom(src => src.City.Name))
            .ForMember(d => d.Country, opt => opt.MapFrom(src => src.Country.Name))
            .ForMember(d => d.PropertyType, opt => opt.MapFrom(src => src.PropertyType.Name))
            .ForMember(d => d.FurnishingType, opt => opt.MapFrom(src => src.FurnishingType.Name))
            .ForMember(d => d.PrimaryPhoto, opt => opt.MapFrom(src => src.Photos.Where(x => x.IsPrimary).FirstOrDefault().ImageUrl));

        CreateMap<Property, PropertyDetailDto>()
            .ForMember(d => d.City, opt => opt.MapFrom(src => src.City.Name))
            .ForMember(d => d.CityLatitude, opt => opt.MapFrom(src => src.City.Latitude))
            .ForMember(d => d.CityLongitude, opt => opt.MapFrom(src => src.City.Longitude))
            .ForMember(d => d.Country, opt => opt.MapFrom(src => src.Country.Name))
            .ForMember(d => d.PropertyType, opt => opt.MapFrom(src => src.PropertyType.Name))
            .ForMember(d => d.FurnishingType, opt => opt.MapFrom(src => src.FurnishingType.Name));

        CreateMap<PropertyCreateUpdateDto, Property>()
            .ForMember(d => d.AvailableStartTime, opt => opt.MapFrom(src => TimeOnly.FromTimeSpan(src.AvailableStartTime)))
            .ForMember(d => d.AvailableEndTime, opt => opt.MapFrom(src => TimeOnly.FromTimeSpan(src.AvailableEndTime)))
            .ReverseMap();

        CreateMap<PropertyType, KeyValuePairDto>().ReverseMap();
        CreateMap<FurnishingType, KeyValuePairDto>().ReverseMap();
        CreateMap<Country, CountryDto>().ReverseMap();
        CreateMap<Photo, PhotoDto>().ReverseMap();
        CreateMap<Role, RoleDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<UserPrivilege, UserPrivilegeDto>()
            .ForMember(d => d.RoleName, opt => opt.MapFrom(src => src.Role.Name));
        CreateMap<UserPrivilegeDto, UserPrivilege>();
        CreateMap<VisitingRequestCreateDto, VisitingRequest>();
        CreateMap<VisitingRequest, VisitingRequestDetailDto>();
    }
}
