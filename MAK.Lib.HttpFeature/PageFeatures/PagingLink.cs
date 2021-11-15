namespace PageFeatures;

public class PagingLink
{
    public string Text { get; set; }
    public int Page { get; set; }
    public bool Enabled { get; set; }
    public bool Active { get; set; }
    public bool IsCursorPointer { get; set; }

    public PagingLink(int page, bool enabled, string text, bool isCursorPointer = false)
    {
        this.Page = page;
        this.Enabled = enabled;
        this.IsCursorPointer = isCursorPointer;
        this.Text = text;
    }
}
