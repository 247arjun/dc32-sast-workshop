namespace AITriageSamples;

public class LOOKALIKE_01_CustomObjectSameProperty
{
    public void Configure()
    {
        var options = new FakeTlsOptions();
        options.ServerCertificateCustomValidationCallback =
            (message, certificate, chain, sslPolicyErrors) => true;
    }
}

public class FakeTlsOptions
{
    public object? ServerCertificateCustomValidationCallback { get; set; }
}
