namespace Persistence.Repositories
{
    public class AssetTypeRepository : IAssetTypeRepository
    {
        private readonly PPIContext _dbContext;

        public AssetTypeRepository(PPIContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<AssetTypeModel>> GetAssetTypesAll()
        {
            try
            {
                var query = from at in _dbContext.AssetTypes
                            select new AssetTypeModel
                            {
                                Id = at.Id,
                                Description = at.Description
                            };
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DbPersistenceException("Persistence Layer Failure: GetAssetTypesAll", ex.Message);
            }
        }

        public async Task<AssetTypeModel> GetAssetTypeById(int id)
        {
            try
            {
                var query = from at in _dbContext.AssetTypes
                            where at.Id == id
                            select new AssetTypeModel
                            {
                                Id = at.Id,
                                Description = at.Description
                            };
                return await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new DbPersistenceException("Persistence Layer Failure: GetAssetTypeById", ex.Message);
            }
        }
    }
}
