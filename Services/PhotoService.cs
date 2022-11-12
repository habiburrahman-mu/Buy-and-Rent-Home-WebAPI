using AutoMapper;
using BuyandRentHomeWebAPI.Data;
using BuyandRentHomeWebAPI.Data.Interfaces;
using BuyandRentHomeWebAPI.Dtos;
using BuyandRentHomeWebAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PhotoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PhotoDto>> GetPhotoListByPropertyId(int propertyId)
        {
            var photoList = await _unitOfWork.PhotoRepository.GetAll(
                expression: x => x.PropertyId == propertyId,
                orderBy: x => x.OrderBy(q => q.IsPrimary));
            var photoDtoList = _mapper.Map<IEnumerable<PhotoDto>>(photoList);
            return photoDtoList;
        }
    }
}
