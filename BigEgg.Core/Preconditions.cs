namespace BigEgg
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Hold all the precondition logics.
    /// </summary>
    public class Preconditions
    {
        /// <summary>
        /// Throw an <see cref="ArgumentNullException" /> if the object is null.
        /// </summary>
        /// <param name="obj">The object which need to check.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void NotNull(object obj, string paramName = "", [CallerMemberName] string methodName = "")
        {
            if (obj == null)
            {
                throw new ArgumentNullException($"Parameter '{paramName}' in '{methodName}' should not be null.");
            }
        }

        /// <summary>
        /// Throw an <see cref="ArgumentException" /> if a specified string is null,
        /// empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <exception cref="ArgumentException"></exception>
        public static void NotNullOrWhiteSpace(string value, string paramName = "", [CallerMemberName] string methodName = "")
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"Parameter '{paramName}' in '{methodName}' should not be empty string.");
            }
        }

        /// <summary>
        /// Throw an <see cref="ArgumentException" /> if a specified collection is null or empty.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public static void NotEmpty<T>(ICollection<T> data, string paramName = "", [CallerMemberName] string methodName = "")
        {
            if (data == null || data.Count == 0)
            {
                throw new ArgumentException($"Parameter '{paramName}' in '{methodName}' should not be null or empty list.");
            }
        }

        /// <summary>
        /// Throw an <see cref="ArgumentException" /> if a specified collection is null or empty.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public static void NotEmpty(ICollection data, string paramName = "", [CallerMemberName] string methodName = "")
        {
            if (data == null || data.Count == 0)
            {
                throw new ArgumentException($"Parameter '{paramName}' in '{methodName}' should not be null or empty list.");
            }
        }

        /// <summary>
        /// Throw an <see cref="ArgumentException" /> if a specified enumeration is null or empty.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public static void NotEmpty<T>(IEnumerable<T> data, string paramName = "", [CallerMemberName] string methodName = "")
        {
            if (data == null)
            {
                using (var enumerator = (data as IEnumerable<object>).GetEnumerator())
                if (!enumerator.MoveNext())
                {
                    throw new ArgumentException($"Parameter '{paramName}' in '{methodName}' should not be null or empty list.");
                }
            }
        }

        /// <summary>
        /// Throw an <see cref="ArgumentException" /> if a specified enumeration is null or empty.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public static void NotEmpty(IEnumerable data, string paramName = "", [CallerMemberName] string methodName = "")
        {
            if (data == null || !data.GetEnumerator().MoveNext())
            {
                throw new ArgumentException($"Parameter '{paramName}' in '{methodName}' should not be null or empty list.");
            }
        }

        /// <summary>
        /// Throw an <see cref="ArgumentException" /> if the condition is false.
        /// </summary>
        /// <param name="condition">The check condition.</param>
        /// <param name="messageFormat">The error message format.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <exception cref="ArgumentException"></exception>
        public static void Check(bool condition, string messageFormat = "", string paramName = "", [CallerMemberName] string methodName = "")
        {
            if (!condition)
            {
                var errorMessage = string.IsNullOrWhiteSpace(messageFormat)
                    ? paramName
                    : string.Format(messageFormat, paramName, methodName);
                throw new ArgumentException(errorMessage);
            }
        }

        /// <summary>
        /// Throw an <see cref="Exception" /> if the condition is false.
        /// </summary>
        /// <typeparam name="TException">The type of the exception should thrown.</typeparam>
        /// <param name="condition">The check condition.</param>
        /// <param name="messageFormat">The error message format.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="methodName">Name of the method.</param>
        public static void Check<TException>(bool condition, string messageFormat = "", string paramName = "", [CallerMemberName] string methodName = "") where TException : Exception
        {
            if (!condition)
            {
                var errorMessage = string.IsNullOrWhiteSpace(messageFormat)
                    ? paramName
                    : string.Format(messageFormat, paramName, methodName);
                throw (TException)Activator.CreateInstance(typeof(TException), new object[] { errorMessage });
            }
        }
    }
}
