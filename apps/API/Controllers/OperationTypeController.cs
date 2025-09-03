namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OperationTypeController : ControllerBase
    {
        private readonly ILogger<OperationTypeController> _logger;
        private readonly IOperationTypeService _operationTypeService;

        public OperationTypeController(ILogger<OperationTypeController> logger,
            IOperationTypeService operationTypeService)
        {
            _logger = logger;
            _operationTypeService = operationTypeService;
        }

        /// <summary>
        /// Obtiene todos los tipos de operaciones disponibles..
        /// </summary>
        /// <returns>Lista de todos los tipos de operaciones.</returns>
        [HttpGet]
        public async Task<IActionResult> GetOperationTypesAll()
        {
            var result = await _operationTypeService.GetOperationTypesAll();

            return Ok(result);
        }

        /// <summary>
        /// Obtiene un tipo de operación específico por su Id.
        /// C = Compra, V = Venta.
        /// </summary>
        /// <param name="id">Id único del tipo de operación.</param>
        /// <returns>Tipo de operación encontrado o NotFound si no existe.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOperationTypeById(string id)
        {
            var result = await _operationTypeService.GetOperationTypeById(id);

            if (result.Data == null) return NotFound();

            return Ok(result);
        }
    }
}
