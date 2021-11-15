namespace HttpServices;

public class HttpEntityServiceBase
{
    public HttpEntityServiceBase(HttpClient httpClient)
    {
        httpClient.BaseAddress = new Uri(ServerApiUrl.Url_Api_Base);
        this.HttpClient = httpClient;
    }

    protected HttpClient HttpClient { get; set; }
    protected string Url { get; set; }
    public string UrlApiUploader { get; set; }

    public List<ImageFile> ImageFiles { get; set; } = new();
    public string Message { get; set; } = string.Empty;
    public bool IsDisabled { get; set; } = true;

    public async Task UploadAsync(string fileNameStart)
    {
        using var msg = await this.HttpClient.PostAsJsonAsync<List<ImageFile>>(this.UrlApiUploader + $"/{fileNameStart}", this.ImageFiles, CancellationToken.None);

        this.IsDisabled = false;

        if(msg.IsSuccessStatusCode)
        {
            this.Message = $"{this.ImageFiles.Count} files uploaded";
            this.ImageFiles.Clear();

            this.IsDisabled = true;
        }
    }

    public async Task<string> UploadAsync(MultipartFormDataContent content)
    {
        var postResult = await this.HttpClient.PostAsync(this.UrlApiUploader, content);
        var postContent = await postResult.Content.ReadAsStringAsync();

        return !postResult.IsSuccessStatusCode ? throw new ApplicationException(postContent) : Path.Combine(ServerApiUrl.ServerApiBaseUrl, postContent);
    }
}
