namespace Domain.Repositories
{
    public interface IAssetRepository
    {
        Task<List<AssetModel>> GetAssetsAll();
        Task<AssetModel> GetAssetById(int id);
        Task<List<AssetModel>> GetAssetsByAssetTypeId(int assetTypeId);
    }
}
