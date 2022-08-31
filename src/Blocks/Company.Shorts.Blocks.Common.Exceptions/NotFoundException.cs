namespace Company.Shorts.Blocks.Common.Exceptions
{
    using System;
    using System.Collections.Generic;

    public class NotFoundException : ApplicationException
    {
        /// <summary>
        /// Creates an instance of <see cref="NotFoundException"/>.
        /// </summary>
        /// <param name="message">Error message.</param>
        public NotFoundException(string message)
        {
            this.Title = message;
            this.Detail = "Unable to find requested resource.";
            this.Errors = new Dictionary<string, string[]>();
        }

        /// <summary>
        /// Error title.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Error detail.
        /// </summary>
        public string Detail { get; }

        /// <summary>
        /// Errors.
        /// </summary>
        public Dictionary<string, string[]> Errors { get; }
    }
}
