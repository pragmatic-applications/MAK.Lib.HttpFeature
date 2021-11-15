namespace HttpServices;

public class HttpEntityService<TEntity> : HttpEntityServiceBase where TEntity : class
{
    public HttpEntityService(HttpClient httpClient) : base(httpClient: httpClient)
    {
    }

    protected async Task<PagingResponse<TEntity>> GetAsync(PagingEntity entityParameter)
    {
        var queryStringParam = new Dictionary<string, string>
        {
            [HttpFeatureData.PageNumber] = entityParameter.PageNumber.ToString(),
            [HttpFeatureData.SearchTerm] = entityParameter.SearchTerm == null ? "" : entityParameter.SearchTerm,
            [HttpFeatureData.OrderBy] = entityParameter.OrderBy
        };

        var response = await this.HttpClient.GetAsync(QueryHelpers.AddQueryString(this.Url, queryStringParam));
        var content = await response.Content.ReadAsStringAsync();

        if(!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        var pagingResponse = new PagingResponse<TEntity>
        {
            Items = JsonSerializer.Deserialize<List<TEntity>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
            PagerData = JsonSerializer.Deserialize<PagerData>
          (response.Headers.GetValues(HttpFeatureData.X_Pagination).First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
        };

        return pagingResponse;
    }

    public async Task<List<TEntity>> GetAsync()
    {
        var response = await this.HttpClient.GetAsync(this.Url);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<List<TEntity>>();

        return result;
    }

    protected async Task<TEntity> GetAsync(int id) => await this.HttpClient.GetFromJsonAsync<TEntity>(this.Url + id);
    protected async Task PostAsync(TEntity model) => await this.HttpClient.PostAsJsonAsync(this.Url, model);
    protected async Task PutAsync(int id, TEntity model) => await this.HttpClient.PutAsJsonAsync(this.Url + id, model);
    protected async Task DeleteAsync(int id) => await this.HttpClient.DeleteAsync(this.Url + id);
}
