using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ToDoApp.Core.Data.Entities;
using ToDoApp.Core.DTOs.Todo;
using ToDoApp.Core.Models;
using ToDoApp.Core.Models.Options;
using ToDoApp.Core.Services;
using ToDoApp.Core.Storages;
using ToDoApp.Core.Storages.Validators;
using ToDoApp.Core.Utils;

namespace ToDoApp.Core.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration config)
        {
            ConfigureJWT(services, config);

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<ITodosService, TodosService>();

            services.AddScoped<IUserStorage, UserStorage>();
            services.AddScoped<ITodoStorage, TodoStorage>();

            services.AddScoped<IAuthUtils, AuthUtils>();

            services.Configure<JwtOptions>(config.GetSection(JwtOptions.JwtSettings));

            services.AddValidators();

            return services;
        }

        private static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<ModificationModel<TodoEntity>>, EditTodoValidator>();
            return services;
        }

        private static void ConfigureJWT(IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["ValidIssuer"],
                    ValidAudience = jwtSettings["ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
                };
            });
        }
    }
}
