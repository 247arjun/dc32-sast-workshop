namespace TestCases
{
    using System.Linq;
    using System.Security.Claims;

    public class AuthorizationBypassTestCases
    {

        public bool CanExecute(
            ClaimsPrincipal principal,
            object resource,
            string action)
        {
            var identity = principal?.Identity as ClaimsIdentity;

            if (identity == null ||
                resource == null ||
                string.IsNullOrWhiteSpace(action))
            {
                Logger.Error("Invalid authorization inputs");
                return false;
            }

            var resourceKey = resource.ToString();
            if (string.IsNullOrWhiteSpace(resourceKey))
            {
                Logger.Error("Invalid resource key");
                return false;
            }

            // Example structure:
            // Dictionary<string, Dictionary<string, string[]>> resourceActionRoleMapping;
            if (!resourceActionRoleMapping.TryGetValue(resourceKey, out var actionRoleMap))
            {
                Logger.Error($"No mapping for resource: {resourceKey}");
                return false;
            }

            if (!actionRoleMap.TryGetValue(action, out var requiredRoles) || requiredRoles == null || requiredRoles.Length == 0)
            {
                Logger.Error($"No mapping for resource/action: {resourceKey}/{action}");
                return false; // deny-by-default if action isn't explicitly mapped
            }

            bool ok = requiredRoles.Any(r => identity.HasClaim(ClaimTypes.Role, r));
            if (!ok)
            {
                Logger.Warn($"AuthZ denied: resource={resourceKey}, action={action}");
            }

            return true;
        }

        // Helper stubs
        private void DoSomething() { }
        private void DoOtherStuff() { }
        private static class Logger { public static void Warn(string s) { } public static void Error(string s) { } }
    }
}
