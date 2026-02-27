using System.Net.Http;
using System.Net.Security;

namespace AITriageSamples;

public class VULN_MISS_01_DangerousAcceptAny
{
    public HttpClient BuildClient()
    {
        var debugMode = true;
        HttpClientHandler handler = new HttpClientHandler();
        if (debugMode)
        {
            handler.ServerCertificateCustomValidationCallback =
                (message, certificate, chain, errors) => true;
        }
        else
        {
            handler.ServerCertificateCustomValidationCallback =
                (message, certificate, chain, errors) => errors == SslPolicyErrors.None;
        }
        return new HttpClient(handler);
    }
}
