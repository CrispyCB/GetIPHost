using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;


namespace GetIPHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var macAddr =
     (
         from nic in NetworkInterface.GetAllNetworkInterfaces()
         where nic.OperationalStatus == OperationalStatus.Up
         select nic.GetPhysicalAddress().ToString()
     ).FirstOrDefault();
            FileStream IPHostStream;
            StreamWriter IPHostWriter;
            TextWriter oldoutput = Console.Out;

            try
            {
                IPHostStream = new FileStream("./IPHost.txt", FileMode.OpenOrCreate, FileAccess.Write);
                IPHostWriter = new StreamWriter(IPHostStream);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open IPHost.txt for writing.");
                Console.WriteLine(e.Message);
                return;
            }
            Console.SetOut(IPHostWriter);
            string HostName = "";
            HostName = Dns.GetHostName();
            Console.WriteLine("The local machine's host name is " + HostName + ".");
            IPAddress[] LocalMachineIPs = Dns.GetHostAddresses(Dns.GetHostName());
            foreach(IPAddress addr in LocalMachineIPs)
            {
                Console.WriteLine("The local machine's IP address is " + addr + ".");
            }
            Console.WriteLine("The local machine's MAC address is " + macAddr + ".");
            Console.SetOut(oldoutput);
            IPHostWriter.Close();
            IPHostStream.Close();
            Console.WriteLine("Done.");
            Console.WriteLine("Your text file has printed to the desktop.");
            Console.WriteLine("Please press any key to close the application.");
            Console.ReadKey();
        }
    }
}
