namespace Interfaces;

public interface IEntityHttpRepository<TEntity> : IUploader where TEntity : class
{
    Task<PagingResponse<TEntity>> GetAsync(PagingEntity PagingEntity);

    public HttpClient HttpClient { get; set; }
    public string Url { get; set; }
    public string UrlFilter { get; set; }

    Task<TEntity> GetAsync(int id);

    Task AddAsync(TEntity model);

    Task EditAsync(int id, TEntity model);

    Task DeleteAsync(int id);
}
