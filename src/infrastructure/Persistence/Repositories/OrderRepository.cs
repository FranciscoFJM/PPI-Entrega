namespace Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly PPIContext _dbContext;

        public OrderRepository(PPIContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Obtiene todas las órdenes almacenadas en la base de datos
        /// </summary>
        /// <returns>Lista de todas las órdenes</returns>
        public async Task<List<OrderModel>> GetOrdersAll()
        {
            try
            {
                var query = from o in _dbContext.Orders

                            select new OrderModel
                            {
                                Id = o.Id,
                                AccountId = o.AccountId,
                                AssetId = o.AssetId,

                                Quantity = o.Quantity,
                                Price = o.Price,
                                OperationTypeId = o.OperationTypeId,
                                StatusId = o.StatusId,
                                TotalAmount = o.TotalAmount,

                            };
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DbPersistenceException("Persistence Layer Failure: GetOrdersAll", ex.Message);
            }
        }

        /// <summary>
        /// Obtiene una orden específica por su Id con información detallada
        /// Se incluyen las descripciones del activo, tipo de activo, tipo de operación y estado de la orden
        /// </summary>
        /// <param name="id">Id único de la orden</param>
        /// <returns>Orden con información completa</returns>
        public async Task<OrderDetailedModel> GetOrderById(int id)
        {
            try
            {
                var query = from o in _dbContext.Orders
                            join a in _dbContext.Assets on o.AssetId equals a.Id into assetJoin
                            from asset in assetJoin.DefaultIfEmpty()
                            join at in _dbContext.AssetTypes on asset.AssetTypeId equals at.Id into assetTypeJoin
                            from assetType in assetTypeJoin.DefaultIfEmpty()
                            join ot in _dbContext.OperationTypes on o.OperationTypeId equals ot.Id into operationTypeJoin
                            from operationType in operationTypeJoin.DefaultIfEmpty()
                            join os in _dbContext.OrderStates on o.StatusId equals os.Id into orderStateJoin
                            from orderState in orderStateJoin.DefaultIfEmpty()
                            where o.Id == id
                            select new OrderDetailedModel
                            {
                                Id = o.Id,
                                AccountId = o.AccountId,
                                AssetId = o.AssetId,
                                Quantity = o.Quantity,
                                Price = o.Price,
                                OperationTypeId = o.OperationTypeId,
                                StatusId = o.StatusId,
                                TotalAmount = o.TotalAmount,
                                AssetName = asset != null ? asset.Name : null,
                                AssetType = asset != null ? asset.AssetTypeId : 0,
                                AssetTypeDescription = assetType != null ? assetType.Description : null,
                                OperationTypeDescription = operationType != null ? operationType.Description : null,
                                StatusDescription = orderState != null ? orderState.Description : null

                            };
                return await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new DbPersistenceException("Persistence Layer Failure: GetOrderById", ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todas las órdenes asociadas a un activo específico por su id
        /// </summary>
        /// <param name="assetId">Id del activo</param>
        /// <returns>Lista de órdenes del activo especificado</returns>
        public async Task<List<OrderModel>> GetOrdersByAssetId(int assetId)
        {
            try
            {
                var query = from o in _dbContext.Orders
                            where o.AssetId == assetId
                            select new OrderModel
                            {
                                Id = o.Id,
                                AccountId = o.AccountId,
                                AssetId = o.AssetId,

                                Quantity = o.Quantity,
                                Price = o.Price,
                                OperationTypeId = o.OperationTypeId,
                                StatusId = o.StatusId,
                                TotalAmount = o.TotalAmount,
                            };
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DbPersistenceException("Persistence Layer Failure: GetOrdersByAssetId", ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todas las órdenes asociadas a una cuenta específica
        /// </summary>
        /// <param name="accountId">Id de la cuenta</param>
        /// <returns>Lista de órdenes de la cuenta especificada</returns>
        public async Task<List<OrderModel>> GetOrdersByAccountId(int accountId)
        {
            try
            {
                var query = from o in _dbContext.Orders
                            where o.AccountId == accountId
                            select new OrderModel
                            {
                                Id = o.Id,
                                AccountId = o.AccountId,
                                AssetId = o.AssetId,

                                Quantity = o.Quantity,
                                Price = o.Price,
                                OperationTypeId = o.OperationTypeId,
                                StatusId = o.StatusId,
                                TotalAmount = o.TotalAmount,
                            };
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DbPersistenceException("Persistence Layer Failure: GetOrdersByAccountId", ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todas las órdenes que tienen un estado específico
        /// </summary>
        /// <param name="statusId">Id del estado de la orden</param>
        /// <returns>Lista de órdenes con el estado especificado</returns>
        public async Task<List<OrderModel>> GetOrdersByStatusId(int statusId)
        {
            try
            {
                var query = from o in _dbContext.Orders
                            where o.StatusId == statusId
                            select new OrderModel
                            {
                                Id = o.Id,
                                AccountId = o.AccountId,
                                AssetId = o.AssetId,

                                Quantity = o.Quantity,
                                Price = o.Price,
                                OperationTypeId = o.OperationTypeId,
                                StatusId = o.StatusId,
                                TotalAmount = o.TotalAmount,
                            };
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DbPersistenceException("Persistence Layer Failure: GetOrdersByStatusId", ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todas las órdenes que tienen un tipo de operación específico
        /// </summary>
        /// <param name="operationTypeId">Id del tipo de operación</param>
        /// <returns>Lista de órdenes con el tipo de operación especificado</returns>
        public async Task<List<OrderModel>> GetOrdersByOperationTypeId(string operationTypeId)
        {
            try
            {
                var query = from o in _dbContext.Orders
                            where o.OperationTypeId == operationTypeId
                            select new OrderModel
                            {
                                Id = o.Id,
                                AccountId = o.AccountId,
                                AssetId = o.AssetId,

                                Quantity = o.Quantity,
                                Price = o.Price,
                                OperationTypeId = o.OperationTypeId,
                                StatusId = o.StatusId,
                                TotalAmount = o.TotalAmount,
                            };
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DbPersistenceException("Persistence Layer Failure: GetOrdersByOperationTypeId", ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todas las órdenes asociadas a un tipo de activo específico
        /// </summary>
        /// <param name="assetTypeId">Id del tipo de activo</param>
        /// <returns>Lista de órdenes del tipo de activo especificado</returns>
        public async Task<List<OrderModel>> GetOrdersByAssetTypeId(int assetTypeId)
        {
            try
            {
                var query = from o in _dbContext.Orders
                            join a in _dbContext.Assets on o.AssetId equals a.Id
                            where a.AssetTypeId == assetTypeId
                            select new OrderModel
                            {
                                Id = o.Id,
                                AccountId = o.AccountId,
                                AssetId = o.AssetId,
                                Quantity = o.Quantity,
                                Price = o.Price,
                                OperationTypeId = o.OperationTypeId,
                                StatusId = o.StatusId,
                                TotalAmount = o.TotalAmount,
                            };
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DbPersistenceException("Persistence Layer Failure: GetOrdersByAssetTypeId", ex.Message);
            }
        }

        /// <summary>
        /// Crea una nueva orden en la base de datos
        /// </summary>
        /// <param name="orderModel">Modelo de la orden a crear</param>
        /// <returns>Respuesta con el resultado de la operación</returns>
        public async Task<ResponseBaseModel> CreateOrder(OrderModel orderModel)
        {
            var result = new ResponseBaseModel();

            try
            {
                var order = new Order()
                {
                    Id = orderModel.Id,
                    AccountId = orderModel.AccountId,
                    AssetId = orderModel.AssetId,

                    Quantity = orderModel.Quantity,
                    Price = orderModel.Price,
                    OperationTypeId = orderModel.OperationTypeId,
                    StatusId = orderModel.StatusId,
                    TotalAmount = orderModel.TotalAmount,
                };

                _dbContext.Orders.Add(order);
                await _dbContext.SaveChangesAsync();

                result.Id = order.Id;

                return result;
            }
            catch (Exception ex)
            {
                throw new DbPersistenceException("Persistence Layer Failure: CreateOrder", ex.Message);
            }
        }


        /// <summary>
        /// Actualiza únicamente el estado de una orden existente
        /// </summary>
        /// <param name="orderId">Id de la orden a actualizar</param>
        /// <param name="statusId">Nuevo Id del estado</param>
        /// <returns>Respuesta con el resultado de la operación</returns>
        public async Task<ResponseBaseModel> UpdateOrderStatus(int orderId, int statusId)
        {
            var result = new ResponseBaseModel();

            try
            {

                var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == orderId);

                if (order != null)
                {
                    order.StatusId = statusId;
                    await _dbContext.SaveChangesAsync();
                    result.Id = order.Id;
                }



                return result;
            }
            catch (Exception ex)
            {
                throw new DbPersistenceException("Persistence Layer Failure: UpdateOrderStatus", ex.Message);
            }
        }

        /// <summary>
        /// Elimina una orden de la base de datos
        /// </summary>
        /// <param name="id">Id de la orden a eliminar</param>
        /// <returns>Respuesta con el resultado de la operación</returns>
        public async Task<ResponseBaseModel> DeleteOrder(int id)
        {
            var result = new ResponseBaseModel();

            try
            {
                var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);

                if (order != null)
                {
                    _dbContext.Orders.Remove(order);
                    await _dbContext.SaveChangesAsync();

                    result.Id = id;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new DbPersistenceException("Persistence Layer Failure: DeleteOrder", ex.Message);
            }
        }
    }
}