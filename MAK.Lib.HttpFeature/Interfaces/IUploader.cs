namespace Interfaces;

public interface IUploader
{
    Task<string> UploadImageAsync(MultipartFormDataContent content);
}
