namespace Fc.AbreuBarber.Api.ApiModels.Procedure
{
    public class UpdateProcedureApiInput
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public double? Value { get; set; }

        public UpdateProcedureApiInput(
            string name,
            double? value = null,
            string? description = null,
            bool? isActive = null
        )
        {
            Name = name;
            Value = value;
            Description = description;
            IsActive = isActive;
        }
    }
}
