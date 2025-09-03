namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AssetTypeController : ControllerBase
    {
        private readonly ILogger<AssetTypeController> _logger;
        private readonly IAssetTypeService _assetTypeService;

        public AssetTypeController(ILogger<AssetTypeController> logger,
            IAssetTypeService assetTypeService)
        {
            _logger = logger;
            _assetTypeService = assetTypeService;
        }

        /// <summary>
        /// Obtiene todos los tipos de activos disponibles.
        /// </summary>
        /// <returns>Lista de todos los tipos de activos.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAssetTypesAll()
        {
            var result = await _assetTypeService.GetAssetTypesAll();

            return Ok(result);
        }

        /// <summary>
        /// Obtiene un tipo de activo específico por su Id.
        /// 1 = Acción, 2 = Bonos, 3 = FCI.
        /// </summary>
        /// <param name="id">Id único del tipo de activo.</param>
        /// <returns>Tipo de activo encontrado o NotFound si no existe.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssetTypeById(int id)
        {
            var result = await _assetTypeService.GetAssetTypeById(id);

            if (result.Data == null) return NotFound();

            return Ok(result);
        }
    }
}