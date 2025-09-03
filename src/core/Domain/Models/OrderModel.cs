namespace Domain.Models;

public partial class OrderModel
{

    public int Id { get; set; }

    public int AccountId { get; set; }

    public int AssetId { get; set; }

    public long Quantity { get; set; }

    public decimal Price { get; set; }

    public string OperationTypeId { get; set; } = null!;

    public int StatusId { get; set; }

    public decimal TotalAmount { get; set; }
}
