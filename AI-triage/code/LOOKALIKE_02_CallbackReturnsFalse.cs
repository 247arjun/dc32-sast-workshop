using System.Net.Http;

namespace AITriageSamples;

public class LOOKALIKE_02_CallbackReturnsFalse
{
    public HttpClient BuildClient()
    {
        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback =
            (message, certificate, chain, sslPolicyErrors) => false;
        return new HttpClient(handler);
    }
}
