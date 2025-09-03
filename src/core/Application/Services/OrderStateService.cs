namespace Application.Services
{
    public class OrderStateService : IOrderStateService
    {
        private readonly ILogger<OrderStateService> _logger;
        private readonly IOrderStateRepository _orderStateRepository;

        public OrderStateService(ILogger<OrderStateService> logger,
            IOrderStateRepository orderStateRepository)
        {
            _logger = logger;
            _orderStateRepository = orderStateRepository;
        }

        /// <summary>
        /// Obtiene todos los estados de órdenes disponibles.
        /// </summary>
        /// <returns>Lista de todos los estados de órdenes</returns>
        public async Task<ResultModel<List<OrderStateModel>>> GetOrderStatesAll()
        {
            var result = new ResultModel<List<OrderStateModel>>();

            try
            {
                result.Data = await _orderStateRepository.GetOrderStatesAll();
            }
            catch (DbPersistenceException ex)
            {
                result.AddDataBaseError(ex.Message);
                _logger.LogError(ex, ex.MessageLogger);
            }
            catch (Exception ex)
            {
                result.AddInternalError(ex.Message);
                _logger.LogError(ex, ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Obtiene un estado de orden específico por su Id
        /// </summary>
        /// <param name="id">Id único del estado de orden</param>
        /// <returns>Estado de orden encontrado o null si no existe</returns>
        public async Task<ResultModel<OrderStateModel>> GetOrderStateById(int id)
        {
            var result = new ResultModel<OrderStateModel>();

            try
            {
                result.Data = await _orderStateRepository.GetOrderStateById(id);
            }
            catch (DbPersistenceException ex)
            {
                result.AddDataBaseError(ex.Message);
                _logger.LogError(ex, ex.MessageLogger);
            }
            catch (Exception ex)
            {
                result.AddInternalError(ex.Message);
                _logger.LogError(ex, ex.Message);
            }

            return result;
        }
    }
}
