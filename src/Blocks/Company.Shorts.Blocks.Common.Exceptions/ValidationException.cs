namespace Company.Shorts.Blocks.Common.Exceptions
{
    using System;
    using System.Collections.Generic;

    public class ValidationException : ApplicationException
    {

        private const string TitleMessage = "One or more validation errors occurred.";
        private const string DetailMessage = "Check the errors for more details.";
        private const string GeneralKey = "General";

        /// <summary>
        /// Creates an instance of <see cref="ValidationException"/>.
        /// </summary>
        /// <remarks>
        /// Creates a new dictionary of errors with "General" key.
        /// </remarks>
        /// <param name="message">Error message.</param>
        public ValidationException(string message)
        {
            this.Title = TitleMessage;
            this.Detail = DetailMessage;
            this.Errors = new Dictionary<string, string[]>
            {
                {
                    GeneralKey , new string[] { message }
                }
            };
        }

        /// <summary>
        /// Creates an instance of <see cref="ValidationException"/>.
        /// </summary>
        /// <param name="errors">Errors.</param>
        public ValidationException(Dictionary<string, string[]> errors)
        {
            this.Title = TitleMessage;
            this.Detail = DetailMessage;
            this.Errors = errors;
        }

        /// <summary>
        /// Error title.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Error details.
        /// </summary>
        public string Detail { get; }

        /// <summary>
        /// Errors.
        /// </summary>
        public Dictionary<string, string[]> Errors { get; }
    }
}
