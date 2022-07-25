namespace Utils;

public class PagedList<T> where T : class
{
    public const int FirstPage = 0;
    public const int DefaultItemsPerPage = 10;

    public PagedList()
    {
        ItemsPerPage = 0;
        CurrentPage = 0;
        TotalItems = 0;
        Items = new List<T>();
    }

    public PagedList(int itemsPerPage, int currentPage) : this()
    {
        ItemsPerPage = itemsPerPage;
        CurrentPage = currentPage;
    }

    public PagedList(int itemsPerPage, int currentPage, long totalItems) : this(itemsPerPage, currentPage)
    {
        TotalItems = totalItems;

        double pageCount = (double)TotalItems / ItemsPerPage;
        TotalPages = (int)Math.Ceiling(pageCount);
    }

    public PagedList(int itemsPerPage, int currentPage, long totalItems, List<T> items) : this(itemsPerPage, currentPage, totalItems)
    {
        Items = items;
    }

    /// <summary>
    /// Total number or items/records in the dataset (the query result).
    /// </summary>
    public long TotalItems { get; set; }

    /// <summary>
    /// Total number of pages the dataset (query) has.
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// Number of items/records per page.
    /// </summary>
    public int ItemsPerPage { get; set; }

    /// <summary>
    /// The index of the current page. The index of the first page is 1.
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// List of the items in the page.
    /// </summary>
    public List<T> Items { get; set; }
}
 