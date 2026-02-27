using System.Net.Http;
using System.Net.Security;

namespace AITriageSamples;

public class GIMMICK_04_LocalhostOrContainerHost
{
    public HttpClient BuildClient()
    {
        var localTarget = true;
        HttpClientHandler handler = new HttpClientHandler();
        if (localTarget)
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
