using Microsoft.AspNetCore.Http;

namespace Application.Services;

public interface ICloudService
{
    public interface ICloudService
    {
        Task<string> UploadImageAsync(IFormFile file);
        Task<string> UploadTrackAsync(IFormFile file);
    }
}
