using System;

namespace backend.Services.PhotoService;

public interface IPhotoService
{
    public Task<string> UploadPhoto(IFormFile file);
    public Task DeletePhoto();
}
