namespace Persistence.Repositories
{
    public class OperationTypeRepository : IOperationTypeRepository
    {
        private readonly PPIContext _dbContext;

        public OperationTypeRepository(PPIContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<OperationTypeModel>> GetOperationTypesAll()
        {
            try
            {
                var query = from ot in _dbContext.OperationTypes
                            select new OperationTypeModel
                            {
                                Id = ot.Id,
                                Description = ot.Description
                            };

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DbPersistenceException("Persistence Layer Failure: GetOperationTypesAll", ex.Message);
            }
        }

        public async Task<OperationTypeModel> GetOperationTypeById(string id)
        {
            try
            {
                var query = from ot in _dbContext.OperationTypes
                            where ot.Id == id
                            select new OperationTypeModel
                            {
                                Id = ot.Id,
                                Description = ot.Description
                            };

                return await query.FirstOrDefaultAsync();
            }
            catch (DbPersistenceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DbPersistenceException("Persistence Layer Failure: GetOperationTypeById", ex.Message);
            }
        }
    }
}
