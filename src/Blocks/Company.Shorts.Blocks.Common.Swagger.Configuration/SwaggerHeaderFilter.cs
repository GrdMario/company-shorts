﻿namespace Company.Shorts.Blocks.Common.Swagger.Configuration
{
    using Microsoft.OpenApi.Any;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System.Reflection;

    public class SwaggerHeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            if (context.MethodInfo.GetCustomAttribute(typeof(SwaggerHeader)) is SwaggerHeader attribute)
            {
                OpenApiParameter? existingParam = operation.Parameters.FirstOrDefault(
                    p => p.In == ParameterLocation.Header && p.Name == attribute.Name);

                // Remove description from [FromHeader] argument attribute
                if (existingParam is not null)
                {
                    operation.Parameters.Remove(existingParam);
                }

                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = attribute.Name,
                    In = ParameterLocation.Header,
                    Description = attribute.Description,
                    Required = attribute.IsRequired,
                    Schema = new OpenApiSchema()
                    {
                        Type = "string",
                        Default = string.IsNullOrEmpty(attribute.DefaultValue)
                            ? null
                            : new OpenApiString(attribute.DefaultValue)
                    }
                });
            }
        }
    }
}