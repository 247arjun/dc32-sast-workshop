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
**Description:** Find code pattern to match `ValidateState` method inside `OAuth2CodeRedeemerMiddleware` class. The code should also match the BinaryFormatter deserialization in `ValidateState` method.

**Hint:** Use `pattern-inside` with `OAuth2CodeRedeemerMiddleware` and `ValidateState` method and pattern to match BinaryFormatter.

### Challenge 4
**Description:** Find the insecure deserialization using `BinaryFormatter` in `ValidateState` method using *Taint* mode.

**Hint:** Use the `pattern-source` and `pattern-sink` in *taint* mode to track `context.Request.Query["state"]`.

### Challenge 5

**Description:** Match the code pattern using *taint* mode to find token signing keys fetched from untrusted input coming from JWT token.

**Hint:** Use the `pattern-source` and `pattern-sink` in *taint* mode to track `var jwt = new JwtSecurityToken(token);`.

### Challenge 6

**Description:** Find all insecure token validation code patterns `RequireSignedTokens = false` & ` var jwt = new JwtSecurityToken(token);
                    // TODO: Validate the token
                    return jwt;`

**Hint:** Use `pattern-either` to find multiple patterns


# Csharp Challenges Solution - TBD
> This is a list of solutions to the 6 challenges that I have created for you to test your Semgrep skills. The challenges are based on the Django python repo.

## Easy Challenges

### Challenge 1
**Description:** How many Python files import the `subprocess` module in the Django codebase.

```yaml
patterns:
    - pattern-either:
        - pattern: |
            import subprocess
        - pattern: |
            from subprocess import *
```

### Challenge 2
**Description:**  Find all instances where a function is defined but not used anywhere in the Django codebase.

```yaml
patterns:
    - pattern: |
        def $FUNC(...):
        ...
    - pattern-not: |
        $FUNC(...)
```

### Challenge 3
**Description:** Find all instances where a variable with "password" in the name is assigned a value

```yaml
patterns:
    - pattern: |
        $VAR = ...
    - metavariable-regex:
        metavariable: "$VAR"
        regex: ".*password.*"
```

## Medium Challenges

### Challenge 4
**Description:** Find all instances of `pickle` usage in the Django codebase.

**Hint:** Use the `pickle` pattern.

```yaml
rules:
  - id: pickle-usage
    patterns:
      - pattern: |
          (pickle)
    message: "Found pickle usage"
    languages:
      - python
```

### Challenge 5

**Description:** Find all instances of `eval` usage in the Django codebase.

**Hint:** Use the `eval` pattern.



```yaml
rules:
  - id: eval-usage
    patterns:
      - pattern: |
          (eval)
    message: "Found eval usage"
    languages:
      - python
```

## Hard Challenges

### Challenge 6

**Description:** Find all instances of `exec` usage in the Django codebase.

**Hint:** Use the `exec` pattern.

```yaml
rules:
  - id: exec-usage
    patterns:
      - pattern: |
          (exec)
    message: "Found exec usage"
    languages:
      - python
```

### Challenge 7

**Description:** Find all instances of `subprocess` usage in the Django codebase.

**Hint:** Use the `subprocess` pattern.

```yaml
rules:
  - id: subprocess-usage
    patterns:
      - pattern: |
          (subprocess)
    message: "Found subprocess usage"
    languages:
      - python
```

