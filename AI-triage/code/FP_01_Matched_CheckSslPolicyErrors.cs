using System.Net.Http;
using System.Net.Security;

namespace AITriageSamples;

public class Feature01
{
    public HttpClient BuildClient()
    {
        var isDev = bool.TryParse(Environment.GetEnvironmentVariable("Dev"), out var parsedIsDev) && parsedIsDev;
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
