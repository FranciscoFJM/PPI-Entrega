namespace Domain.Services
{
    public interface IAssetService
    {
        Task<ResultModel<List<AssetModel>>> GetAssetsAll();
        Task<ResultModel<AssetModel>> GetAssetById(int id);
        Task<ResultModel<List<AssetModel>>> GetAssetsByAssetTypeId(int assetTypeId);
    }
}