using Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Xamarin.Android.Net;

namespace e_Commerce_Grocery_App.Platforms.Android
{
    class AndroidHttpMessageHandler : IPlatformHttpMessageHandler
    {
        public HttpMessageHandler GetHttpMessageHandler() =>
            new AndroidClientHandler
            {
                ServerCertificateCustomValidationCallback = (httpRequestMessage, certificate, chain, sslPolicyErrors) =>
                    certificate?.Issuer == "CN=localhost" || sslPolicyErrors == SslPolicyErrors.None
            };

    }
}
