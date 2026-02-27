using System.Net.Http;
using System.Net.Security;

namespace AITriageSamples;

public class SAFE_01_CheckSslPolicyErrors
{
    public HttpClient BuildClient()
    {
        var isDev = true;
        HttpClientHandler handler = new HttpClientHandler();
        if (isDev)
        {
            handler.ServerCertificateCustomValidationCallback =
                (message, certificate, chain, sslPolicyErrors) => true;
        }
        else
        {
            handler.ServerCertificateCustomValidationCallback =
                (message, certificate, chain, sslPolicyErrors) => sslPolicyErrors == SslPolicyErrors.None;
        }
        return new HttpClient(handler);
    }
}
