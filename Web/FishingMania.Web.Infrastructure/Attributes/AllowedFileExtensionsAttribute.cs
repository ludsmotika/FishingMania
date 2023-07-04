﻿namespace FishingMania.Web.Infrastructure.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FishingMania.Common;
    using Microsoft.AspNetCore.Http;

    public class AllowedFileExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] extensions = GlobalConstants.AllowedImageExtensions;

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);

                if (!this.extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(this.GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return GlobalConstants.AllowedExtensionsErrorMessage;
        }
    }
}
