using System;

namespace CSharpChallenges
{
    public class AuthenticationBypassByControllingSTS
    {
        public static ClaimsPrincipal ValidateToken(string token)
        {
            // Parse the token as a JWT
            var jwt = new JwtSecurityToken(token);
            // Validate the token signature, issuer, audience, and expiration
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidIssuer = jwt.Issuer,
                ValidateAudience = true,
                ValidAudience = Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(5)
            };
            validationParameters.IssuerSigningKeys = GetSigningKeys(jwt.Issuer).Result;
            var handler = new JwtSecurityTokenHandler();
            var claimsPrincipal = handler.ValidateToken(token, validationParameters, out var validatedToken);
            return claimsPrincipal;
        }


        private static async Task<IEnumerable<SecurityKey>> GetSigningKeys(string issuer)
        {
            try
            {
                // Retrieve the signing keys from the token issuer's metadata endpoint
                var configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                    issuer + "/.well-known/openid-configuration",
                    new OpenIdConnectConfigurationRetriever());
                var configuration = await configurationManager.GetConfigurationAsync();
                return configuration.SigningKeys;
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return null;
            }
        }

    }
}

