namespace Domain.Services
{
    public interface IAssetTypeService
    {
        Task<ResultModel<List<AssetTypeModel>>> GetAssetTypesAll();
        Task<ResultModel<AssetTypeModel>> GetAssetTypeById(int id);
    }
}
