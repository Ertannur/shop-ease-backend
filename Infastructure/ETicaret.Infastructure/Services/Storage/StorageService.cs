using ETicaret.Application.Abstractions.Storage;
using Microsoft.AspNetCore.Http;


namespace ETicaret.Infastructure.Services.Storage;

public class StorageService(IStorage storage) : IStorageService
{
    public async Task<List<(string filename, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
    {
        return await storage.UploadAsync(pathOrContainerName, files);
    }

    public async Task DeleteAsync(string pathOrContainer, string fileName)
    {
        await storage.DeleteAsync(pathOrContainer, fileName);
    }

    public  List<string> GetFiles(string pathOrContainerName)
    {
         return storage.GetFiles(pathOrContainerName);
    }

    public bool HasFile(string pathOrContainerName, string fileName)
    {
        return storage.HasFile(pathOrContainerName, fileName);
    }

    public string StorageName
    {
        get => storage.GetType().Name;
    }
}