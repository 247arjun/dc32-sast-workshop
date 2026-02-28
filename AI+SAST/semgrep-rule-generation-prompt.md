# Prompt: Generate Semgrep Rules for Authorization Bypass Detection

You are a senior application security engineer specializing in static analysis rule authoring. Generate Semgrep rules (in YAML format) that detect the authorization bypass vulnerability pattern found in the following C# code.

---

## Vulnerable Code Pattern

In `test-cases.cs`, the `CanExecute` method performs a role-based authorization check but fails to deny access when the check fails:

```csharp
bool ok = requiredRoles.Any(r => identity.HasClaim(ClaimTypes.Role, r));
if (!ok)
{
    Logger.Warn($"AuthZ denied: resource={resourceKey}, action={action}");
    // BUG: Missing "return false;" here
}

return true; // Always grants access, even when ok == false
```

The core pattern is: **a boolean authorization/permission check is performed, the failure branch logs or handles the denial but does NOT return false or throw, and the method unconditionally returns true afterward.**

---

## Requirements

### Rule 1: Authorization check result ignored — method always returns true

Detect methods that:
1. Are `bool`-returning methods related to authorization (e.g., method names containing `CanExecute`, `CheckAccess`, `IsAuthorized`, `HasPermission`, `Authorize`, `CheckPermission`)
2. Contain a boolean variable assigned from an authorization/permission check (e.g., `HasClaim`, `IsInRole`, `Any(...)` on roles/claims/permissions)
3. Have an `if (!result)` branch that does NOT contain a `return false;` or `throw`
4. End with `return true;` unconditionally

### Rule 2: Boolean authorization result not used in return statement

Detect methods that:
1. Assign an authorization check result to a boolean variable (e.g., `bool ok = ...HasClaim(...)`, `bool allowed = ...IsInRole(...)`)
2. Never use that variable in any `return` statement
3. Instead return a hardcoded `return true;`

### Rule 3: Deny branch without return/throw (general pattern)

Detect `if` blocks that:
1. Check for a negative/failure condition (`if (!ok)`, `if (!authorized)`, `if (!isValid)`, `if (result == false)`)
2. Contain logging or error handling (calls to `Logger`, `Log`, `Console.Write`, `Trace`, etc.)
3. Do NOT contain a `return`, `throw`, or assignment that would prevent fall-through
4. Are followed by a `return true;` statement

---

## Output Format

Generate the rules as a single YAML file suitable for use with `semgrep --config <file>.yaml`. Each rule should include:

- `id`: Descriptive kebab-case identifier (e.g., `csharp-authorization-bypass-missing-deny`)
- `patterns` / `pattern`: Semgrep pattern(s) using metavariables
- `message`: Clear explanation of the vulnerability and why it matters
- `severity`: `ERROR` for the authorization bypass rules
- `languages`: `[csharp]`
- `metadata`: Include CWE (CWE-863), OWASP (A01:2021), and confidence level

### Example structure:

```yaml
rules:
  - id: csharp-authorization-bypass-missing-deny
    patterns:
      - pattern: |
          ...
    message: >-
      Authorization check result is not used to deny access.
      The method returns true even when the check fails,
      allowing unauthorized access.
    severity: ERROR
    languages: [csharp]
    metadata:
      cwe: "CWE-863: Incorrect Authorization"
      owasp: "A01:2021 - Broken Access Control"
      confidence: HIGH
```

---

## Validation

After generating the rules:
1. Save the rules to `semgrep-rules/authorization-bypass.yaml`
2. Run `semgrep --config semgrep-rules/authorization-bypass.yaml test-cases.cs` to validate that the rules detect the vulnerability in the test file
3. Verify the output shows findings on the correct lines (lines 47-53 of test-cases.cs)
4. If the rules don't match, iterate on the patterns until they produce correct results

---

## Guidelines

- **Minimize false positives**: The patterns should be specific enough to avoid flagging legitimate code (e.g., methods that intentionally return true after logging)
- **Use metavariables**: Use Semgrep metavariables (`$VAR`, `$FUNC`, `...`) to generalize patterns across different variable names and method calls
- **Use `pattern-not`**: Exclude safe patterns where the denial branch contains `return false` or `throw`
- **Test-driven**: The rules MUST detect the vulnerability in `test-cases.cs` — iterate until they do
- **Include test annotations**: Add `# ruleid: <rule-id>` comments to test-cases.cs for semgrep test validation if appropriate
