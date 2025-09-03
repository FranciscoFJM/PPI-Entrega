namespace Domain.Repositories
{
    public interface IAssetTypeRepository
    {
        Task<List<AssetTypeModel>> GetAssetTypesAll();
        Task<AssetTypeModel> GetAssetTypeById(int id);
    }
}
