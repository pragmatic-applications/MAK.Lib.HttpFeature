namespace PageFeatures;

public class PagingEntity
{
    private class PageNumberData
    {
        public const int Start_Page = 1;
        public const int Quantity_Per_Page = 3;
        public const int Page_Size = 8;
    }

    private const int max_Page_Size = 50;
    public int PageNumber { get; set; } = 1;

    private int pageSize = PageNumberData.Page_Size;

    public int PageSize
    {
        get => this.pageSize;
        set => this.pageSize = (value > max_Page_Size) ? max_Page_Size : value;
    }

    public string SearchTerm { get; set; } = string.Empty;
    public string OrderBy { get; set; } = "name";
}
