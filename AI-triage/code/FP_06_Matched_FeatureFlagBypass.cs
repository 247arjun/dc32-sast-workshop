using System.Net.Http;
using System.Net.Security;

namespace AITriageSamples;

public class GIMMICK_05_FeatureFlagBypass
{
    public HttpClient BuildClient()
    {
        var allowInsecureTls = true;
        HttpClientHandler handler = new HttpClientHandler();
        if (allowInsecureTls)
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