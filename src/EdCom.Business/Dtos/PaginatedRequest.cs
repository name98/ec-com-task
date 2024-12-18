namespace EdCom.Business.Dtos;

public class PaginatedRequest
{
    private int _page = 0;
    public int Page { get => _page; init => _page = value < 0 ? 0 : value; }

    private int _pageSize = 10;
    public int PageSize { get => _pageSize; init => _pageSize = value < 0 ? 10 : value; }
}
