namespace Company.Shorts.Infrastructure.HttpContextAccessorAdapter.Internal
{
    using Company.Shorts.Application.Contracts.HttpContextAccessor;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;
    using System;
    using System.ComponentModel;

    internal sealed class DefaultHttpContextAccessorAdapter : IHttpContextAccessorAdapter
    {
        private const string NameHeaderValue = "x-name";
        private readonly IHttpContextAccessor httpContextAccessor;

        public DefaultHttpContextAccessorAdapter(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetName()
        {
            return this.GetRequiredHeaderValue<string>(NameHeaderValue);
        }

        private T GetRequiredHeaderValue<T>(string key)
        {
            StringValues values = GetValues(key);

            return values.Count switch
            {
                0 => throw new InvalidOperationException($"Value not found for header '{key}'."),
                > 1 => throw new InvalidOperationException($"Multiple unexpected values found for header '{key}'."),
                _ => ConvertSafeTo<T>(values)
            };
        }

        private static T? ConvertTo<T>(string value, TypeConverter? converter = null)
        {
            converter ??= TypeDescriptor.GetConverter(typeof(T));

            return (T?)converter.ConvertFromString(value);
        }

        private static T ConvertSafeTo<T>(string value, TypeConverter? converter = null)
        {
            T? convertedValue = ConvertTo<T>(value, converter);

            if (convertedValue is null)
            {
                throw new InvalidOperationException($"Unable to convert header value '{value}' to type of '{typeof(T)}'.");
            }

            return convertedValue;
        }

        private StringValues GetValues(string key)
        {
            this.httpContextAccessor!.HttpContext!.Request.Headers.TryGetValue(key, out var values);

            return values;
        }
    }
}
