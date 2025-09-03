namespace Domain.Repositories
{
    public interface IOperationTypeRepository
    {
        Task<List<OperationTypeModel>> GetOperationTypesAll();
        Task<OperationTypeModel> GetOperationTypeById(string id);
    }
}
