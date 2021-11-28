namespace HttpServices;

public class HttpEntityService<TEntity> : HttpEntityServiceBase where TEntity : class
{
    public HttpEntityService(HttpClient httpClient) : base(httpClient: httpClient)
    {
    }

    protected async Task<DataResult<PagingResponse<TEntity>>> GetAsync(PagingEntity entityParameter)
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

        if(pagingResponse is null)
        {
            return new DataResult<PagingResponse<TEntity>>
            {
                Success = false,
                Message = "No Data"
            };
        }
        else
        {
            return new DataResult<PagingResponse<TEntity>>
            {
                Data = pagingResponse,
                Success = true,
                Message = "Data"
            };
        }
    }

    protected async Task<DataResult<List<TEntity>>> GetAsync() //public => protected
    {
        var response = await this.HttpClient.GetAsync(this.Url);
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<List<TEntity>>();

        if(data is null)
        {
            return new DataResult<List<TEntity>>
            {
                Success = false,
                Message = "No Data"
            };
        }
        else
        {
            return new DataResult<List<TEntity>>
            {
                Data = data,
                Success = true,
                Message = "Data"
            };
        }
    }

    protected async Task<DataResult<TEntity>> GetAsync(int id)
    {
        var data = await this.HttpClient.GetFromJsonAsync<TEntity>(this.Url + id);

        if(data is null)
        {
            return new DataResult<TEntity>
            {
                Success = false,
                Message = "No Data"
            };
        }
        else
        {
            return new DataResult<TEntity>
            {
                Data = data,
                Success = true,
                Message = "Data"
            };
        }
    }

    protected async Task PostAsync(TEntity model) => await this.HttpClient.PostAsJsonAsync(this.Url, model);
    protected async Task PutAsync(int id, TEntity model) => await this.HttpClient.PutAsJsonAsync(this.Url + id, model);
    protected async Task DeleteAsync(int id) => await this.HttpClient.DeleteAsync(this.Url + id);
}

////=============
//// S Orig
//protected async Task<PagingResponse<TEntity>> GetAsync(PagingEntity entityParameter)
//{
//    var queryStringParam = new Dictionary<string, string>
//    {
//        [HttpFeatureData.PageNumber] = entityParameter.PageNumber.ToString(),
//        [HttpFeatureData.SearchTerm] = entityParameter.SearchTerm == null ? "" : entityParameter.SearchTerm,
//        [HttpFeatureData.OrderBy] = entityParameter.OrderBy
//    };

//    var response = await this.HttpClient.GetAsync(QueryHelpers.AddQueryString(this.Url, queryStringParam));
//    var content = await response.Content.ReadAsStringAsync();

//    if(!response.IsSuccessStatusCode)
//    {
//        throw new ApplicationException(content);
//    }

//    var pagingResponse = new PagingResponse<TEntity>
//    {
//        Items = JsonSerializer.Deserialize<List<TEntity>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
//        PagerData = JsonSerializer.Deserialize<PagerData>
//      (response.Headers.GetValues(HttpFeatureData.X_Pagination).First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
//    };

//    return pagingResponse;
//}
//
//public async Task<List<TEntity>> GetAsync()
//{
//    var response = await this.HttpClient.GetAsync(this.Url);
//    response.EnsureSuccessStatusCode();
//    var result = await response.Content.ReadFromJsonAsync<List<TEntity>>();

//    return result;
//}
//
//protected async Task<TEntity> GetAsync(int id) => await this.HttpClient.GetFromJsonAsync<TEntity>(this.Url + id);
//protected async Task PostAsync(TEntity model) => await this.HttpClient.PostAsJsonAsync(this.Url, model);
//protected async Task PutAsync(int id, TEntity model) => await this.HttpClient.PutAsJsonAsync(this.Url + id, model);
//protected async Task DeleteAsync(int id) => await this.HttpClient.DeleteAsync(this.Url + id);
//// E Orig
