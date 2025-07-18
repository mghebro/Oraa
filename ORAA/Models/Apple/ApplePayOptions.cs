namespace ORAA.Models.Apple
{
    public class ApplePayOptions
    {
        public string? StoreName { get; set; }

        public bool UseCertificateStore { get; set; }

        public string? MerchantCertificate { get; set; }

        public string? MerchantCertificateFileName { get; set; }

        public string? MerchantCertificatePassword { get; set; }

        public string? MerchantCertificateThumbprint { get; set; }
    }
}
