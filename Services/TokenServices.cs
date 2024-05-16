using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Xml.Schema;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using E_Commerce_Api.Interfaces;


public class TokenServices: ITokenServices {

    private readonly IConfiguration _configuration;

    public TokenServices(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(string userEmail) {
        var tokenConfig = _configuration.GetSection("JWT");
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Convert.FromBase64String(tokenConfig["SecretKey"]);
        var tokenDescripto = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[] {new Claim(ClaimTypes.Name,userEmail)}),
            Expires = DateTime.UtcNow.AddDays(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescripto);
        return tokenHandler.WriteToken(token);
    }


    public bool IsTokenExpired(string token) {
        var tokenConfig = _configuration.GetSection("JWT");
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Convert.FromBase64String(tokenConfig["SecretKey"]);
        tokenHandler.ValidateToken(token,new TokenValidationParameters {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        },out SecurityToken validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;
        var expiryDateUnix = long.Parse(jwtToken.Payload.Exp.ToString());
        var expiryyDateTime = DateTimeOffset.FromUnixTimeSeconds(expiryDateUnix).UtcDateTime;
        return expiryyDateTime < DateTime.UtcNow;
    }


}
