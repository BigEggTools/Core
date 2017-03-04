namespace BigEgg.UnitTesting
{
    using System;

    /// <summary>
    /// This class contains assert methods which can be used in unit tests.
    /// </summary>
    public static class AssertHelper
    {
        /// <summary>
        /// Asserts that the execution of the provided action throws the specified exception.
        /// </summary>
        /// <typeparam name="T">The expected exception type</typeparam>
        /// <param name="action">The action to execute.</param>
        public static void ExpectedException<T>(Action action) where T : Exception
        {
            if (action == null) { throw new ArgumentNullException("action"); }

            try
            {
                action();
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(T)) { return; }

                throw new AssertException($"Test method threw exception {ex.GetType().Name}, but exception {typeof(T).Name} was expected. Exception message: {ex.GetType().Name}: {ex.Message}");
            }

            throw new AssertException($"Test method did not throw expected exception {typeof(T).Name}");
        }
    }
}
