using AutoMapper;
using BuyandRentHomeWebAPI.Data.Interfaces;
using BuyandRentHomeWebAPI.Dtos;
using BuyandRentHomeWebAPI.Models;
using BuyandRentHomeWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
                orderBy: x => x.OrderByDescending(q => q.PostedOn),
                includes: new List<string> { "PropertyType", "FurnishingType", "City", "Country" });
            var propertyListDto = _mapper.Map<IEnumerable<PropertyListDto>>(properties);
            return propertyListDto;
        }

        public async Task<PageResult<PropertyListDto>> GetMyPropertyPaginatedList(PaginationParameter paginationParameter)
        {
            var myUserId = _sharedService.GetUserId();

            var includeList = new Expression<Func<Property, object>>[]
            {
                x => x.PropertyType,
                x => x.FurnishingType,
                x => x.City,
                x => x.Country,
            };

            var paginatedPropertyResult = await _unitOfWork.PropertyRepository.GetPaginateList(
                paginationParameter.CurrentPageNo, paginationParameter.PageSize,
                filter: q => q.PostedBy == myUserId,
                orderBy: x => x.OrderByDescending(q => q.PostedOn),
                includes: includeList
                );
            //var paginatedResult = _mapper.Map<PageResult<PropertyListDto>>(paginatedPropertyResult);
            var paginatedResult = new PageResult<PropertyListDto>
            {
                PageNo = paginatedPropertyResult.PageNo,
                PageSize = paginatedPropertyResult.PageSize,
                TotalPages = paginatedPropertyResult.TotalPages,
                TotalRecords = paginatedPropertyResult.TotalRecords,
                ResultList = _mapper.Map<List<PropertyListDto>>(paginatedPropertyResult.ResultList)
            };

            return paginatedResult;
        }

        public async Task<PropertyDetailDto> GetPropertyDetail(int id)
        {
            var property = await _unitOfWork.PropertyRepository.Get(
                expression: x => x.Id == id,
                includes: new List<string> { "PropertyType", "FurnishingType", "City", "Country", "Photos" });
            var propertyDto = _mapper.Map<PropertyDetailDto>(property);
            return propertyDto;
        }

        public async Task<int> AddNewProperty(PropertyCreateUpdateDto propertyCreateUpdateDto)
        {
            var property = _mapper.Map<Property>(propertyCreateUpdateDto);

            if (propertyCreateUpdateDto.Id > 0)
            {
                property = await _unitOfWork.PropertyRepository.Get(x => x.Id == propertyCreateUpdateDto.Id);
                _mapper.Map(propertyCreateUpdateDto, property);
                property.LastUpdatedOn = DateTime.Now;
                property.LastUpdatedBy = _sharedService.GetUserId();
                _unitOfWork.PropertyRepository.Update(property);
            }
            else
            {
                property.PostedOn = DateTime.Now;
                property.PostedBy = _sharedService.GetUserId();
                property.LastUpdatedOn = DateTime.Now;
                property.LastUpdatedBy = _sharedService.GetUserId();

                await _unitOfWork.PropertyRepository.Insert(property);
            }

            await _unitOfWork.SaveAsync();

            return property.Id;
        }

        public async Task<bool> DeleteProperty(int id)
        {
            await _unitOfWork.PropertyRepository.Delete(id);
            return await _unitOfWork.SaveAsync();
        }
    }
}
