using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Validations
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string [] _allowedExtensions;
        public AllowedExtensionsAttribute(string[] allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName).ToLower();
                if (!_allowedExtensions.Contains(extension))
                    return new ValidationResult($"Allowed Extensions : {string.Join(", ", _allowedExtensions)}");
            }
            return ValidationResult.Success;
        }
    }
}
