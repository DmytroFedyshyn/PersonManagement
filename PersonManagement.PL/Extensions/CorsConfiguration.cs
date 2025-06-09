using PersonManagement.BLL.Constants;

namespace PersonManagement.PL.Extensions
{
    public static class CorsConfiguration
    {
        public static WebApplicationBuilder AddProjectCors(this WebApplicationBuilder builder)
        {
            var allowedOrigins = builder.Configuration
                .GetSection("CorsSettings:AllowedOrigins")
                .Get<string[]>() ?? Array.Empty<string>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(ApplicationConstants.CorsPolicySectionName, policy =>
                {
                    if (allowedOrigins.Length > 0)
                    {
                        policy.WithOrigins(allowedOrigins)
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                    }
                    else
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                    }
                });
            });

            return builder;
        }
    }
}
