namespace Domain.Models;

public partial class OrderDetailedModel
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public int AssetId { get; set; }

    public long Quantity { get; set; }

    public decimal Price { get; set; }

    public string OperationTypeId { get; set; } = null!;

    public int StatusId { get; set; }

    public decimal TotalAmount { get; set; }

    // Campos de descripción para entidades relacionadas
    public string AssetName { get; set; }
    public int AssetType { get; set; }
    public string AssetTypeDescription { get; set; }
    public string OperationTypeDescription { get; set; }
    public string StatusDescription { get; set; }
}
