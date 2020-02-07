using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using FirstCatering.Lib.Security.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FirstCatering.Lib.AspNetCore.Extensions
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extension helpers
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add and configure Jwt Bearer authentication to the service collection
        /// </summary>
        public static AuthenticationBuilder AddAuthenticationJwtBearer(this IServiceCollection services)
        {
            var jsonTokenSettings = services.BuildServiceProvider().GetRequiredService<IJsonWebTokenSettings>();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jsonTokenSettings.Key));

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            void AuthenticationScheme(AuthenticationOptions options)
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }

            void JwtBearer(JwtBearerOptions options)
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    SaveSigninToken = true,
                    IssuerSigningKey = securityKey,
                    ValidAudience = jsonTokenSettings.Audience,
                    ValidIssuer = jsonTokenSettings.Issuer,
                    ValidateAudience = !string.IsNullOrEmpty(jsonTokenSettings.Audience),
                    ValidateIssuer = !string.IsNullOrEmpty(jsonTokenSettings.Issuer),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            }

            return services.AddAuthentication(AuthenticationScheme).AddJwtBearer(JwtBearer);
        }

        /// <summary>
        /// Add and configure data protection to the service collection
        /// </summary>
        public static IDataProtectionBuilder AddDataProtectionDefault(this IServiceCollection services)
        {
            return services.AddDataProtection()
                .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration
                {
                    EncryptionAlgorithm = EncryptionAlgorithm.AES_256_GCM,
                    ValidationAlgorithm = ValidationAlgorithm.HMACSHA512
                })
                .SetDefaultKeyLifetime(TimeSpan.FromDays(7));
        }

        /// <summary>
        /// Add and configure Mvc and Json to the service collection
        /// </summary>
        public static IMvcCoreBuilder AddMvcJson(this IServiceCollection services)
        {
            static void Mvc(MvcOptions options)
                => options.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()));
            static void Json(JsonOptions options)
                => options.JsonSerializerOptions.IgnoreNullValues = true;

            return services.AddMvcCore(Mvc).AddJsonOptions(Json);
        }
    }
}