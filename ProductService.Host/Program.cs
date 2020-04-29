using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            string ServiceName = "ProductService";
            string hostURI = "http://127.0.0.1:8080/ProductService";

            ServiceHost svcHost = null;
            try
            {
                Uri baseAddress = new Uri(hostURI);
                svcHost = new ServiceHost(typeof(Lib.ProductService), baseAddress);
                ServiceMetadataBehavior servBehavior = new ServiceMetadataBehavior();
                ServiceDebugBehavior servDebugBehavior = new ServiceDebugBehavior();
                servBehavior.HttpGetEnabled = true;
                servDebugBehavior.IncludeExceptionDetailInFaults = true;

                svcHost.Description.Behaviors.Add(servBehavior);
                svcHost.Description.Behaviors.Find<ServiceDebugBehavior>().IncludeExceptionDetailInFaults = true;

                svcHost.Open();

                Console.WriteLine($"\n\n{ServiceName} is Running  at following address");
                Console.WriteLine(baseAddress);
            }
            catch (Exception ex)
            {
                svcHost = null;
                Console.WriteLine($"Error has occured in starting - {ServiceName} : [ {ex.Message} ] ");
            }

            if (svcHost != null)
            {
                Console.WriteLine("\nPress any key to close the Service");
                Console.ReadKey();
                svcHost.Close();
                svcHost = null;
            }

            Console.ReadKey();
        }
    }
}
