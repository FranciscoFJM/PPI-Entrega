namespace Persistence.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        private readonly PPIContext _dbContext;

        public AssetRepository(PPIContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<AssetModel>> GetAssetsAll()
        {
            try
            {
                var query = from a in _dbContext.Assets
                            select new AssetModel
                            {
                                Id = a.Id,
                                Ticker = a.Ticker,
                                Name = a.Name,
                                UnitPrice = a.UnitPrice,
                                AssetTypeId = a.AssetTypeId
                            };
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DbPersistenceException("Persistence Layer Failure: GetAssetsAll", ex.Message);
            }
        }
        public async Task<AssetModel> GetAssetById(int id)
        {
            try
            {
                var query = from a in _dbContext.Assets
                            where a.Id == id
                            select new AssetModel
                            {
                                Id = a.Id,
                                Ticker = a.Ticker,
                                Name = a.Name,
                                UnitPrice = a.UnitPrice,
                                AssetTypeId = a.AssetTypeId
                            };
                return await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new DbPersistenceException("Persistence Layer Failure: GetAssetById", ex.Message);
            }
        }

        public async Task<List<AssetModel>> GetAssetsByAssetTypeId(int assetTypeId)
        {
            try
            {
                var query = from a in _dbContext.Assets
                            where a.AssetTypeId == assetTypeId
                            select new AssetModel
                            {
                                Id = a.Id,
                                Ticker = a.Ticker,
                                Name = a.Name,
                                UnitPrice = a.UnitPrice,
                                AssetTypeId = a.AssetTypeId
                            };
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DbPersistenceException("Persistence Layer Failure: GetAssetsByAssetTypeId", ex.Message);
            }
        }
    }
}