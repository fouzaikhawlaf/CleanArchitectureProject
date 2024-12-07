using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.FrameworkAndDrivers.Middlewares
{
   public class ValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ValidationMiddleware> _logger;

        public ValidationMiddleware(RequestDelegate next, ILogger<ValidationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Put)
            {
                context.Request.EnableBuffering();
                context.Request.Body.Position = 0;

                using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
                var body = await reader.ReadToEndAsync();

                context.Request.Body.Position = 0;

                // Validate the body content
                var errors = ValidateBody(body);
                if (errors.Any())
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new { Errors = errors }));
                    return;
                }
            }

            await _next(context);
        }

        private List<string> ValidateBody(string body)
        {
            var errors = new List<string>();

            // Add your validation logic here
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            try
            {
                var obj = JsonSerializer.Deserialize<Dictionary<string, object>>(body, options);
                if (obj != null)
                {
                    foreach (var key in obj.Keys)
                    {
                        if (obj[key] == null)
                        {
                            errors.Add($"{key} cannot be null.");
                        }
                    }
                }
            }
            catch (JsonException ex)
            {
                errors.Add("Invalid JSON format.");
                _logger.LogError(ex, "JSON deserialization error in ValidationMiddleware.");
            }

            return errors;
        }
    }
}

