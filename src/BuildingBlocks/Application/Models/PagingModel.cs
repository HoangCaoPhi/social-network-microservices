namespace Application.Models;

public class PagingModel
{
    private int? pageNumber;
    private int? pageSize;
    
    public int? PageNumber
    {
        get => !pageNumber.HasValue || pageNumber.Value <= 0 ? 1 : pageNumber.Value;
        set => pageNumber = value;
    }

    public int? PageSize
    {
        get => !pageSize.HasValue || pageSize.Value <= 0 ? 10 : pageSize.Value;
        set => pageSize = value;
    }

    public PagingModel()
    {
    }

    public PagingModel(int? pageNumber, int? pageSize)
    {
        this.pageNumber = pageNumber;
        this.pageSize = pageSize;
    }
}