# Python Challenges
> This is a list of 7 challenges that have been created for you to test your Semgrep skills. The challenges are based on code inside the `python-code-for-challenges` folder, and are designed to help you learn how to write Semgrep rules.

## Easy Challenges

### Challenge 1
**Description:** How many Python *files* import the `subprocess` module in the Django codebase.

**Hint:** Don't forget about `from subprocess import *`

### Challenge 2
**Description:**  Find all instances where a function is defined but not used anywhere in the Django codebase.

**Hint:** Look for function definitions and check if they are called.

### Challenge 3
**Description:** Find all instances where a variable with "`password`" in the name is assigned a value. Example: `password_changed`

**Hint:** Use a Regex pattern to match the variable name.

## Medium Challenges

### Challenge 4
**Description:** Find all instances of `pickle` usage in the Django codebase.

**Hint:** Use the `pickle` pattern.

### Challenge 5

**Description:** Find all instances of `eval` usage in the Django codebase.

**Hint:** Use the `eval` pattern.

## Hard Challenges

### Challenge 6

**Description:** Find all instances of `exec` usage in the Django codebase.

**Hint:** Use the `exec` pattern.

### Challenge 7

**Description:** Find all instances of `subprocess` usage in the Django codebase.

**Hint:** Use the `subprocess` pattern.


# Python Challenges Solution
> This is a list of solutions to the 7 challenges that I have created for you to test your Semgrep skills. The challenges are based on the Django python repo.

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

