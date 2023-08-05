using System.ComponentModel.DataAnnotations;

namespace Common.Validators
{
    public class ValidGuidAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var stringValue = value as string;

            if (!string.IsNullOrEmpty(stringValue))
            {
                Guid guidValue;
                return Guid.TryParse(stringValue, out guidValue);
            }

            return true;
        }
    }
}
