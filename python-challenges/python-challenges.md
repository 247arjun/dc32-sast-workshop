# Python Challenges
> This is a list of challenges that have been created for you to test your Semgrep skills. The challenges are based on code inside the `python-code-for-challenges` folder, and are designed to help you learn how to write Semgrep rules.

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
**Description:** Identify the `process_request` method in the Django codebase that handles user authentication 

**Hint:** Search for a variable `username` inside this method.

## Hard Challenges

### Challenge 5

**Description:** Find the function that validates that passwords are not entirely numeric.

**Hint:** Look for built-in Python functions that can be used to check if a string is numeric.
