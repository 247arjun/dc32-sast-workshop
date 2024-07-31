using System;

namespace CSharpChallenges
{
    public class AuthenticationBypassByTamperingToken
    {
        public static ClaimsPrincipal ValidateTokenWithDisabledSignatureValidation(string token)
        {
            // Parse the token as a JWT
            var jwt = new JwtSecurityToken(token);

            // Validate the token signature, issuer, audience, and expiration
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false, // multi-tenant               
                ValidateAudience = true,
                ValidAudience = Audience,
                RequireSignedTokens = false

            };
            var handler = new JwtSecurityTokenHandler();
            var claimsPrincipal = handler.ValidateToken(token, validationParameters, out var validatedToken);
            validationParameters.IssuerSigningKeys = GetSigningKeys().Result;
            return claimsPrincipal;
        }


        public static ClaimsPrincipal ValidateTokenWithIncorrectSignatureValidation(string token)
        {
            // Parse the token as a JWT
            var jwt = new JwtSecurityToken(token);

            // Validate the token signature, issuer, audience, and expiration
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false, // multi-tenant               
                ValidateAudience = true,
                ValidAudience = Audience,
                SignatureValidator = SignatureValidator
            };
            var handler = new JwtSecurityTokenHandler();
            var claimsPrincipal = handler.ValidateToken(token, validationParameters, out var validatedToken);
            return claimsPrincipal;
        }        

        public static SecurityToken SignatureValidator(string token, TokenValidationParameters validationParameters)
        {
            // TODO : see if we can implement this logic
            var jwt = new JwtSecurityToken(token);
            return jwt;
        }

        public static ClaimsPrincipal ValidateTokenIncorrectSignatureValidation(string token)
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false, // multi-tenant               
                ValidateAudience = true,
                ValidAudience = Audience,
                SignatureValidator = (token, parameters) =>
                {
                    var jwt = new JwtSecurityToken(token);
                    // TODO: Validate the token
                    return jwt;
                }
            };
            var handler = new JwtSecurityTokenHandler();
            var claimsPrincipal = handler.ValidateToken(token, validationParameters, out var validatedToken);
            return claimsPrincipal;
        }
    }
}
