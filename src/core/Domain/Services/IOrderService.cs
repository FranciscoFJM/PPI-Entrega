namespace Domain.Services
{
    public interface IOrderService
    {
        Task<ResultModel<List<OrderModel>>> GetOrdersAll();
        Task<ResultModel<OrderDetailedModel>> GetOrderById(int id);
        Task<ResultModel<List<OrderModel>>> GetOrdersByAssetId(int assetId);
        Task<ResultModel<List<OrderModel>>> GetOrdersByAccountId(int accountId);
        Task<ResultModel<List<OrderModel>>> GetOrdersByStatusId(int statusId);
        Task<ResultModel<List<OrderModel>>> GetOrdersByAssetTypeId(int assetTypeId);
        Task<ResultModel<List<OrderModel>>> GetOrdersByOperationTypeId(string operationTypeId);
        Task<ResultModel<ResponseBaseModel>> CreateStockOrder(OrderModel orderModel);
        Task<ResultModel<ResponseBaseModel>> CreateBondOrder(OrderModel orderModel);
        Task<ResultModel<ResponseBaseModel>> CreateMutualFundOrder(OrderModel orderModel);
        Task<ResultModel<ResponseBaseModel>> UpdateOrderStatus(int orderId, int statusId);
        Task<ResultModel<ResponseBaseModel>> DeleteOrder(int id);
    }
}