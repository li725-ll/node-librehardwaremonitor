using System;
using LibreHardwareMonitor.Entrypoint;


namespace LibreHardwareMonitorTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NodeLibreHardwareMonitorLib nodeLibreHardwareMonitorLib = new NodeLibreHardwareMonitorLib();
            string result = nodeLibreHardwareMonitorLib.GetComputerMessage();
            Console.WriteLine(result);
        }
    }
}
