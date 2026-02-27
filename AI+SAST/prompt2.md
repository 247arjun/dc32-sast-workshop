Please create a prompt asking copilot agent to create a workflow consist of the following 7 steps:
    1. Review the following code pattern and report vulnerability. 
    ```
    
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
    ```
    2. Use the code pattern above as the seed template to generate more testcases (as many variations as you could to strike for high coverage) demonstrating the similar vulnerability pattern. Please generate at least 1 SAFE testcase as well as at least 1 vulnerable testcase for each variation.
    3. Generate semgrep rules so that it can match all vulnerable code patterns with zero false positive.
    4. Run semgrep scan to validate the generated rules to ensure 0 false positive (FP) and 0 false negative (FN).
    5. If needed, please iterate the process above to generate more testcases (both SAFE and Vulnerable ones) to further improve test coverages.
    6. If there are new testcases, please update semgrep rules when needed to ensure 0 FP and 0 FN
    7. Please iterate the workflow above until you are satisfied with the semgrep rules outcome.

    Note that when generating more testcases, 
    1. Please include the variations on the method names which are commonly used for a method performing authorization checking.
    2. Please include the variations on the variable names which are commonly used to store the result of performing authorization checking.
    3. please include the variations on the code patterns which demonstrates the common  characteristics for a method performing authorization checking.
    4. Please include the variations on the control flow. For example, 
        i. Consider the variation of if-else structure such as single-if and if-else structure, with or without braces
        ii. Consider the variation of "falsy" condition location, for example, the "falsy" condition could be in the if branch, or in the else branch.
        iii. Consider the variation of implicit/explicit "return false" statement, for example, it is vulnerable code pattern to return true for a "fasly" condition (this the explicit "return true"), and it is also a vulnerable code patter to miss the "return false" statement for a "falsy" condition (this is the implicit case because the fall-through will happen and the default return is "return true" statement)
        iv. Also consider the variation of falsy condition as well the truthy condition. For example,
        The following are variations for the "Truthy" condition (6 variations)
            if (retVal )
            if (retVal ==true)
            if (true==retVal )
            if (retVal !=false)
            if (false!=retVal )
            if (retVal is true)
        The following are variations for the "Falsy" condition (6 variations)
            if (!retVal )
            if (retVal !=true)
            if (retVal !=VAR)
            if (retVal ==false)
            if (false==retVal )
            if (retVal is false)
        
    5. Please include the variations that syntactically different but semantically similar
    6. please try to achieve as many coverage as possible for testcase variations if achieving 100% coverage is not possible.

    For those generated new testcases, please make sure
    1. All testcases will be stored in testcases.cs file.
    2. All testcases having "return true" as the last statement with comment labeling if SAFE or VULNERABLE according to the code  logic
    3. Please use meaningful testcase name so that it is understandable by reading the testcase for the variation covered by each testcase
    4. Please group testcases into logical sections and provide a summary of variation for each section at the beginning of each section.