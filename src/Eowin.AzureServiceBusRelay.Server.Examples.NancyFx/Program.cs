using System;
using Eowin.AzureServiceBusRelay.Server.Tests;
using Nancy;
using Owin;
using System.ServiceModel;
using Microsoft.ServiceBus;
using System.ServiceModel.Web;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace Eowin.AzureServiceBusRelay.Server.Examples.NancyFx
{
    internal class Program
    {

        [ServiceContract(Name = "ImageContract", Namespace = "http://samples.microsoft.com/ServiceModel/Relay/")]
        public interface IImageContract
        {
            [OperationContract, WebGet]
            Stream GetImage();
        }

        public interface IImageChannel : IImageContract, IClientChannel { }

        [ServiceBehavior(Name = "ImageService", Namespace = "http://samples.microsoft.com/ServiceModel/Relay/")]
        class ImageService : IImageContract
        {
         

            public ImageService()
            {
            }

            public Stream GetImage()
            {

                return null;
            }
        }

        static void Main(string[] args)
        {
            string address = SecretCredentials.ServiceBusAddress;

            var sbConfig = new AzureServiceBusOwinServiceConfiguration(
                issuerName: SecretCredentials.PolicyName,
                issuerSecret: SecretCredentials.Secret,
                address: address);
            var server = AzureServiceBusOwinServer.Create(sbConfig, app =>
            {
                app.UseNancy();
            });
            Console.WriteLine("Server is listening at {0}", address);
            Console.ReadKey();
        }
    }

    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/nancy"] = x => "hello from Nancy in the cloud";
        }
    }
}
