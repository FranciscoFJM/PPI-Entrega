namespace API.Validators;

public class CreateBondOrderValidator : AbstractValidator<CreateBondOrderDto>
{
       public CreateBondOrderValidator()
       {


              RuleFor(x => x.AccountId)
                     .NotEmpty().WithMessage("El id de cuenta es requerido");

              RuleFor(x => x.AssetId)
                     .NotEmpty().WithMessage("El id de activo es requerido");

              RuleFor(x => x.OperationTypeId)
                     .NotEmpty().WithMessage("El id de tipo de operación es requerido");

              RuleFor(x => x.Quantity)
                  .NotEmpty().WithMessage("La cantidad es requerida")
                  .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0");

              RuleFor(x => x.Price)
                 .NotEmpty().WithMessage("El precio es requerido")
                  .GreaterThan(0).WithMessage("El precio debe ser mayor a 0");
       }
}
