using GeneratorService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(WorkService));
            ServiceHost host2 = new ServiceHost(typeof(CallBackService));
            host.Open();
            host2.Open();
            Console.WriteLine("server ready");
            Console.ReadLine();
        }
    }
}
