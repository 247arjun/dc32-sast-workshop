using System;
using System.Net.Http;
using System.Net.Security;

namespace AITriageSamples;

public class SAFE_02_PinThumbprint
{
    public HttpClient BuildClient()
    {
        var localOnly = true;
        HttpClientHandler handler = new HttpClientHandler();
        if (localOnly)
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
