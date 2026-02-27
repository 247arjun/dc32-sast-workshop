using System.Net.Http;

namespace AITriageSamples;

public class TP_03_InlineLambdaTrueWithThis
{
    public HttpClient BuildClient()
    {
        HttpClientHandler handler = new HttpClientHandler();
        this.Configure(handler);
        return new HttpClient(handler);
    }

    private void Configure(HttpClientHandler handler)
    {
        handler.ServerCertificateCustomValidationCallback =
            (request, cert, chain, errors) => true;
    }
}
