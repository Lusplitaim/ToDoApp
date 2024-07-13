using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoApp.Core.Data.Entities;
using ToDoApp.Core.DTOs.User;
using ToDoApp.Core.Exceptions;
using ToDoApp.Core.Models;
using ToDoApp.Core.Models.Options;
using ToDoApp.Core.Storages;

namespace ToDoApp.Core.Services
{
    internal class AuthService : IAuthService
    {
        private readonly IUserStorage m_UserStorage;
        private readonly UserManager<UserEntity> m_UserManager;
        private readonly SignInManager<UserEntity> m_SignInManager;
        private readonly JwtOptions m_JwtOptions;
        public AuthService(
            IUserStorage userStorage,
            UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager,
            IOptions<JwtOptions> opts)
        {
            m_UserStorage = userStorage;
            m_UserManager = userManager;
            m_SignInManager = signInManager;
            m_JwtOptions = opts.Value;
        }

        public async Task<ExecResult<AuthResult>> AuthenticateUserAsync(LoginUserDto model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            try
            {
                if (model is null) throw new ArgumentNullException(nameof(model));

                var result = new ExecResult<AuthResult>();

                var existingUser = await m_UserManager.FindByEmailAsync(model.Email);
                if (existingUser is null)
                {
                    result.AddError("Wrong email or password");
                    return result;
                }

                var signInResult = await m_SignInManager.CheckPasswordSignInAsync(existingUser, model.Password, false);
                if (!signInResult.Succeeded)
                {
                    result.AddError("Wrong email or password");
                    return result;
                }

                var token = await CreateTokenAsync(model.Email);
                var user = await m_UserStorage.GetAsync(model.Email);

                result.Result = new() { User = user!, Token = token };

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to login user", ex);
            }
        }

        private async Task<string> CreateTokenAsync(string userEmail)
        {
            try
            {
                var user = await m_UserManager.FindByEmailAsync(userEmail);

                if (user is not null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    };

                    var userRoles = await m_UserManager.GetRolesAsync(user);
                    foreach (var role in userRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var jwt = new JwtSecurityToken(
                        issuer: m_JwtOptions.ValidIssuer,
                        audience: m_JwtOptions.ValidAudience,
                        claims: claims,
                        expires: DateTime.UtcNow.Add(TimeSpan.FromDays(5)),
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(m_JwtOptions.SecretKey)),
                            SecurityAlgorithms.HmacSha256));

                    return new JwtSecurityTokenHandler().WriteToken(jwt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create a token", ex);
            }

            throw new NotFoundCoreException();
        }

        public async Task<ExecResult<AuthResult>> RegisterUserAsync(RegisterUserDto model)
        {
            if (model is null) { throw new ArgumentNullException("model"); }

            try
            {
                var result = new ExecResult<AuthResult>();

                var registerResult = await m_UserStorage.CreateAsync(model);
                result.AddErrors(registerResult);

                var token = await CreateTokenAsync(model.Email);
                var user = await m_UserStorage.GetAsync(model.Email);

                result.Result = new() { User = user!, Token = token };

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create user", ex);
            }
        }
    }
}
