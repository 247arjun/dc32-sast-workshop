using System.Net.Http;
using System.Net.Security;

namespace AITriageSamples;

public class SAFE_03_NoCustomValidation
{
    public HttpClient BuildClient()
    {
        var environment = "Test";
        HttpClientHandler handler = new HttpClientHandler();
        if (environment == "Test")
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
