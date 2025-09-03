namespace Application.Services
{
    public class AssetService : IAssetService
    {
        private readonly ILogger<AssetService> _logger;
        private readonly IAssetRepository _assetRepository;

        public AssetService(ILogger<AssetService> logger,
            IAssetRepository assetRepository)
        {
            _logger = logger;
            _assetRepository = assetRepository;
        }

        /// <summary>
        /// Obtiene todos los activos disponibles.
        /// </summary>
        /// <returns>Lista de todos los activos</returns>
        public async Task<ResultModel<List<AssetModel>>> GetAssetsAll()
        {
            var result = new ResultModel<List<AssetModel>>();

            try
            {
                result.Data = await _assetRepository.GetAssetsAll();
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
        /// Obtiene un activo específico por su Id
        /// </summary>
        /// <param name="id">Id único del activo</param>
        /// <returns>Activo encontrado o null si no existe</returns>
        public async Task<ResultModel<AssetModel>> GetAssetById(int id)
        {
            var result = new ResultModel<AssetModel>();

            try
            {
                result.Data = await _assetRepository.GetAssetById(id);
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
        /// Obtiene todos los activos que pertenecen a un tipo específico
        /// </summary>
        /// <param name="assetTypeId">Id del tipo de activo</param>
        /// <returns>Lista de activos del tipo especificado</returns>
        public async Task<ResultModel<List<AssetModel>>> GetAssetsByAssetTypeId(int assetTypeId)
        {
            var result = new ResultModel<List<AssetModel>>();

            try
            {
                result.Data = await _assetRepository.GetAssetsByAssetTypeId(assetTypeId);
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