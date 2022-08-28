namespace Company.Shorts.Presentation.Api.Controllers.V2.Models.Examples
{
    /// <summary>
    /// Request for updating existing example.
    /// </summary>
    public class UpdateExampleCommandDto : IApiDto
    {
        public UpdateExampleCommandDto(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Name to be updated.
        /// </summary>
        public string Name { get; set; }
    }
}
