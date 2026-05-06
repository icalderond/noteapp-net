namespace Api.Models;

public class NoteQueryParams
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public string? Search { get; set; }

    public string? SortBy { get; set; } = "createdAt";
    public bool Desc { get; set; } = true;
}