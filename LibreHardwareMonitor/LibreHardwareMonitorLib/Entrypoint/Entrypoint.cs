using System;
using System.Collections.Generic;
using System.Collections;
using LibreHardwareMonitor.Hardware;
using Newtonsoft.Json;

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
            _computer.Open();
            _computer.Accept(new UpdateVisitor());
        }

        private String GetSensorType(SensorType sensorType) {
            switch (sensorType)
            {
                case SensorType.Clock: return "CLOCK";
                case SensorType.Control: return "CONTROL";
                case SensorType.Current: return "CURRENT";
                case SensorType.Data: return "DATA";
                case SensorType.Energy: return "ENERGY";
                case SensorType.Factor: return "FACTOR";
                case SensorType.Fan: return "FAN";
                case SensorType.Flow: return "FLOW";
                case SensorType.Frequency: return "FREQUENCY";
                case SensorType.Level: return "LEVEL";
                case SensorType.Load: return "LOAD";
                case SensorType.Noise: return "NOISE";
                case SensorType.Power: return "POWER";
                case SensorType.SmallData: return "SMALLDATA";
                case SensorType.Temperature: return "TEMPERATURE";
                case SensorType.Throughput: return "THROUGHPUT";
                case SensorType.TimeSpan: return "TIMESPAN";
                default: return "VOLTAGE";
            }
        }

        public String GetComputerMessage() {
            Dictionary<String, ArrayList> HardwardMap = new Dictionary<string, ArrayList>();
            foreach (IHardware hardware in _computer.Hardware)
            {
                ArrayList HardwareList = new ArrayList();
                HardwardMap.Add(hardware.Name, HardwareList); //最外层
                foreach (IHardware subhardware in hardware.SubHardware)
                {
                    ArrayList SubHardwareList = new ArrayList();
                    Dictionary<String, ArrayList> SubHardwareMap = new Dictionary<String, ArrayList>();
                    SubHardwareMap.Add(subhardware.Name, SubHardwareList);
                    foreach (ISensor sensor in subhardware.Sensors)
                    {
                        Dictionary<String, Object> SensorMap = new Dictionary<String, Object>();
                        SensorMap.Add("Name", sensor.Name);
                        SensorMap.Add("Type", GetSensorType(sensor.SensorType));
                        SensorMap.Add("Value", sensor.Value);
                        SensorMap.Add("Min", sensor.Min);
                        SensorMap.Add("Max", sensor.Max);
                        SubHardwareList.Add(SensorMap);
                    }
                }

                foreach (ISensor sensor in hardware.Sensors)
                {
                    Dictionary<String, Object> SensorMap = new Dictionary<String, Object>();
                    SensorMap.Add("Name", sensor.Name);
                    SensorMap.Add("Type", GetSensorType(sensor.SensorType));
                    SensorMap.Add("Value", sensor.Value);
                    SensorMap.Add("Min", sensor.Min);
                    SensorMap.Add("Max", sensor.Max);
                    HardwareList.Add(SensorMap);
                }
            }

            return JsonConvert.SerializeObject(HardwardMap);
        }
    }
}
