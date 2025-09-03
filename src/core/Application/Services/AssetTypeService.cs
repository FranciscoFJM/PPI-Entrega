namespace Application.Services
{
    public class AssetTypeService : IAssetTypeService
    {
        private readonly ILogger<AssetTypeService> _logger;
        private readonly IAssetTypeRepository _assetTypeRepository;

        public AssetTypeService(ILogger<AssetTypeService> logger,
            IAssetTypeRepository assetTypeRepository)
        {
            _logger = logger;
            _assetTypeRepository = assetTypeRepository;
        }

        /// <summary>
        /// Obtiene todos los tipos de activos disponibles.
        /// </summary>
        /// <returns>Lista de todos los tipos de activos</returns>
        public async Task<ResultModel<List<AssetTypeModel>>> GetAssetTypesAll()
        {
            var result = new ResultModel<List<AssetTypeModel>>();

            try
            {
                result.Data = await _assetTypeRepository.GetAssetTypesAll();
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
        /// Obtiene un tipo de activo específico por su Id
        /// </summary>
        /// <param name="id">Id único del tipo de activo</param>
        /// <returns>Tipo de activo encontrado o null si no existe</returns>
        public async Task<ResultModel<AssetTypeModel>> GetAssetTypeById(int id)
        {
            var result = new ResultModel<AssetTypeModel>();

            try
            {
                result.Data = await _assetTypeRepository.GetAssetTypeById(id);
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
