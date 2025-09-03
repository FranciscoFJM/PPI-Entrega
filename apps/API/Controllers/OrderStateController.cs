namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderStateController : ControllerBase
    {
        private readonly ILogger<OrderStateController> _logger;
        private readonly IOrderStateService _orderStateService;

        public OrderStateController(ILogger<OrderStateController> logger,
            IOrderStateService orderStateService)
        {
            _logger = logger;
            _orderStateService = orderStateService;
        }

        /// <summary>
        /// Obtiene todos los estados de órdenes disponibles.
        /// </summary>
        /// <returns>Lista de todos los estados de órdenes.</returns>
        [HttpGet]
        public async Task<IActionResult> GetOrderStatesAll()
        {
            var result = await _orderStateService.GetOrderStatesAll();

            return Ok(result);
        }

        /// <summary>
        /// Obtiene un estado de orden específico por su Id.
        /// 0 = En proceso, 1= Ejecutada, 3 = Cancelada.
        /// </summary>
        /// <param name="id">Id único del estado de orden.</param>
        /// <returns>Estado de orden encontrado o NotFound si no existe.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderStateById(int id)
        {
            var result = await _orderStateService.GetOrderStateById(id);

            if (result.Data == null) return NotFound();

            return Ok(result);
        }
    }
}