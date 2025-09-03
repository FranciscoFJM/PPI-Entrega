namespace Domain.Repositories
{
    public interface IOrderStateRepository
    {
        Task<List<OrderStateModel>> GetOrderStatesAll();
        Task<OrderStateModel> GetOrderStateById(int id);
    }
}
