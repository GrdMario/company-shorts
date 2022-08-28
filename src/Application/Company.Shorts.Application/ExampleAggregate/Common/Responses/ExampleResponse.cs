﻿namespace Company.Shorts.Application.ExampleAggregate.Common.Responses
{
    using System;

    public class ExampleResponse
    {
        /// <summary>
        /// Unique identifier of an example.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name of an example.
        /// </summary>
        public string Name { get; set; } = default!;
    }
}
