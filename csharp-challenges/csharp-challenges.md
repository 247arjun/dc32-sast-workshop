# Csharp Challenges
> This is a list of 6 challenges that have been created for you to test your Semgrep skills. The challenges are based on code inside the `csharp-code-for-challenges` folder, and are designed to help you learn how to write Semgrep rules.

## Challenges

### Challenge 1
**Description:** Find all instances of `ActionResult` methods missing `[Authorize('Admin')]` attribute.

**Hint:** Use `pattern-not` with `[Authorize('Admin')]` attribute.

### Challenge 2
**Description:**  Find all instances of Azure Functions with HTTP Trigger and have anonymous authentication.

**Hint:** Pattern match the `AuthorizationLevel.Anonymous` usage in a function app.

### Challenge 3
**Description:** Find code pattern to match `ValidateState` method inside `OAuth2RequestManager` class. The code should also match the BinaryFormatter deserialization in `ValidateState` method.

**Hint:** Use `pattern-inside` with `OAuth2RequestManager` and `ValidateState` method and pattern to match BinaryFormatter.

### Challenge 4
**Description:** Find the code pattern which tracks user input `context.Request.Query["state"]` and passes tainted value to `ValidateState` vulnerable method.

**Hint:** Use the `pattern-source` and `pattern-sink` in *taint* mode to track taint from `context.Request.Query["state"]` to `ValidateState` method call.

### Challenge 5

**Description:** Match the code pattern to find token validation and signing keys fetched from untrusted input coming from JWT token.

**Hint:** Use the `pattern-either` to match `ValidateToken` and `GetSigningKeys` methods.

### Challenge 6

**Description:** Find all insecure token validation code patterns such as `RequireSignedTokens = false`, `SignatureValidator` delegate returning JWT tokens

**Hint:** Use `pattern-either` to find multiple insecure token validation code patterns


