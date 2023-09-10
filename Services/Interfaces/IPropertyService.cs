using BuyandRentHomeWebAPI.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Services.Interfaces
{
    public interface IPropertyService
    {
        Task<List<PropertyListDto>> GetPropertyList(int sellRent);
        Task<List<PropertyListDto>> GetMyPropertyList();
        Task<PageResult<PropertyListDto>> GetPropertyPaginatedList(PaginationParameter paginationParameter, int sellRent);
        Task<PageResult<PropertyListDto>> GetMyPropertyPaginatedList(PaginationParameter paginationParameter);
        Task<PropertyDetailDto> GetPropertyDetail(int id);
        Task<int> AddNewProperty(PropertyCreateUpdateDto propertyCreateUpdateDto);
        Task<bool> DeleteProperty(int id);
        Task<dynamic> GetAvailableSlotsForNext10Days(int propertyId);
    }
}
