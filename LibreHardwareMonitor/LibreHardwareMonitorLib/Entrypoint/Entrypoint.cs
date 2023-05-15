using System;
using LibreHardwareMonitor.Hardware;

namespace LibreHardwareMonitor.Entrypoint
{
    public class NodeLibreHardwareMonitorLib {
        private Computer _computer;
       
        public NodeLibreHardwareMonitorLib() {
             _computer = new Computer
            {
                IsCpuEnabled = true,
                IsGpuEnabled = true,
                IsMemoryEnabled = true,
                IsMotherboardEnabled = true,
                IsControllerEnabled = true,
                IsNetworkEnabled = true,
                IsStorageEnabled = true
            };
        }

        public void GetComputerMessage() {
            _computer.Open();
            _computer.Accept(new UpdateVisitor());

            foreach (IHardware hardware in _computer.Hardware)
            {
                Console.WriteLine("Hardware: {0}", hardware.Name);

                foreach (IHardware subhardware in hardware.SubHardware)
                {
                    Console.WriteLine("\tSubhardware: {0}", subhardware.Name);

                    foreach (ISensor sensor in subhardware.Sensors)
                    {
                        Console.WriteLine("\t\tSensor: {0}, value: {1}", sensor.Name, sensor.Value);
                    }
                }

                foreach (ISensor sensor in hardware.Sensors)
                {
                    Console.WriteLine("\tSensor: {0}, value: {1}", sensor.Name, sensor.Value);
                }
            }
        }
    }
}
