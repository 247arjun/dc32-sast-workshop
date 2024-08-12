# Python Challenges Solution
> This is a list of solutions to the challenges that I have created for you to test your Semgrep skills. The challenges are based on the Django python repo, and only needs the code inside this GitHub repo (it wil also work with the official Django repo).

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
**Description:** Identify the `process_request` method in the Django codebase that handles user authentication 

**Hint:** Search for a variable `username` inside this method.

```yaml
patterns:
    - pattern-inside: |
        def process_request(...): ...
    - pattern: |
            username = ...
```

#### Alternate solution
> Note how the entire function is matched in the pattern.

```yaml
patterns:
    - pattern: |
        def process_request(...): 
            ...
            username = ...
            ...
```


## Hard Challenges

### Challenge 5

**Description:** Find the function that validates that passwords are not entirely numeric.

**Hint:** Look for built-in Python functions that can be used to check if a string is numeric.

```yaml
patterns:
    - pattern-inside: |
        def $FUNC(...): ...
    - pattern-either: 
        - pattern: |
            $VAR.isdigit()
        - pattern: |
            $VAR1 = $VAR2.isdigit()
```

#### Alternate solution
> This is simpler, but doesn't account for the possibility of the `isdigit()` function being called on a variable.
```yaml
patterns:
    - pattern-inside: |
        def $FUNC(...): ...
    - pattern: |
        $VAR.isdigit()
```