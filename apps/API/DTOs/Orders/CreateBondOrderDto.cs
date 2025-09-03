namespace API.DTOs.Orders;

public class CreateBondOrderDto
{

    public int AccountId { get; set; }
    public int AssetId { get; set; }
    public long Quantity { get; set; }
    public decimal Price { get; set; }
    public string OperationTypeId { get; set; } = null!;
}
