using System.Net.Http;

namespace AITriageSamples;

public class LOOKALIKE_03_NullAssignment
{
    public HttpClient BuildClient()
    {
        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = null;
        return new HttpClient(handler);
    }
}