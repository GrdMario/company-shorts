namespace Company.Shorts.Presentation.Api.Internal.Configuration.Rest
{
    using Microsoft.AspNetCore.Routing;
    using System.Text.RegularExpressions;

    internal sealed class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        private const string SlugifyRegexPattern = "([a-z])([A-Z])";
        private const string SlugifyRegexReplacement = "$1-$2";

        public string? TransformOutbound(object? value)
        {
            return value is not null
                ? Regex.Replace(value.ToString()!, SlugifyRegexPattern, SlugifyRegexReplacement).ToLower()
                : null;
        }
    }
}