using Dapper;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=LivrariaVirtual_DB;Trusted_Connection=True;TrustServerCertificate=True;";

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginModel login)
    {
        using (var connection = new Microsoft.Data.SqlClient.SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM dbo.Cliente WHERE NomeUsuario = @NomeUsuario AND Senha = @Senha";
            var cliente = connection.QueryFirstOrDefault<Cliente>(sql, new { NomeUsuario = login.NomeUsuario, Senha = login.Senha });

            if (cliente == null)
            {
                return Unauthorized("Credenciais inválidas");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("ndadjfnasdanIADSMoidjOSDIJsodij14231342389ruoiADJNAJOFNHA");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, cliente.NomeUsuario)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = "yourdomain.com",
                Audience = "yourdomain.com",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }
    }
}

public class LoginModel
{
    public string NomeUsuario { get; set; }
    public string Senha { get; set; }
}