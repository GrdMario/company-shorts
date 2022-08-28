namespace Company.Shorts.Presentation.Api.Controllers.V2.Models.Examples
{
    using System;

    /// <summary>
    /// Example response.
    /// </summary>
    public class ExampleResponseDto
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
