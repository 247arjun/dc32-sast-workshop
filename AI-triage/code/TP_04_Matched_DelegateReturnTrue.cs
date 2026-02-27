using System.Net.Http;

namespace AITriageSamples;

public class VULN_MISS_02_DelegateReturnTrue
{
    public HttpClient BuildClient()
    {
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback =
            (message, certificate, chain, errors) => true;
        return new HttpClient(handler);
    }
}
