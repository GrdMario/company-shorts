namespace Company.Shorts.Blocks.Common.Exceptions
{
    using System;
    using System.Collections.Generic;

    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message)
        {
            Title = message;
            Detail = "Check the errors for more details";
            Errors = new Dictionary<string, string[]>();
        }

        public string Title { get; }

        public string Detail { get; }

        public Dictionary<string, string[]> Errors { get; }
    }
}
