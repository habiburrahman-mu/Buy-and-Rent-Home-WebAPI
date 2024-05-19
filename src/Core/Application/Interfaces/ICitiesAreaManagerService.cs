using Application.DTOs;

namespace Application.Interfaces;

public interface ICitiesAreaManagerService
{
    Task<bool> SaveCitiesAreaManager(CitiesAreaManagerDto citiesAreaManagerDto, int currentUserId);
}
