using System;
using System.Threading.Tasks;
using LibreHardwareMonitor.Entrypoint;


namespace LibreHardwareMonitorTest
{
    internal class Program
    {
        static void  Main(string[] args)
        {
            GetHardwareMessageTest();
            SetFanSpeed();
            GetHardwareMessageLog();
        }

        private static void GetHardwareMessageTest() {
            Core core = new Core();
            string result = core.GetHardwareMessage();
            Console.WriteLine(result);
        }

        private static void SetFanSpeed() {
            Core core = new Core();
            bool result = core.SetFanSpeed("Pump Fan", 20);
            Console.WriteLine(result);
        }

        private static void GetHardwareMessageLog()
        {
            Core core = new Core();
            core.GetHardwareMessageLog();
        }
    }
}
