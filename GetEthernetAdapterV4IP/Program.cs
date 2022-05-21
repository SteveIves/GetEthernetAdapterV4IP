using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace GetEthernetAdapterV4IP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            { 
                Console.WriteLine("ERROR: Must specify one parameter containing a network adapter name!");
                Environment.ExitCode = 1;
                return;
            }

            var ipAddress = NetTools.GetIpsForNetworkAdapters(args[0]).FirstOrDefault();

            if (ipAddress == null)
            {
                Environment.ExitCode = 1;
            }

            Console.Write(ipAddress.ToString());
        }
    }

    public static class NetTools {
        public static IEnumerable<IPAddress> GetIpsForNetworkAdapters(string adapterName)
        {
            var nics = from i in NetworkInterface.GetAllNetworkInterfaces()
                       where i.OperationalStatus == OperationalStatus.Up
                       where i.Name == adapterName
                       select new { name = i.Name, ip = GetIpV4FromUnicastAddresses(i) };


            return nics.Select(x => x.ip);
        }

        public static IPAddress GetIpV4FromUnicastAddresses(NetworkInterface i)
        {
            return (from ip in i.GetIPProperties().UnicastAddresses
                    where ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork
                    select ip.Address).SingleOrDefault();
        }
    }
}
