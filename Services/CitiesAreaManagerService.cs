using BuyandRentHomeWebAPI.Data.Entities;
using BuyandRentHomeWebAPI.Data.Interfaces;
using BuyandRentHomeWebAPI.Dtos;
using BuyandRentHomeWebAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Services
{
    public class CitiesAreaManagerService : ICitiesAreaManagerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ISharedService sharedService;
        private const string AreaManagerRoleName = "Area Manager";

        public CitiesAreaManagerService(IUnitOfWork unitOfWork, ISharedService sharedService)
        {
            this.unitOfWork = unitOfWork;
            this.sharedService = sharedService;
        }

        public async Task<bool> SaveCitiesAreaManager(CitiesAreaManagerDto citiesAreaManagerDto)
        {
            var userRole = await unitOfWork.UserPrivilegeRepository.GetAll(expression: x => x.UserId == citiesAreaManagerDto.ManagerId, includes: x => x.Role);

            if (userRole.Count == 0)
            {
                throw new Exception("User Not Found");
            }

            var areaManagerRoleExists = userRole.Any(x => x.Role.Name == AreaManagerRoleName);

            if (areaManagerRoleExists == false)
            {
                throw new Exception("User is not an area manager");
            }

            var citiesAreaManagerDataInDb = await unitOfWork.CitiesAreaManagerRepository.GetAll(x => x.ManagerId == citiesAreaManagerDto.ManagerId);

            var createList = new List<CitiesAreaManager>();
            var deleteList = new List<CitiesAreaManager>();

            citiesAreaManagerDto.Cities.ForEach(city =>
            {
                if (citiesAreaManagerDataInDb.Any(x => x.CityId == city.Id) == false)
                {
                    var citiesAreaManagerNewEntity = new CitiesAreaManager
                    {
                        ManagerId = citiesAreaManagerDto.ManagerId,
                        CityId = city.Id,
                        LastUpdatedBy = sharedService.GetUserId(),
                        LastUpdatedOn = DateTime.UtcNow
                    };
                    createList.Add(citiesAreaManagerNewEntity);
                }
            });

            deleteList = citiesAreaManagerDataInDb.Where(x => citiesAreaManagerDto.Cities.FirstOrDefault(c => c.Id == x.CityId) == null).ToList();



            return true;
        }
    }
}
