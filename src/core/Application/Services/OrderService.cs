namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderStateService _orderStateService;
        private readonly IAssetService _assetService;
        private readonly IOperationTypeService _operationTypeService;

        public OrderService(ILogger<OrderService> logger,
            IOrderRepository orderRepository,
            IOrderStateService orderStateService,
            IAssetService assetService,
            IOperationTypeService operationTypeService)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _orderStateService = orderStateService;
            _assetService = assetService;
            _operationTypeService = operationTypeService;
        }

        /// <summary>
        /// Obtiene todas las órdenes
        /// </summary>
        /// <returns>Lista de todas las órdenes disponibles</returns>
        public async Task<ResultModel<List<OrderModel>>> GetOrdersAll()
        {
            var result = new ResultModel<List<OrderModel>>();

            try
            {
                result.Data = await _orderRepository.GetOrdersAll();
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
        /// Obtiene una orden específica por su id
        /// </summary>
        /// <param name="id">Id único de la orden</param>
        /// <returns>Orden encontrada o null si no existe</returns>
        public async Task<ResultModel<OrderDetailedModel>> GetOrderById(int id)
        {
            var result = new ResultModel<OrderDetailedModel>();

            try
            {
                result.Data = await _orderRepository.GetOrderById(id);


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
        /// Obtiene todas las órdenes asociadas a un activo específico
        /// </summary>
        /// <param name="assetId">Id del activo</param>
        /// <returns>Lista de órdenes del activo especificado</returns>
        public async Task<ResultModel<List<OrderModel>>> GetOrdersByAssetId(int assetId)
        {
            var result = new ResultModel<List<OrderModel>>();

            try
            {
                result.Data = await _orderRepository.GetOrdersByAssetId(assetId);

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
        /// Obtiene todas las órdenes asociadas a una cuenta específica
        /// </summary>
        /// <param name="accountId">Id de la cuenta</param>
        /// <returns>Lista de órdenes de la cuenta especificada</returns>
        public async Task<ResultModel<List<OrderModel>>> GetOrdersByAccountId(int accountId)
        {
            var result = new ResultModel<List<OrderModel>>();

            try
            {
                result.Data = await _orderRepository.GetOrdersByAccountId(accountId);

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
        /// Obtiene todas las órdenes que tienen un estado específico
        /// </summary>
        /// <param name="statusId">Id del estado de la orden</param>
        /// <returns>Lista de órdenes con el estado especificado</returns>
        public async Task<ResultModel<List<OrderModel>>> GetOrdersByStatusId(int statusId)
        {
            var result = new ResultModel<List<OrderModel>>();

            try
            {
                result.Data = await _orderRepository.GetOrdersByStatusId(statusId);
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
        /// Obtiene todas las órdenes que tienen un tipo de operación específico
        /// </summary>
        /// <param name="operationTypeId">Id del tipo de operación</param>
        /// <returns>Lista de órdenes con el tipo de operación especificado</returns>
        public async Task<ResultModel<List<OrderModel>>> GetOrdersByOperationTypeId(string operationTypeId)
        {
            var result = new ResultModel<List<OrderModel>>();

            try
            {
                var operationTypeResult = await _operationTypeService.GetOperationTypeById(operationTypeId);
                if (operationTypeResult.Data == null)
                {
                    result.AddInputDataError($"El tipo de operación con ID {operationTypeId} no existe.");
                    return result;
                }
                result.Data = await _orderRepository.GetOrdersByOperationTypeId(operationTypeId);
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
        /// Obtiene todas las órdenes asociadas a un tipo de activo específico
        /// </summary>
        /// <param name="assetTypeId">Id del tipo de activo</param>
        /// <returns>Lista de órdenes del tipo de activo especificado</returns>
        public async Task<ResultModel<List<OrderModel>>> GetOrdersByAssetTypeId(int assetTypeId)
        {
            var result = new ResultModel<List<OrderModel>>();

            try
            {
                result.Data = await _orderRepository.GetOrdersByAssetTypeId(assetTypeId);
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
        /// Valida que todos los datos requeridos para crear una orden sean correctos
        /// Para que una orden se pueda guardar, el estado, el activo y el tipo de operación deben existir en la db
        /// </summary>
        /// <param name="orderModel">Modelo de la orden a validar</param>
        /// <returns>Resultado de la validación junto con el activo asociado</returns>
        private async Task<(bool IsValid, AssetModel Asset, ResultModel<ResponseBaseModel> ValidationResult)> ValidateOrderCreation(OrderModel orderModel, int assetTypeId)
        {
            var result = new ResultModel<ResponseBaseModel>();

            var orderResult = await GetOrderById(orderModel.Id);
            if (orderResult.Data != null)
            {
                result.AddInputDataError($"La orden con ID {orderModel.Id} ya existe.");
                return (false, null, result);
            }

            var orderStateResult = await _orderStateService.GetOrderStateById(orderModel.StatusId);
            if (orderStateResult.Data == null)
            {
                result.AddInputDataError($"El estado de orden con ID {orderModel.StatusId} no existe.");
                return (false, null, result);
            }

            var assetResult = await _assetService.GetAssetById(orderModel.AssetId);
            if (assetResult.Data == null)
            {
                result.AddInputDataError($"El activo con ID {orderModel.AssetId} no existe.");
                return (false, null, result);
            }
            else if (assetResult.Data.AssetTypeId != assetTypeId)
            {
                result.AddInputDataError($"El activo con ID {orderModel.AssetId} no es del tipo correcto para esta orden.");
                return (false, null, result);
            }

            var operationTypeResult = await _operationTypeService.GetOperationTypeById(orderModel.OperationTypeId);
            if (operationTypeResult.Data == null)
            {
                result.AddInputDataError($"El tipo de operación con ID {orderModel.OperationTypeId} no existe.");
                return (false, null, result);
            }

            return (true, assetResult.Data, result);
        }

        /// <summary>
        /// Calcula el monto total de una orden FCI incluyendo comisiones e impuestos
        /// </summary>
        /// <param name="price">Precio unitario del activo el cual se ingresa a mano</param>
        /// <param name="quantity">Cantidad , ingresada a mano</param>
        /// <returns>Monto total con comisiones (0.2%) e impuestos (21% sobre comisiones)</returns>
        private decimal CalculateBondOrderTotal(decimal price, long quantity)
        {
            // Monto base = precio * cantidad
            var baseAmount = price * quantity;

            // Comisiones: 0.2% sobre el monto total
            var commission = baseAmount * 0.002m;

            // Impuestos: 21% sobre el valor de las comisiones
            var taxes = commission * 0.21m;

            // Monto total final
            var totalAmount = baseAmount + commission + taxes;

            return totalAmount;
        }

        /// <summary>
        /// Calcula el monto total de una orden de accion incluyendo comisiones e impuestos
        /// </summary>
        /// <param name="price">Precio unitario de la orden, obtenido de la base de datos</param>
        /// <param name="quantity">Cantidad ingresada a mano</param>
        /// <returns>Monto total con comisiones (0.6%) e impuestos (21% sobre comisiones)</returns>
        private decimal CalculateStockOrderTotal(decimal price, long quantity)
        {
            // Monto base = precio * cantidad
            var baseAmount = price * quantity;

            // Comisiones: 0.6% sobre el monto total
            var commission = baseAmount * 0.006m;

            // Impuestos: 21% sobre el valor de las comisiones
            var taxes = commission * 0.21m;

            // Monto total final
            var totalAmount = baseAmount + commission + taxes;

            return totalAmount;
        }

        /// <summary>
        /// Crea una nueva orden de con tipo de activo Acción
        /// </summary>
        /// <param name="orderModel">Modelo de la orden</param>
        /// <returns>Resultado de la operación de creación</returns>
        public async Task<ResultModel<ResponseBaseModel>> CreateStockOrder(OrderModel orderModel)
        {
            var result = new ResultModel<ResponseBaseModel>();

            try
            {
                var (isValid, asset, validationResult) = await ValidateOrderCreation(orderModel, 1);
                if (!isValid)
                {
                    return validationResult;
                }
                orderModel.Price = asset.UnitPrice;
                orderModel.TotalAmount = CalculateStockOrderTotal(orderModel.Price, orderModel.Quantity);
                result.Data = await _orderRepository.CreateOrder(orderModel);
                result.Message = "Orden creada exitosamente";
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
        /// Crea una nueva orden de con tipo de activo Bono
        /// </summary>
        /// <param name="orderModel">Modelo de la orden a crear</param>
        /// <returns>Resultado de la operación de creación</returns>
        public async Task<ResultModel<ResponseBaseModel>> CreateBondOrder(OrderModel orderModel)
        {
            var result = new ResultModel<ResponseBaseModel>();

            try
            {
                var (isValid, asset, validationResult) = await ValidateOrderCreation(orderModel, 2);
                if (!isValid)
                {
                    return validationResult;
                }

                // Calcular el monto total con comisiones e impuestos
                orderModel.TotalAmount = CalculateBondOrderTotal(orderModel.Price, orderModel.Quantity);

                result.Data = await _orderRepository.CreateOrder(orderModel);
                result.Message = "Orden creada exitosamente";
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
        /// Crea una nueva orden con tipo de activo FCI
        /// </summary>
        /// <param name="orderModel">Modelo de la orden crear</param>
        /// <returns>Resultado de la operación de creación</returns>
        public async Task<ResultModel<ResponseBaseModel>> CreateMutualFundOrder(OrderModel orderModel)
        {
            var result = new ResultModel<ResponseBaseModel>();

            try
            {
                var (isValid, asset, validationResult) = await ValidateOrderCreation(orderModel, 3);
                if (!isValid)
                {
                    return validationResult;
                }

                orderModel.Price = asset.UnitPrice;
                orderModel.TotalAmount = asset.UnitPrice * orderModel.Quantity;
                result.Data = await _orderRepository.CreateOrder(orderModel);
                result.Message = "Orden creada exitosamente";
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
        /// Actualiza el estado de una orden existente
        /// La orden y el estado nuevo deben existir en la base de datos
        /// </summary>
        /// <param name="orderId">Id de la orden a actualizar</param>
        /// <param name="statusId">Id de estado que se le va a colocar a la orden</param>
        /// <returns>Resultado de la operación de actualización</returns>
        public async Task<ResultModel<ResponseBaseModel>> UpdateOrderStatus(int orderId, int statusId)
        {
            var result = new ResultModel<ResponseBaseModel>();

            try
            {

                var orderResult = await GetOrderById(orderId);
                if (orderResult.Data == null)
                {
                    result.AddInputDataError($"La orden con ID {orderId} no existe.");
                    return result;
                }

                var orderStateResult = await _orderStateService.GetOrderStateById(statusId);
                if (orderStateResult.Data == null)
                {
                    result.AddInputDataError($"El estado de orden con ID {statusId} no existe.");
                    return result;
                }

                result.Data = await _orderRepository.UpdateOrderStatus(orderId, statusId);
                result.Message = "Estado de la orden modificado con éxito";
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
        /// Elimina una orden del sistema
        /// </summary>
        /// <param name="id">Id de la orden a eliminar</param>
        /// <returns>Resultado de la operación de eliminación</returns>
        public async Task<ResultModel<ResponseBaseModel>> DeleteOrder(int id)
        {
            var result = new ResultModel<ResponseBaseModel>();

            try
            {
                var orderResult = await GetOrderById(id);
                if (orderResult.Data == null)
                {
                    result.AddInputDataError($"La orden con ID {id} no existe.");
                    return result;
                }
                result.Data = await _orderRepository.DeleteOrder(id);
                result.Message = "Orden eliminada con éxito";
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