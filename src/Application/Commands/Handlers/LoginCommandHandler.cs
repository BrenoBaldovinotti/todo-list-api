using Application.Utilities;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;


namespace Application.Commands.Handlers
{
    public class LoginCommandHandler(IUserRepository userRepository, IConfiguration configuration) : IRequestHandler<LoginCommand, string>
    {
        private readonly string? _jwtSecretKey = configuration["Jwt:Key"];
        private readonly string? _jwtIssuer = configuration["Jwt:Issuer"];
        private readonly string? _jwtAudience = configuration["Jwt:Audience"];

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserByUsernameAsync(request.Username);
            if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
            {
                throw new Exception("Invalid username or password");
            }

            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            if (string.IsNullOrEmpty(_jwtSecretKey) || string.IsNullOrEmpty(_jwtIssuer) || string.IsNullOrEmpty(_jwtAudience))
            {
                throw new ArgumentNullException("JWT Settings", "JWT is not configured.");
            }

            var key = Encoding.ASCII.GetBytes(_jwtSecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                ]),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _jwtIssuer,
                Audience = _jwtAudience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private static bool VerifyPassword(string password, string storedHash)
        {
            var hash = PasswordHasher.HashPassword(password);
            return hash == storedHash;
        }
    }
}
