namespace HttpServices;

public class HttpEntityServiceLite : HttpEntityServiceBase
{
    public HttpEntityServiceLite(HttpClient httpClient) : base(httpClient: httpClient)
    {
    }

    public async Task<HttpResponseMessage> GetAsync() => await this.HttpClient.GetAsync(this.Url);
}
