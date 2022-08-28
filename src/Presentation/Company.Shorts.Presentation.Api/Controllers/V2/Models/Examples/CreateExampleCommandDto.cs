namespace Company.Shorts.Presentation.Api.Controllers.V2.Models.Examples
{

    /// <summary>
    /// Request for creating an example.
    /// </summary>
    public class CreateExampleCommandDto : IApiDto
    {
        public CreateExampleCommandDto(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Name of an example.
        /// </summary>
        public string Name { get; set; }
    }
}
