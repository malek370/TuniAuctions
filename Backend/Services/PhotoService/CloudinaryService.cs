using System;

namespace backend.Services.PhotoService;

public class CloudinaryService : IPhotoService
{
    public Task DeletePhoto()
    {
        throw new NotImplementedException();
    }

    public Task<string> UploadPhoto(IFormFile file)
    {
        throw new NotImplementedException();
    }
}
