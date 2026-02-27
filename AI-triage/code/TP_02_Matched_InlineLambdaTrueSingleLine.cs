using System.Net.Http;

namespace AITriageSamples;

public class TP_02_InlineLambdaTrueSingleLine
{
    public HttpClient BuildClient()
    {
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = (a, b, c, d) => true;
        return new HttpClient(handler);
    }
}
