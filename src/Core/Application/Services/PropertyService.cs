using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Common.Constants;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces.Data;
using Domain.Interfaces.Services;
using System.Linq.Expressions;

namespace Application.Services;

public class PropertyService : IPropertyService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPhotoService _photoService;
    private readonly IFileService fileService;

    public PropertyService(IMapper mapper, IUnitOfWork unitOfWork, IPhotoService photoService, IFileService fileService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _photoService = photoService;
        this.fileService = fileService;
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

    public async Task<List<PropertyListDto>> GetMyPropertyList(int currentUserId)
    {

        var includeList = new Expression<Func<Property, object>>[]
        {
            x => x.PropertyType,
            x => x.FurnishingType,
            x => x.City,
            x => x.Country,
            x => x.Photos
        };

        var properties = await _unitOfWork.PropertyRepository.GetAll(
            expression: q => q.PostedBy == currentUserId,
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

        Expression<Func<Property, bool>> filter;

        if (!String.IsNullOrEmpty(paginationParameter.SearchingText))
        {
            filter = q => q.SellRent == sellRent && q.Name.ToLower().Contains(paginationParameter.SearchingText.ToLower());
        }
        else
        {
            filter = q => q.SellRent == sellRent;
        }

        var paginatedPropertyResult = await _unitOfWork.PropertyRepository.GetPaginateList(
            paginationParameter.CurrentPageNo, paginationParameter.PageSize,
            filter: filter,
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

    public async Task<PageResult<PropertyListDto>> GetMyPropertyPaginatedList(PaginationParameter paginationParameter, int currentUserId)
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
            filter: q => q.PostedBy == currentUserId,
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

    public async Task<int> AddNewProperty(PropertyCreateUpdateDto propertyCreateUpdateDto, int currentUserId)
    {
        var property = _mapper.Map<Property>(propertyCreateUpdateDto);

        if (propertyCreateUpdateDto.Id > 0)
        {
            property = await _unitOfWork.PropertyRepository.Get(x => x.Id == propertyCreateUpdateDto.Id);
            _mapper.Map(propertyCreateUpdateDto, property);
            property.LastUpdatedOn = DateTime.UtcNow;
            property.LastUpdatedBy = currentUserId;
            _unitOfWork.PropertyRepository.Update(property);
        }
        else
        {
            property.PostedOn = DateTime.UtcNow;
            property.PostedBy = currentUserId;
            property.LastUpdatedOn = DateTime.UtcNow;
            property.LastUpdatedBy = currentUserId;

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
                fileService.DeleteFile(photo.ImageUrl);
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

        var tomorrow = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1));
        var endDate = tomorrow.AddDays(7);

        var takenTimeSlotList = await _unitOfWork.VisitingRequestRepository.GetAll(x => x.PropertyId == propertyId && x.Status != ((char)VisitingRequestStatus.NotApproved).ToString() && tomorrow <= x.DateOn && x.DateOn <= endDate);

        for (var currentDate = tomorrow; currentDate < endDate; currentDate = currentDate.AddDays(1))
        {
            var existingSchedules = takenTimeSlotList.Where(x => x.DateOn == currentDate).ToList();
            var takenStartTimeList = existingSchedules.Select(x => TimeOnly.FromDateTime(x.StartTime)).ToList();

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

    private static List<TimeSlot> CreateTimeSlotList(Property property, List<TimeOnly> takenStartTimeList)
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
