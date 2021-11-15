namespace PageFeatures;

public class PagedList<T> : List<T>
{
    public PagedList() => this.PagerData = new();

    public PagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        this.PagerData = new PagerData
        {
            TotalCount = count,
            PageSize = pageSize,
            CurrentPage = pageNumber,
            TotalPages = (int)Math.Ceiling(count / (double)pageSize)
        };

        this.AddRange(items);
    }

    public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize)
    {
        var count = source.Count();
        var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }

    public PagerData PagerData { get; set; }
}
