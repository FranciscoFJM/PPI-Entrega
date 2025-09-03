namespace Application.Services
{
    public class OperationTypeService : IOperationTypeService
    {
        private readonly ILogger<OperationTypeService> _logger;
        private readonly IOperationTypeRepository _operationTypeRepository;

        public OperationTypeService(ILogger<OperationTypeService> logger,
            IOperationTypeRepository operationTypeRepository)
        {
            _logger = logger;
            _operationTypeRepository = operationTypeRepository;
        }

        /// <summary>
        /// Obtiene todos los tipos de operaciones disponibles.
        /// </summary>
        /// <returns>Lista de todos los tipos de operaciones</returns>
        public async Task<ResultModel<List<OperationTypeModel>>> GetOperationTypesAll()
        {
            var result = new ResultModel<List<OperationTypeModel>>();

            try
            {
                result.Data = await _operationTypeRepository.GetOperationTypesAll();
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
        /// Obtiene un tipo de operación específico por su Id
        /// </summary>
        /// <param name="id">Id único del tipo de operación</param>
        /// <returns>Tipo de operación encontrado o null si no existe</returns>
        public async Task<ResultModel<OperationTypeModel>> GetOperationTypeById(string id)
        {
            var result = new ResultModel<OperationTypeModel>();

            try
            {
                result.Data = await _operationTypeRepository.GetOperationTypeById(id);
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
