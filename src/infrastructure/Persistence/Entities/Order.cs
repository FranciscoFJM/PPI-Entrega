namespace Persistence.Entities;

public partial class Order
{
    public int Id { get; set; }
    public int AccountId { get; set; }

    public long Quantity { get; set; }

    public decimal Price { get; set; }

    public string OperationTypeId { get; set; }

    public int StatusId { get; set; }

    public decimal TotalAmount { get; set; }
    public int AssetId { get; set; }
}
