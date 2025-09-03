namespace Domain.Services
{
    public interface IOperationTypeService
    {
        Task<ResultModel<List<OperationTypeModel>>> GetOperationTypesAll();
        Task<ResultModel<OperationTypeModel>> GetOperationTypeById(string id);
    }
}
