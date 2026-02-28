# Prompt: Security Vulnerability Review of Codebase

You are a senior application security engineer. Perform a thorough security review of the entire codebase and report all security vulnerabilities found.

---

## Objective

Systematically review all source code files in this repository and produce a comprehensive security vulnerability report. Focus on identifying real, exploitable vulnerabilities — not stylistic issues or theoretical concerns.

---

## Step 1: Codebase Discovery

1. Enumerate all source code files in the repository (all languages, all directories)
2. Identify the technology stack, frameworks, and languages used
3. Note any configuration files, deployment manifests, or infrastructure-as-code files
4. Map the application architecture (entry points, data flows, trust boundaries)

---

## Step 2: Vulnerability Analysis

Review the codebase for the following vulnerability categories. For each category, examine all relevant code paths.

### Authentication & Authorization (OWASP A01, A07)
- **Broken access control**: Missing or bypassable authorization checks
- **Authentication flaws**: Weak credential handling, missing MFA enforcement, session management issues
- **Privilege escalation**: Paths where lower-privilege users can access higher-privilege functionality
- **Authorization bypass**: Methods that fail to return `false` or deny access on failed checks (see `semgrep-rules/authorization-bypass.yaml` for known patterns)

### Injection (OWASP A03)
- **SQL injection**: Unsanitized user input in SQL queries, string concatenation in queries
- **Command injection**: User input passed to OS command execution (`Process.Start`, `exec`, `system`)
- **LDAP injection**: Unsanitized input in LDAP queries
- **XSS (Cross-Site Scripting)**: Unencoded user input rendered in HTML responses
- **Template injection**: User input in server-side template rendering

### Cryptographic Failures (OWASP A02)
- **Weak algorithms**: Use of MD5, SHA1, DES, RC4, or other deprecated algorithms
- **Hardcoded secrets**: API keys, passwords, tokens, connection strings in source code
- **Insecure random**: Use of `System.Random` or `Math.random()` for security-sensitive operations
- **Missing encryption**: Sensitive data stored or transmitted in plaintext

### Security Misconfiguration (OWASP A05)
- **Debug mode enabled**: Debug flags left on in production configurations
- **Verbose error messages**: Stack traces or internal details exposed to users
- **Default credentials**: Unchanged default passwords or accounts
- **Overly permissive CORS**: Wildcard or overly broad CORS policies
- **Missing security headers**: Missing CSP, HSTS, X-Frame-Options, etc.

### Insecure Design (OWASP A04)
- **Missing rate limiting**: Endpoints without throttling (brute force, DoS)
- **IDOR (Insecure Direct Object Reference)**: Direct use of user-supplied IDs without ownership validation
- **Mass assignment**: Binding user input directly to internal models without allowlisting
- **Race conditions**: TOCTOU (time-of-check-time-of-use) vulnerabilities

### Vulnerable Dependencies (OWASP A06)
- **Known CVEs**: Check package manifests (NuGet, npm, pip, etc.) for known vulnerable versions
- **Outdated dependencies**: Libraries significantly behind current versions

### Data Protection
- **Sensitive data exposure**: PII, credentials, or tokens logged or returned in responses
- **Insecure deserialization**: Deserialization of untrusted data (`BinaryFormatter`, `JSON.parse` with type info)
- **Path traversal**: User input used to construct file paths without sanitization
- **SSRF (Server-Side Request Forgery)**: User-controlled URLs in server-side HTTP requests

### Infrastructure & Deployment
- **Secrets in code**: Credentials, connection strings, or keys committed to source control
- **Insecure file permissions**: Overly permissive file or directory permissions
- **Container security**: Insecure Dockerfile practices (running as root, exposing unnecessary ports)

---

## Step 3: Produce the Vulnerability Report

For **each vulnerability found**, provide the following structured information:

### Vulnerability Entry Format

```
### [SEVERITY] Title
- **File**: path/to/file.cs
- **Line(s)**: 42-48
- **CWE**: CWE-XXX (Name)
- **OWASP**: A0X:2021 — Category Name
- **Severity**: CRITICAL / HIGH / MEDIUM / LOW / INFO
- **Confidence**: HIGH / MEDIUM / LOW

**Description**: 
Clear explanation of the vulnerability and why it is exploitable.

**Vulnerable Code**:
```language
// The vulnerable code snippet
```

**Impact**:
What an attacker can achieve by exploiting this vulnerability.

**Remediation**:
Specific fix recommendation with corrected code example where applicable.

**References**:
- Relevant CWE/OWASP links
```

### Severity Definitions
- **CRITICAL**: Directly exploitable, leads to full compromise (RCE, auth bypass, data breach)
- **HIGH**: Exploitable with moderate effort, significant impact (SQLi, privilege escalation)
- **MEDIUM**: Requires specific conditions to exploit, moderate impact (XSS, CSRF, IDOR)
- **LOW**: Minor security concern, limited impact (info disclosure, missing headers)
- **INFO**: Best practice recommendation, no direct exploit path

---

## Step 4: Summary and Metrics

At the end of the report, provide:

1. **Executive Summary**: 2-3 sentence overview of the security posture
2. **Finding Statistics**:
   - Total findings by severity (CRITICAL / HIGH / MEDIUM / LOW / INFO)
   - Total findings by OWASP category
   - Most affected files/modules
3. **Top Risks**: The 3-5 most critical issues requiring immediate attention
4. **Remediation Priority**: Ordered list of fixes from most to least urgent

---

## Guidelines

- **No false positives**: Only report issues you are confident are real vulnerabilities. Do not pad the report with speculative or theoretical issues.
- **Be specific**: Include exact file paths, line numbers, and code snippets for every finding.
- **Provide fixes**: Every finding must include a concrete remediation recommendation.
- **Consider context**: Assess whether a vulnerability is actually reachable/exploitable given the application architecture.
- **Check existing protections**: Note if a vulnerability is partially mitigated by other controls (e.g., WAF, framework defaults).
- **Run existing tools**: If semgrep rules exist in the repository (e.g., `semgrep-rules/`), run them as part of the analysis and include their findings.
