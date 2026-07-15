namespace Banking.Application.DTOs.Common;

public class PagedResultDto<T>
{
    public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();

    /// <example>1</example>
    public int Page { get; set; }

    /// <example>20</example>
    public int PageSize { get; set; }

    /// <example>1</example>
    public int TotalItems { get; set; }

    /// <example>1</example>
    public int TotalPages { get; set; }
}
