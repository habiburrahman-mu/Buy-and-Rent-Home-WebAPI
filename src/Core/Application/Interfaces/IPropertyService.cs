using Application.DTOs;
using Domain.Common;

namespace Application.Interfaces;

public interface IPropertyService
{
    Task<List<PropertyListDto>> GetPropertyList(int sellRent);
    Task<List<PropertyListDto>> GetMyPropertyList(int currentUserId);
    Task<PageResult<PropertyListDto>> GetPropertyPaginatedList(PaginationParameter paginationParameter, int sellRent);
    Task<PageResult<PropertyListDto>> GetMyPropertyPaginatedList(PaginationParameter paginationParameter, int currentUserId);
    Task<PropertyDetailDto> GetPropertyDetail(int id);
    Task<int> SaveProperty(PropertyCreateUpdateDto propertyCreateUpdateDto, int currentUserId);
    Task<bool> DeleteProperty(int id);
    Task<List<DayAvailability>> GetAvailableSlotsForNext7Days(int propertyId);
    Task<bool> UpdatePropertyStatus(int propertyId, string status);
}
