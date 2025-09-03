namespace Domain.Models;

public partial class AssetModel
{
    public int Id { get; set; }

    public string Ticker { get; set; } = null!;

    public string Name { get; set; } = null!;

    public decimal UnitPrice { get; set; }

    public int AssetTypeId { get; set; }
}
