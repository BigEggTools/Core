namespace BigEgg.Validations
{
    using System;
    using System.Collections;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Specifies that a data field (a list) have at lease some elements.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class MinimumElementsAttribute : ValidationAttribute
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="MinimumElementsAttribute"/> class.
        /// </summary>
        /// <param name="minElementsCount">The minimum elements count.</param>
        public MinimumElementsAttribute(int minElementsCount)
        {
            MinElementsCount = minElementsCount;
        }

        /// <summary>
        /// Gets the minimum elements count.
        /// </summary>
        /// <value>
        /// The minimum elements count.
        /// </value>
        public int MinElementsCount { get; private set; }

        /// <summary>
        /// Gets a value that indicates whether the attribute requires validation context.
        /// </summary>
        public override bool RequiresValidationContext { get { return false; } }


        /// <summary>
        /// Gets or sets a value indicating whether the validation allow null.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the validation allow null; otherwise, <c>false</c>.
        /// </value>
        public bool AllowNull { get; set; }


        /// <summary>
        /// Returns true if the data field (list) has at least some elements.
        /// </summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <returns>
        /// true if if the data field (list) has at least some elements; otherwise, false.
        /// </returns>
        public override bool IsValid(object value)
        {
            if (value == null && AllowNull) { return true; }

            Preconditions.Check<ValidationException>(value is IList, $"The property should be a list.");

            var list = value as IList;
            return list.Count >= MinElementsCount;
        }
    }
}
