using System.Net.Http;

namespace AITriageSamples;

public class Feature01
{
    public HttpClient BuildClient()
    {
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback =
            (message, certificate, chain, sslPolicyErrors) => true;
        return new HttpClient(handler);
    }
}