namespace BigEgg.Validations
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;

    /// <summary>
    /// Specifies that a data field value is required when another property's value is equal to some specific data.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class RequiredIfAttribute : ValidationAttribute
    {
        private string dependentPropertyName;
        private object matchValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredIfAttribute" /> class with the specified property name
        /// and the value which should be equal to.
        /// </summary>
        /// <param name="dependentPropertyName">The specified property name which need to be compared.</param>
        /// <param name="matchValue">The specified value to compare.</param>
        public RequiredIfAttribute(string dependentPropertyName, object matchValue)
        {
            Preconditions.NotNullOrWhiteSpace(dependentPropertyName, "dependentPropertyNamw");
            Preconditions.NotNull(matchValue, "matchValue");

            this.dependentPropertyName = dependentPropertyName;
            this.matchValue = matchValue;
        }


        /// <summary>
        /// Gets or sets a value that indicates whether an empty string is allowed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if an empty string is allowed; otherwise, <c>false</c>. The default value is false.
        /// </value>
        public bool AllowEmptyStrings { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether dependent property's value should match or differ from the match value.
        /// </summary>
        /// <value>
        ///   <c>true</c> if dependent property's value validation should be inverted; otherwise, <c>false</c>.
        /// </value>
        public bool IsInverted { get; set; }


        /// <summary>
        /// Validates the specified value with respect to the current validation attribute.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>An instance of the <see cref="ValidationResult"/> class.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Preconditions.NotNull(validationContext, "validationContext");

            PropertyInfo dependentProperty = validationContext.ObjectType.GetProperty(dependentPropertyName);
            if (dependentProperty == null) { throw new ValidationException($"Cannot find the dependent property '{dependentPropertyName}'."); }

            var dependentPropertyValue = dependentProperty.GetValue(validationContext.ObjectInstance);
            if (dependentPropertyValue.GetType() != matchValue.GetType()) { throw new ValidationException($"The dependent property '{dependentPropertyName}' ({matchValue.GetType().Name}) has different type with the match value ({matchValue.GetType().Name})."); }

            if (!IsInverted && Equals(dependentPropertyValue, matchValue) ||
                IsInverted && !Equals(dependentPropertyValue, matchValue))
            {
                if ((value == null) ||
                    (value is string && (string.IsNullOrWhiteSpace(value as string) && !AllowEmptyStrings)))
                {
                    return IsInverted
                        ? new ValidationResult(ErrorMessage ?? $"'{validationContext.DisplayName}' is mandatory because value of '{dependentPropertyName}' not match '{matchValue}'.")
                        : new ValidationResult(ErrorMessage ?? $"'{validationContext.DisplayName}' is mandatory because value of '{dependentPropertyName}' matches '{matchValue}'.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
