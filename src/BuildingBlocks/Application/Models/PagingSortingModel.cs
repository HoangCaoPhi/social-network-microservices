namespace Application.Models;

public class PagingSortingModel : PagingModel
{
    private OrderDirectionEnum _orderDirection;

    public string? OrderField { get; set; } = string.Empty;

    public string? OrderDirection 
    {
        get => _orderDirection.ToString();

        set => _orderDirection = Enum.TryParse(value?.ToUpper(), out OrderDirectionEnum val) 
            ? val 
            : OrderDirectionEnum.ASC;
    }

    public OrderDirectionEnum OrderDirectionEnum => _orderDirection;
}

public enum OrderDirectionEnum
{
    ASC,
    DESC
}