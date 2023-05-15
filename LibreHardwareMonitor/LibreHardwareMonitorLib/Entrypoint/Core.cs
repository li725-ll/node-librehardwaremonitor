using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using LibreHardwareMonitor.Hardware;

namespace LibreHardwareMonitor.Entrypoint
{
    public class Core
    {
        private Computer _computer;

        public Core()
        {
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

        private String GetSensorType(SensorType sensorType)
        {
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

        // 处理不合法的转速输入
        private int HandleSpeed(int speed)
        {
            if (speed < 0)
            {
                speed = 0;
            }
            if (speed > 100)
            {
                speed = 100;
            }
            return speed;
        }

        // 获取所有传感器
        private ArrayList GetHardwareAllSensors(IHardware hardware)
        {
            ArrayList sensorList = new ArrayList();
            // 硬件上的传感器
            if (hardware.Sensors.Length > 0)
            {
                foreach (var sensor in hardware.Sensors)
                {
                    sensorList.Add(sensor);
                }
            }

            // 子硬件上的传感器
            foreach (IHardware subHardware in hardware.SubHardware)
            {
                foreach (var sensor in subHardware.Sensors)
                {
                    sensorList.Add(sensor);
                }
            }

            return sensorList;
        }

        // 获取所有风扇控制器
        private ArrayList GetControler()
        {
            _computer.Open();
            _computer.Accept(new UpdateVisitor());
            ArrayList Controls = new ArrayList();
            foreach (IHardware hardware in _computer.Hardware)
            {
                if (
                    hardware.HardwareType == HardwareType.GpuAmd ||
                    hardware.HardwareType == HardwareType.GpuIntel ||
                    hardware.HardwareType == HardwareType.GpuNvidia ||
                    hardware.HardwareType == HardwareType.Motherboard
                    )
                {
                    var SensorList = GetHardwareAllSensors(hardware);
                    foreach (ISensor sensor in SensorList)
                    {
                        if (sensor.SensorType.ToString() == "Control")
                        {
                            Controls.Add(sensor);
                        }
                    }
                }
            }

            return Controls;
        }

        public string GetHardwareMessage()
        {
            _computer.Open();
            _computer.Accept(new UpdateVisitor());
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
                    HardwareList.Add(SubHardwareMap);
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
            _computer.Close();
            return JsonConvert.SerializeObject(HardwardMap);
        }

        public bool SetFanSpeed(string fanName, int speed) {
            var Controls = GetControler();
            bool flag = false;
            foreach (ISensor sensor in Controls)
            {
                if (fanName == sensor.Name)
                {
                    sensor.Control.SetSoftware(HandleSpeed(speed));
                    flag = true;
                    break;
                }
            }
            _computer.Close();
            return flag;
        }
    }
}
