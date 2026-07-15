namespace Banking.Application.DTOs.Common;

/// <summary>
/// Generic paginated result wrapper.
/// </summary>
/// <typeparam name="T">The type of items in the result set.</typeparam>
public class PagedResultDto<T>
{
    /// <summary>The items on the current page.</summary>
    public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();

    /// <summary>The current page number (1-based).</summary>
    /// <example>1</example>
    public int Page { get; set; }

    /// <summary>The number of items per page.</summary>
    /// <example>20</example>
    public int PageSize { get; set; }

    /// <summary>The total number of items across all pages.</summary>
    /// <example>1</example>
    public int TotalItems { get; set; }

    /// <summary>The total number of pages.</summary>
    /// <example>1</example>
    public int TotalPages { get; set; }
}
