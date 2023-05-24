namespace Company.Shorts.Blocks.Application.Core.Adapters
{
    using Company.Shorts.Blocks.Application.Contracts;
    using Microsoft.Extensions.Primitives;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    using Microsoft.AspNetCore.Connections.Features;
    using Microsoft.AspNetCore.Http;

    public class HttpContextAccessorAdapter : IHttpContextAccessorAdapter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextAccessorAdapter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public T? GetHeaderValue<T>(string key)
        {
            StringValues values = GetValues(key);

            return values.Count is 0 or > 1 ? default : ConvertTo<T>(values);
        }

        public List<T> GetHeaderValues<T>(string key)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));

            return GetValues(key)
                .Select(value => ConvertSafeTo<T>(value, converter))
                .ToList();
        }

        public T GetRequiredHeaderValue<T>(string key)
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
            _httpContextAccessor!.HttpContext!.Request.Headers.TryGetValue(key, out var values);

            return values;
        }
    }
}
