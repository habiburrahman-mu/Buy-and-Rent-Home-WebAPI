using AutoMapper;
using BuyAndRentHomeWebAPI.Data.Entities;
using BuyAndRentHomeWebAPI.Dtos;
using System.Linq;

namespace BuyAndRentHomeWebAPI.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
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
                .ForMember(d => d.CityLattitude, opt => opt.MapFrom(src => src.City.Lattitude))
                .ForMember(d => d.CityLongitude, opt => opt.MapFrom(src => src.City.Longitude))
                .ForMember(d => d.Country, opt => opt.MapFrom(src => src.Country.Name))
                .ForMember(d => d.PropertyType, opt => opt.MapFrom(src => src.PropertyType.Name))
                .ForMember(d => d.FurnishingType, opt => opt.MapFrom(src => src.FurnishingType.Name));

            CreateMap<PropertyCreateUpdateDto, Property>().ReverseMap();

            CreateMap<PropertyType, KeyValuePairDto>().ReverseMap();
            CreateMap<FurnishingType, KeyValuePairDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Photo, PhotoDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserPrivilege, UserPrivilegeDto>()
                .ForMember(d => d.RoleName, opt => opt.MapFrom(src => src.Role.Name));
            CreateMap<VisitingRequestCreateDto, VisitingRequest>();
            CreateMap<VisitingRequest, VisitingRequestDetailDto>();
        }
    }
}
