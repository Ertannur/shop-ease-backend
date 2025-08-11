using Microsoft.AspNetCore.Http;

namespace ETicaret.Application.Abstractions.Storage;

public interface IStorage
{
    Task<List<(string filename, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files);
    Task DeleteAsync(string pathOrContainer,string fileName);
    List<string> GetFiles(string pathOrContainerName);
    bool HasFile(string pathOrContainerName, string fileName);
}