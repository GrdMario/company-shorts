namespace Company.Shorts.Blocks.Common.Extensions
{ 
    using System.Text;
    using System.Web;

    public static class StringExtensions
    {
        public static string Base64Encoding(this string input, Encoding? encoding = null)
        {
            encoding ??= Encoding.UTF8;

            return Convert.ToBase64String(encoding.GetBytes(input));
        }

        public static string? GetQueryParamValue(this string? url, string paramName)
        {
            if (url is null)
            {
                return null;
            }

            if (!Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                return null;
            }

            HttpUtility
                .UrlDecode(new Uri(url).Query)
                .TrimStart('?')
                .Split('&')
                .Select(queryParam =>
                {
                    var keyValue = queryParam.Split('=');
                    return new { Key = keyValue[0], Value = keyValue[1] };
                })
                .ToDictionary(item => item.Key, item => item.Value)
                .TryGetValue(paramName, out var paramValue);

            return paramValue;
        }
    }
}