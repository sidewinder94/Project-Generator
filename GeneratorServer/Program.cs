using GeneratorService;
using System;
using System.ServiceModel;

namespace GeneratorServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(WorkService));
            ServiceHost host2 = new ServiceHost(typeof(SubService));
            host.Open();
            host2.Open();
            Console.WriteLine("server ready");
            Console.ReadLine();
        }
    }
}
