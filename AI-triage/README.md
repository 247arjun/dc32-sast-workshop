# AI Triage Practice

## Summary
Hotspot rules capture ranges of code to find points of interest,
see https://parsiya.net/blog/semgrep-hotspot/. In this challenge we use
AI to triage the results of such a rule.

## Pre-requisites

1. VS Code with SARIF Viewer extension.
    1. https://marketplace.visualstudio.com/items?itemName=MS-SarifVSCode.sarif-viewer
2. GitHub Copilot Chat. Free tier is enough.

## Commands

1. Use Semgrep to output a SARIF file.
    1. `semgrep -c rule.yaml --sarif -o output.sarif csharp-challenges\AI-triage`
2. Open the SARIF file in VS Code from the same directory.
3. The SARIF Viewer extension will display the results.
4. Highlight a finding and chat with AI about it in GitHub Copilot Chat.

## Step 1: Create a Rule
We're looking for a rule that detects when certificate validation callback is disabled. In C# this can be done in many ways, and this is a good pattern.

```csharp
using System.Net.Http;

namespace Demo;

public class CertificateExample
{
  public HttpClient CreateClient()
  {
    var handler = new HttpClientHandler();
    handler.ServerCertificateCustomValidationCallback =
      (message, certificate, chain, sslPolicyErrors) => true;
    return new HttpClient(handler);
  }
}
```

```yaml
rules:
- id: csharp-cert-validation-disabled
  languages:
    - csharp
  message: Certificate validation callback disabled.
  severity: WARNING
  pattern: |
    $HANDLER.ServerCertificateCustomValidationCallback =
        (...) => true;
```

## Step 2: Test in Playground
Test the rule in the Semgrep playground against the code above.

https://semgrep.dev/playground/s/9Rn2z

## Corpus for Triage
The `code` folder now contains a mix of classes for this exercise. Some files are true positives for this rule, some are vulnerable but missed by this rule, and some are safe or look similar but should not be flagged by a precise rule. Use SARIF output plus AI triage notes to decide which findings are real issues and which are noise.

## Step 3: Run Semgrep
Once you are happy with the rule, paste the rule in `cert-bypass.yaml` and run:

```
semgrep -c cert-bypass.yaml --sarif -o cert-bypass.sarif code\
```

## Step 4: Triage
Open the SARIF file in VS Code and click the first result. It should show the
captured code. Highlight the code and chat with AI in GitHub Copilot Chat to
triage.