using AutoMapper;
using BuyandRentHomeWebAPI.Data.Interfaces;
using BuyandRentHomeWebAPI.Dtos;
using BuyandRentHomeWebAPI.Models;
using BuyandRentHomeWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISharedService _sharedService;

        public PropertyService(IMapper mapper, IUnitOfWork unitOfWork, ISharedService sharedService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _sharedService = sharedService;
        }

        public async Task<IEnumerable<PropertyListDto>> GetPropertyList(int sellRent)
        {
            var properties = await _unitOfWork.PropertyRepository.GetAll(
                expression: q => q.SellRent == sellRent,
                orderBy: x => x.OrderBy(q => q.PostedOn),
                includes: new List<string> { "PropertyType", "FurnishingType", "City", "Country" });
            var propertyListDto = _mapper.Map<IEnumerable<PropertyListDto>>(properties);
            return propertyListDto;
        }

        public async Task<IEnumerable<PropertyListDto>> GetMyPropertyList()
        {
            var myUserId = _sharedService.GetUserId();
            var properties = await _unitOfWork.PropertyRepository.GetAll(
                expression: q => q.PostedBy == myUserId,
                includes: new List<string> { "PropertyType", "FurnishingType", "City", "Country" });
            var propertyListDto = _mapper.Map<IEnumerable<PropertyListDto>>(properties);
            return propertyListDto;
        }

        public async Task<PropertyDetailDto> GetPropertyDetail(int id)
        {
            var property = await _unitOfWork.PropertyRepository.Get(
                expression: x => x.Id == id, 
                includes: new List<string> { "PropertyType", "FurnishingType", "City", "Country" });
            var propertyDto = _mapper.Map<PropertyDetailDto>(property);
            return propertyDto;
        }

        public async Task<int> AddNewProperty(PropertyCreateUpdateDto propertyCreateUpdateDto)
        {
            var property = _mapper.Map<Property>(propertyCreateUpdateDto);
            property.PostedOn = DateTime.Now;
            property.PostedBy = _sharedService.GetUserId();
            property.LastUpdatedOn = property.PostedOn;
            property.LastUpdatedBy = property.PostedBy;

            await _unitOfWork.PropertyRepository.Insert(property);
            await _unitOfWork.SaveAsync();

            return property.Id;
        }

    }
}
