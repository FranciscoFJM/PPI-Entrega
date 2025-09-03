namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AssetController : ControllerBase
    {
        private readonly ILogger<AssetController> _logger;
        private readonly IAssetService _assetService;

        public AssetController(ILogger<AssetController> logger,
            IAssetService assetService)
        {
            _logger = logger;
            _assetService = assetService;
        }

        /// <summary>
        /// Obtiene todos los activos disponibles.
        /// </summary>
        /// <returns>Lista de todos los activos.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAssetsAll()
        {
            var result = await _assetService.GetAssetsAll();

            return Ok(result);
        }


        /// <summary>
        /// Obtiene un activo específico por su Id.
        /// </summary>
        /// <param name="id">Id único del activo.</param>
        /// <returns>Activo encontrado o NotFound si no existe.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssetById(int id)
        {
            var result = await _assetService.GetAssetById(id);

            if (result.Data == null) return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Obtiene todos los activos que pertenecen a un tipo específico.
        /// 1 = Acción, 2 = Bonos, 3 = FCI.
        /// </summary>
        /// <param name="assetTypeId">Id del tipo de activo.</param>
        /// <returns>Lista de activos del tipo especificado o NotFound si no existen.</returns>
        [HttpGet("GetAssetsByAssetTypeId/{assetTypeId}")]
        public async Task<IActionResult> GetAssetsByAssetTypeId(int assetTypeId)
        {
            var result = await _assetService.GetAssetsByAssetTypeId(assetTypeId);
            if (result.Data == null) return NotFound();
            return Ok(result);
        }

    }
}