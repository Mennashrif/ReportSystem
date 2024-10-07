using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ReportSystem.Application.Features.Common.DTO;
using ReportSystem.Domain.Entities.UserEntity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ReportSystem.Application.Features.Account.Queries.Login
{
    public class LoginQueryHundler : IRequestHandler<LoginQuery, ResponseDTO>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        public LoginQueryHundler(UserManager<User> userManager, SignInManager<User> signInManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;

        }
        public async Task<ResponseDTO> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user == null)
            {
                return new ResponseDTO
                {
                    ErrorMessage = "Invalid login attempt.",
                    StatusCode = 200
                };
            }

            var result = await _signInManager.PasswordSignInAsync(user,request.Password, isPersistent: false, lockoutOnFailure: false);

            if(result.Succeeded)
            {
                string token = GenerateJSONWebToken(user.Id);
                var roles = await _userManager.GetRolesAsync(user);
                return new ResponseDTO
                {
                    Data = new LoginResponse(token,roles.FirstOrDefault()),
                    StatusCode = 200
                };
            }

            return new ResponseDTO
            {
                ErrorMessage = "Invalid login attempt.",
                StatusCode = 200
            };
        }

        private string GenerateJSONWebToken(long userLoginId)
        {
            var signingKey = Convert.FromBase64String(_configuration["Jwt:Key"]);
            var audience = _configuration["Jwt:Audience"];
            var expiryDuration = int.Parse(_configuration["Jwt:ExpiryDuration"]);
            var issuer = _configuration["Jwt:Issuer"];

            var claims = new ClaimsIdentity(new List<Claim>() {
                    new Claim("userLoginId", userLoginId.ToString()),
                     });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,              // Not required as no third-party is involved
                Audience = audience,            // Not required as no third-party is involved
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(expiryDuration),
                Subject = claims,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var token = jwtTokenHandler.WriteToken(jwtToken);
            return token;
        }
    }
}
