using System;
using System.ComponentModel.DataAnnotations;

namespace FormularioContactoAPI.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ProhibitDomainsAttribute : ValidationAttribute
    {
        private readonly string[] _prohibitedDomains;

        public ProhibitDomainsAttribute(params string[] prohibitedDomains)
        {
            _prohibitedDomains = prohibitedDomains;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var email = value.ToString();
            var domain = email.Substring(email.IndexOf('@') + 1);

            if (_prohibitedDomains.Contains(domain))
            {
                return new ValidationResult("El dominio de correo electrónico está prohibido.");
            }

            return ValidationResult.Success;
        }
    }
}
