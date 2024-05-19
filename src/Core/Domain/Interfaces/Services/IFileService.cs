using Domain.Models;

namespace Domain.Interfaces.Services;

public interface IFileService
{
    void DeleteFile(string fileName);
    Task<bool> SaveFilesAsync(IEnumerable<PhotoFileDto> files, List<string> savedFileNames);
}
