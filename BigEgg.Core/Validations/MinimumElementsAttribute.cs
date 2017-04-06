namespace BigEgg.Validations
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Specifies that a data field (a IEnumerable property) have at lease some elements.
    /// </summary>
    /// <seealso cref="IEnumerable"/>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class MinimumElementsAttribute : ValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MinimumElementsAttribute"/> class.
        /// </summary>
        /// <param name="minElementsCount">The minimum elements count.</param>
        public MinimumElementsAttribute(int minElementsCount)
        {
            Preconditions.Check(minElementsCount >= 0, "The minimum elements count cannot less than 0.");

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
        /// true if the data field (list) has at least some elements; otherwise, false.
        /// </returns>
        public override bool IsValid(object value)
        {
            if (value == null) { return AllowNull; }
            Preconditions.Check<ValidationException>(value is IEnumerable, $"The property should be a IEnumerable data.");

            var count = 0;

            if (value is ICollection)
            {
                count = (value as ICollection).Count;
            }
            else if (value is ICollection<object>)
            {
                count = (value as ICollection<object>).Count;
            }
            else
            {
                var enumerator = (value as IEnumerable).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    count++;
                }
            }
            return count >= MinElementsCount;
        }
    }
}
