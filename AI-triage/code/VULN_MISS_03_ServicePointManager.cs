using System.Net;

namespace AITriageSamples;

public class VULN_MISS_03_ServicePointManager
{
    public void Configure()
    {
        ServicePointManager.ServerCertificateValidationCallback +=
            (sender, cert, chain, errors) => true;
    }
}
