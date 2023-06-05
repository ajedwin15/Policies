using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using src.InsurancePolicies.Domain.Domain.config;
using src.InsurancePolicies.Domain.Entities.Security;
using src.InsurancePolicies.Infrastructure.Data;

namespace src.InsurancePolicies.Application.Auth
{
    public class AuthApplication : IAuthApplication
    {
        private readonly IMongoDatabase _db;
        private readonly IConfiguration _configuration;

        public AuthApplication(IOptions<MongoSettings> options, IConfiguration configuration)
        {
            _configuration = configuration;
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.DatabaseName);
        }
        public IMongoCollection<User> userCredentialModels => _db.GetCollection<User>("Login");

        public async Task<User> PostLogin(CredentialEntity credentialEntity)
        {
            var user = await userCredentialModels.Find(d => d.Email == credentialEntity.Email).SingleOrDefaultAsync();
            if (user == null) return null;
            var validateCredential = Encrypt.verifyHashData(credentialEntity.Password, user.Password);
            if (!validateCredential) return null;
            return user;
        }

        public async Task<User> Create(User newUser)
        {
            newUser.Password = Encrypt.hashData(newUser.Password);
            await userCredentialModels.InsertOneAsync(newUser);
            return newUser;
        }

        public async Task<string> PostGenerate(CredentialEntity credentialEntity)
        {
            var validateUser = await PostLogin(credentialEntity);

            if (validateUser == null) return null;
            var issuer = _configuration["JWT_ISSUER"];
            var audience = _configuration["JWT_AUDIENCE"];
            var key = Encoding.ASCII.GetBytes(_configuration["JWT_KEY"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim("Id", Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, validateUser.Email),
            new Claim(ClaimTypes.Role, validateUser.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
                Expires = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["JWT_EXPIRES"])),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}