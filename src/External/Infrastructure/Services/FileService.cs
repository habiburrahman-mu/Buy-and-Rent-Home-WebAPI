using Domain.Interfaces.Services;
using Domain.Models;

namespace Infrastructure.Services;

public class FileService : IFileService
{
    private string _fileUploadDirectory = "../Upload\\files";
    public async Task<bool> SaveFilesAsync(IEnumerable<PhotoFileDto> files, List<string> savedFileNames)
    {
        bool isSaveSuccess = false;

        foreach (var file in files)
        {
            var fileName = file.FileName;
            var extension = Path.GetExtension(fileName);
            var newFileName = DateTime.UtcNow.Ticks + extension;

            var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), _fileUploadDirectory);

            if (!Directory.Exists(pathBuilt))
            {
                Directory.CreateDirectory(pathBuilt);
            }

            var path = Path.Combine(pathBuilt, newFileName);

            await File.WriteAllBytesAsync(path, file.Content);

            savedFileNames.Add(newFileName);
        }

        isSaveSuccess = true;

        return isSaveSuccess;
    }

    public void DeleteFile(string fileName)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), _fileUploadDirectory, fileName);
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
