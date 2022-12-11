using BuyandRentHomeWebAPI.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Services.Interfaces
{
    public interface IPropertyService
    {
        Task<IEnumerable<PropertyListDto>> GetPropertyList(int sellRent);
        Task<IEnumerable<PropertyListDto>> GetMyPropertyList();
        Task<PageResult<PropertyListDto>> GetMyPropertyPaginatedList(PaginationParameter paginationParameter);
        Task<PropertyDetailDto> GetPropertyDetail(int id);
        Task<int> AddNewProperty(PropertyCreateUpdateDto propertyCreateUpdateDto);
    }
}
