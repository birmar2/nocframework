using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common.Interfaces;
using Common.Models;

namespace Common
{
    public static class Validator<T>
    {
        /// <summary>
        /// Validates a DTO's properties with custom attributes.
        /// </summary>
        /// <param name="input">input object (DTO)</param>
        /// <returns></returns>
        public static ValidationResult Validate(T input)
        {
            ValidationResult result = new ValidationResult();
            //Getting input object's properties, checking for custom attributes

            if (input.GetType().Assembly.GetName().Name == "mscorlib")
            {
                result.Errors.Add("Input can't be validated!");
                return result;
            }

            PropertyInfo[] properties = input.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                var attributes = property.GetCustomAttributes();
                foreach (var attribute in attributes)
                {
                    if (attribute != null)
                    {
                        var validation = new ValidationResult();
                        //Validating string type attributes with it's own validation method

                        if (property.GetValue(input).GetType() == typeof(string))
                        {
                            var validationAttribute = (IValidationAttribute<string>)attribute;
                            validation = validationAttribute.Validate((string)property.GetValue(input));
                        }
                        result.Errors.AddRange(validation.Errors);
                    }
                }
            }

            return result;
        }
    }
}
