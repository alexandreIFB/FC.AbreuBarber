

using FluentValidation;

namespace FC.AbreuBarber.Application.UseCases.Procedure.UpdateProcedure
{
    public class UpdateProcedureInputValidator : AbstractValidator<UpdateProcedureInput>
    {
        public UpdateProcedureInputValidator() {
            ValidatorOptions.Global.LanguageManager.Enabled = true;
            ValidatorOptions.Global.LanguageManager.Culture = new System.Globalization.CultureInfo("en");

            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
