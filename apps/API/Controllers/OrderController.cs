namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderServices;

        public OrderController(ILogger<OrderController> logger,
            IOrderService orderServices)
        {
            _logger = logger;
            _orderServices = orderServices;
        }

        /// <summary>
        /// Obtiene todas las órdenes disponibles.
        /// </summary>
        /// <returns>Lista de todas las órdenes disponibles.</returns>
        [HttpGet]
        public async Task<IActionResult> GetOrdersAll()
        {
            var result = await _orderServices.GetOrdersAll();

            return Ok(result);
        }


        /// <summary>
        /// Obtiene una orden específica por su Id.
        /// Esta orden vendrá con información detallada.
        /// Se incluyen las descripciones del activo, tipo de activo, tipo de operación y estado de la orden.
        /// </summary>
        /// <param name="id">Id único de la orden.</param>
        /// <returns>Orden encontrada o NotFound si no existe.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _orderServices.GetOrderById(id);

            if (result.Data == null) return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Obtiene todas las órdenes asociadas a un activo específico.
        /// </summary>
        /// <param name="assetId">Id del activo.</param>
        /// <returns>Lista de órdenes del activo especificado.</returns>
        [HttpGet("asset/{assetId}")]
        public async Task<IActionResult> GetOrdersByAssetId(int assetId)
        {
            var result = await _orderServices.GetOrdersByAssetId(assetId);

            return Ok(result);
        }



        /// <summary>
        /// Obtiene todas las órdenes asociadas a una cuenta específica.
        /// </summary>
        /// <param name="accountId">Id de la cuenta.</param>
        /// <returns>Lista de órdenes de la cuenta especificada.</returns>
        [HttpGet("account/{accountId}")]
        public async Task<IActionResult> GetOrdersByAccountId(int accountId)
        {
            var result = await _orderServices.GetOrdersByAccountId(accountId);

            return Ok(result);
        }

        /// <summary>
        /// Obtiene todas las órdenes que tienen un estado específico.
        /// 0 = En proceso, 1= Ejecutada, 3 = Cancelada.
        /// </summary>
        /// <param name="statusId">Id del estado de la orden.</param>
        /// <returns>Lista de órdenes con el estado especificado.</returns>
        [HttpGet("status/{statusId}")]
        public async Task<IActionResult> GetOrdersByStatusId(int statusId)
        {
            var result = await _orderServices.GetOrdersByStatusId(statusId);

            return Ok(result);
        }

        /// <summary>
        /// Obtiene todas las órdenes que tienen un estado específico.
        /// C = Compra, V = Venta.
        /// </summary>
        /// <param name="operationTypeId">Id del tipo de operación.</param>
        /// <returns>Lista de órdenes con el tipo de operación especificado.</returns>
        [HttpGet("operation/{operationTypeId}")]
        public async Task<IActionResult> GetOrdersByOperationTypeId(string operationTypeId)
        {
            var result = await _orderServices.GetOrdersByOperationTypeId(operationTypeId);

            return Ok(result);
        }

        /// <summary>
        /// Obtiene todas las órdenes asociadas a un tipo de activo específico.
        /// 1 = Acción, 2 = Bono, 3 = FCI.
        /// </summary>
        /// <param name="assetTypeId">Id del tipo de activo.</param>
        /// <returns>Lista de órdenes del tipo de activo especificado.</returns>
        [HttpGet("assettype/{assetTypeId}")]
        public async Task<IActionResult> GetOrdersByAssetTypeId(int assetTypeId)
        {
            var result = await _orderServices.GetOrdersByAssetTypeId(assetTypeId);

            return Ok(result);
        }


        /// <summary>
        /// Crea una nueva orden con Activo tipo Acción.
        /// Solo se pueden usar los Activos de tipo Acción (AssetTypeId = 1).
        /// No se ingresa el precio, se obtiene del activo.
        /// Se deben discriminar comisiones e impuestos:.
        /// ● Comisiones: 0.6% sobre el "Monto Total".
        /// ● Impuestos: 21% sobre el valor de las comisiones.
        /// </summary>
        /// <param name="createOrderDto">Datos de la orden.</param>
        /// <returns>Resultado de la operación de creación.</returns>
        [HttpPost("Stock")]
        public async Task<IActionResult> CreateStockOrder([FromBody] CreateStockOrderDto createOrderDto)
        {
            var orderModel = new OrderModel
            {

                AccountId = createOrderDto.AccountId,
                AssetId = createOrderDto.AssetId,
                Quantity = createOrderDto.Quantity,
                OperationTypeId = createOrderDto.OperationTypeId,
                StatusId = 0
            };

            var result = await _orderServices.CreateStockOrder(orderModel);

            return Ok(result);
        }

        /// <summary>
        /// Crea una nueva orden con Activo tipo Bono.
        /// Solo se pueden usar los Activos de tipo Bono (AssetTypeId = 2).
        /// Se ingresan el precio y la cantidad.
        /// Se deben discriminar comisiones e impuestos:.
        /// ● Comisiones: 0.2% sobre el "Monto Total".
        /// ● Impuestos: 21% sobre el valor de las comisiones.
        /// </summary>
        /// <param name="createOrderDto">Datos de la orden.</param>
        /// <returns>Resultado de la operación de creación.</returns>
        [HttpPost("Bond")]
        public async Task<IActionResult> CreateBondOrder([FromBody] CreateBondOrderDto createOrderDto)
        {
            var orderModel = new OrderModel
            {

                AccountId = createOrderDto.AccountId,
                AssetId = createOrderDto.AssetId,
                Quantity = createOrderDto.Quantity,
                Price = createOrderDto.Price,
                OperationTypeId = createOrderDto.OperationTypeId,
                StatusId = 0
            };

            var result = await _orderServices.CreateBondOrder(orderModel);

            return Ok(result);
        }

        /// <summary>
        /// Crea una nueva orden de Activo tipo FCI.
        /// Solo se pueden usar los Activos de tipo FCI (AssetTypeId = 3).
        /// No se ingresa el precio, se obtiene del activo.
        /// No se aplican comisiones ni impuestos.
        /// </summary>
        /// <param name="createOrderDto">Datos de la orden.</param>
        /// <returns>Resultado de la operación de creación.</returns>
        [HttpPost("MutualFund")]
        public async Task<IActionResult> CreateMutualFundOrder([FromBody] CreateMutualFundOrderDto createOrderDto)
        {
            var orderModel = new OrderModel
            {

                AccountId = createOrderDto.AccountId,
                AssetId = createOrderDto.AssetId,
                Quantity = createOrderDto.Quantity,
                OperationTypeId = createOrderDto.OperationTypeId,
                StatusId = 0
            };

            var result = await _orderServices.CreateMutualFundOrder(orderModel);

            return Ok(result);
        }

        /// <summary>
        /// Actualiza el estado de una orden existente.
        /// Se puede cambiar el estado de la orden a:.
        /// ● 0 = En proceso.
        /// ● 1 = Ejecutada.
        /// ● 3 = Cancelada.
        /// </summary>
        /// <param name="id">Id de la orden a actualizar.</param>
        /// <param name="updateOrderDto">Datos para actualizar el estado de la orden.</param>
        /// <returns>Resultado de la operación de actualización.</returns>
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] UpdateOrderStatusDto updateOrderDto)
        {
            var result = await _orderServices.UpdateOrderStatus(id, updateOrderDto.StatusId);

            if (result.Data != null && result.Data.Id == 0) return NotFound();

            return Ok(result);
        }


        /// <summary>
        /// Elimina una orden del sistema.
        /// </summary>
        /// <param name="id">Id de la orden a eliminar.</param>
        /// <returns>Resultado de la operación de eliminación.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderServices.DeleteOrder(id);

            if (result.Data != null && result.Data.Id == 0) return NotFound();

            return Ok(result);
        }
    }
}