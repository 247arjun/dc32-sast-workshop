// Test file for Semgrep authorization bypass rules
// Run: semgrep --config semgrep-rules/authorization-bypass.yaml .

namespace TestCases
{
    using System.Linq;
    using System.Security.Claims;

    public class AuthorizationBypassTestCases
    {

        public bool CanExecute(ClaimsPrincipal principal, object resource, string action)
        {
            var identity = (ClaimsIdentity)principal.Identity;
            if (resource is null || string.IsNullOrWhiteSpace(action))
            {
                Logger.Error("Invalid authorization inputs");
                return false;
            }
            if (!resourceRoleMapping.TryGetValue(resource.ToString(), out var roles))
            {
                Logger.Error("No mapping for resource");
                return false;
            }
            bool ok = roles.Any(required => identity.HasClaim(ClaimTypes.Role, required));
            if (!ok)
            {
                Logger.Warn("AuthZ denied");
                //Missing "return false" statement here for the failed authorization check which will cause it fall through to the default "return true", allowing access granted to the unauthorized users.
            }
            return true; // VULNERABLE: should be `return ok;`
        }

        // Helper stubs
        private void DoSomething() { }
        private void DoOtherStuff() { }
        private static class Logger { public static void Warn(string s) { } public static void Error(string s) { } }
    }
}
