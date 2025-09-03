namespace API.Validators;

public class CreateStockOrderValidator : AbstractValidator<CreateStockOrderDto>
{
       public CreateStockOrderValidator()
       {


              RuleFor(x => x.AccountId)
                     .NotEmpty().WithMessage("El id de cuenta es requerido");

              RuleFor(x => x.AssetId)
                     .NotEmpty().WithMessage("El id de activo es requerido");

              RuleFor(x => x.OperationTypeId)
                     .NotEmpty().WithMessage("El id de tipo de operación es requerido");

              RuleFor(x => x.Quantity)
                  .GreaterThan(0)
                  .WithMessage("La cantidad debe ser mayor a 0");
       }
}
