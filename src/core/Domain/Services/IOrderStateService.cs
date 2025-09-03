namespace Domain.Services
{
    public interface IOrderStateService
    {
        Task<ResultModel<List<OrderStateModel>>> GetOrderStatesAll();
        Task<ResultModel<OrderStateModel>> GetOrderStateById(int id);
    }
}
