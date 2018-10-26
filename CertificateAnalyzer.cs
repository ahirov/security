using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace X509
{
    internal sealed class CertificateAnalyzer
    {
        public void Analyze()
        {
            Console.WriteLine("SSL Certificate analyzer...");
            Console.WriteLine("Enter url address (eg, https://resolver.technology):");

            var address = Console.ReadLine();
            if (string.IsNullOrEmpty(address))
                throw new InvalidOperationException("No input...");

            var uri = new Uri(address);
            var port = uri.Port;
            if (port != 443)
                throw new InvalidOperationException("Not https://");

            ServicePointManager.ServerCertificateValidationCallback = Analyze;
            var request = WebRequest.Create(uri);
            using (request.GetResponse()) { }
        }

        private static bool Analyze(object sender,
                                    X509Certificate certificate,
                                    X509Chain chain,
                                    SslPolicyErrors sslPolicyErrors)
        {
            Console.WriteLine(certificate.ToString(true));
            Console.WriteLine($"Errors: {sslPolicyErrors}");
            return true;
        }
    }
}