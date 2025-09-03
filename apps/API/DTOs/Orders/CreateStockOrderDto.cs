namespace API.DTOs.Orders;

public class CreateStockOrderDto
{

    public int AccountId { get; set; }
    public int AssetId { get; set; }
    public long Quantity { get; set; }
    public string OperationTypeId { get; set; } = null!;
}
