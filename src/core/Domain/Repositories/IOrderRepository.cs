namespace Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<List<OrderModel>> GetOrdersAll();
        Task<OrderDetailedModel> GetOrderById(int id);
        Task<List<OrderModel>> GetOrdersByAssetId(int assetId);
        Task<List<OrderModel>> GetOrdersByAccountId(int accountId);
        Task<List<OrderModel>> GetOrdersByStatusId(int statusId);
        Task<List<OrderModel>> GetOrdersByAssetTypeId(int assetTypeId);
        Task<ResponseBaseModel> CreateOrder(OrderModel orderModel);
        Task<ResponseBaseModel> UpdateOrderStatus(int orderId, int statusId);
        Task<ResponseBaseModel> DeleteOrder(int id);
        Task<List<OrderModel>> GetOrdersByOperationTypeId(string operationTypeId);
    }
}