namespace HttpServices;

public class HttpImageUploaderService : HttpEntityServiceBase
{
    public HttpImageUploaderService(HttpClient httpClient) : base(httpClient: httpClient) => this.UrlApiUploader = ServerApiUrl.Url_S_Api_Upload;

    public const string Url_S_Api_Upload = ServerApiUrl.Url_S_Api_Upload;
}
