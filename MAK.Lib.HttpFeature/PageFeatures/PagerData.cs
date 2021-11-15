namespace PageFeatures;

public class PagerData
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    public bool IsCursorPointer => this.CurrentPage > 1;
    public bool HasPrevious => this.CurrentPage > 1;
    public bool HasNext => this.CurrentPage < this.TotalPages;
}
