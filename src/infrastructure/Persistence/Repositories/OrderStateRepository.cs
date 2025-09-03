namespace Persistence.Repositories
{
    public class OrderStateRepository : IOrderStateRepository
    {
        private readonly PPIContext _dbContext;

        public OrderStateRepository(PPIContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<OrderStateModel>> GetOrderStatesAll()
        {
            try
            {
                var query = from os in _dbContext.OrderStates
                            select new OrderStateModel
                            {
                                Id = os.Id,
                                Description = os.Description
                            };

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DbPersistenceException("Persistence Layer Failure: GetOrderStatesAll", ex.Message);
            }
        }

        public async Task<OrderStateModel> GetOrderStateById(int id)
        {
            try
            {
                var query = from os in _dbContext.OrderStates
                            where os.Id == id
                            select new OrderStateModel
                            {
                                Id = os.Id,
                                Description = os.Description
                            };

                return await query.FirstOrDefaultAsync();
            }
            catch (DbPersistenceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DbPersistenceException("Persistence Layer Failure: GetOrderStateById", ex.Message);
            }
        }
    }
}
