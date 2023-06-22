

using FC.AbreuBarber.Application.UseCases.Procedure.GetProcedure;
using FluentValidation;

namespace FC.AbreuBarber.Application.UseCases.Procedure.CreateProcedure
{
    public class GetProcedureInputValidator : AbstractValidator<GetProcedureInput>
    {
        public GetProcedureInputValidator() {
            ValidatorOptions.Global.LanguageManager.Enabled = true;
            ValidatorOptions.Global.LanguageManager.Culture = new System.Globalization.CultureInfo("en");

            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
