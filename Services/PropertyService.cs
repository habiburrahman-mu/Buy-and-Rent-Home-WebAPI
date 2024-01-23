using AutoMapper;
using BuyAndRentHomeWebAPI.Data.Interfaces;
using BuyAndRentHomeWebAPI.Data.Entities;
using BuyAndRentHomeWebAPI.Dtos;
using BuyAndRentHomeWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BuyAndRentHomeWebAPI.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISharedService _sharedService;
        private readonly IPhotoService _photoService;

        public PropertyService(IMapper mapper, IUnitOfWork unitOfWork, ISharedService sharedService, IPhotoService photoService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _sharedService = sharedService;
            _photoService = photoService;
        }

        public async Task<List<PropertyListDto>> GetPropertyList(int sellRent)
        {
            var properties = await _unitOfWork.PropertyRepository.GetAll(
                expression: q => q.SellRent == sellRent,
                orderBy: x => x.OrderBy(q => q.PostedOn),
                includes: new List<string> { "PropertyType", "FurnishingType", "City", "Country", "Photo" });
            var propertyListDto = _mapper.Map<List<PropertyListDto>>(properties);
            return propertyListDto;
        }

        public async Task<List<PropertyListDto>> GetMyPropertyList()
        {
            var myUserId = _sharedService.GetUserId();

            var includeList = new Expression<Func<Property, object>>[]
            {
                x => x.PropertyType,
                x => x.FurnishingType,
                x => x.City,
                x => x.Country,
                x => x.Photos
            };

            var properties = await _unitOfWork.PropertyRepository.GetAll(
                expression: q => q.PostedBy == myUserId,
                orderBy: x => x.OrderByDescending(q => q.PostedOn),
                includes: includeList);
            var propertyListDto = _mapper.Map<List<PropertyListDto>>(properties);
            return propertyListDto;
        }

        public async Task<PageResult<PropertyListDto>> GetPropertyPaginatedList(PaginationParameter paginationParameter, int sellRent)
        {
            var includeList = new Expression<Func<Property, object>>[]
            {
                x => x.PropertyType,
                x => x.FurnishingType,
                x => x.City,
                x => x.Country,
                x => x.Photos
            };

            var paginatedPropertyResult = await _unitOfWork.PropertyRepository.GetPaginateList(
                paginationParameter.CurrentPageNo, paginationParameter.PageSize,
                filter: q => q.SellRent == sellRent,
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

        public async Task<PageResult<PropertyListDto>> GetMyPropertyPaginatedList(PaginationParameter paginationParameter)
        {
            var myUserId = _sharedService.GetUserId();

            var includeList = new Expression<Func<Property, object>>[]
            {
                x => x.PropertyType,
                x => x.FurnishingType,
                x => x.City,
                x => x.Country,
                x => x.Photos
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
                property.LastUpdatedOn = DateTime.UtcNow;
                property.LastUpdatedBy = _sharedService.GetUserId();
                _unitOfWork.PropertyRepository.Update(property);
            }
            else
            {
                property.PostedOn = DateTime.UtcNow;
                property.PostedBy = _sharedService.GetUserId();
                property.LastUpdatedOn = DateTime.UtcNow;
                property.LastUpdatedBy = _sharedService.GetUserId();

                await _unitOfWork.PropertyRepository.Insert(property);
            }

            await _unitOfWork.SaveAsync();

            return property.Id;
        }

        public async Task<bool> DeleteProperty(int id)
        {
            var photoList = await _unitOfWork.PhotoRepository.GetAll(x => x.PropertyId == id);
            await _unitOfWork.PropertyRepository.Delete(id);
            var result = await _unitOfWork.SaveAsync();
            if (result && photoList != null && photoList.Any())
            {
                foreach (var photo in photoList)
                {
                    _photoService.DeleteFileFromPath(photo.ImageUrl);
                }
            }
            return result;
        }

        public async Task<List<DayAvailability>> GetAvailableSlotsForNext7Days(int propertyId)
        {
            var property = await _unitOfWork.PropertyRepository.Get(x => x.Id == propertyId);
            var availableDays = property.AvailableDays.Split(',').Select(x => x).ToList();
            List<DayAvailability> dayAvailabilityList = await CreateDayAvailabilityList(propertyId, property, availableDays);

            return dayAvailabilityList;
        }

        private async Task<List<DayAvailability>> CreateDayAvailabilityList(int propertyId, Property property, List<string> availableDays)
        {
            var dayAvailabilityList = new List<DayAvailability>();

            var tomorrow = DateTime.UtcNow.AddDays(1);
            var endDate = tomorrow.AddDays(7);

            var takenTimeSlotList = await _unitOfWork.VisitingRequestRepository.GetAll(x => x.PropertyId == propertyId && tomorrow <= x.DateOn && x.DateOn <= endDate);

            for (var currentDate = tomorrow; currentDate < endDate; currentDate = currentDate.AddDays(1))
            {
                var existingSchedules = takenTimeSlotList.Where(x => x.DateOn.Date == currentDate.Date).ToList();
                var takenStartTimeList = existingSchedules.Select(x => x.StartTime.TimeOfDay).ToList();

                if (availableDays.Contains(currentDate.DayOfWeek.ToString()))
                {
                    List<TimeSlot> timeSlotList = CreateTimeSlotList(property, takenStartTimeList);
                    var dayAvailability = new DayAvailability
                    {
                        Date = currentDate,
                        Day = currentDate.DayOfWeek.ToString(),
                        AvailableTimeSlots = timeSlotList
                    };
                    dayAvailabilityList.Add(dayAvailability);
                }
            }

            return dayAvailabilityList;
        }

        private static List<TimeSlot> CreateTimeSlotList(Property property, List<TimeSpan> takenStartTimeList)
        {
            var timeSlotList = new List<TimeSlot>();

            for (var startTime = property.AvailableStartTime; startTime < property.AvailableEndTime; startTime = startTime.Add(TimeSpan.FromMinutes(30)))
            {
                if (!takenStartTimeList.Contains(startTime))
                {
                    var timeSlot = new TimeSlot
                    {
                        Start = startTime,
                        End = startTime.Add(TimeSpan.FromMinutes(30))
                    };
                    timeSlotList.Add(timeSlot);
                }

            }

            return timeSlotList;
        }
    }
}
