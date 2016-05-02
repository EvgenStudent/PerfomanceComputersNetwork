using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Microsoft.VisualBasic.Devices;
using PCN.BL.DTO;

namespace PCN.BL.Services
{
    public class MeasureService
    {
        public RamDto GetRamUsage()
        {
            var pc = new ComputerInfo();
            return new RamDto
            {
                Total = pc.TotalPhysicalMemory,
                Usage = pc.TotalPhysicalMemory - pc.AvailablePhysicalMemory
            };
        }

        public double GetCpuUsage()
        {
            var cpuCounter = new PerformanceCounter
            {
                CategoryName = "Processor",
                CounterName = "% Processor Time",
                InstanceName = "_Total"
            };

            dynamic firstValue = cpuCounter.NextValue();
            Thread.Sleep(1000);
            dynamic secondValue = cpuCounter.NextValue();

            return secondValue;
        }

        public ComputerInfoDto GetComputerInfo()
        {
            var ipAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            var res = new ComputerInfoDto
            {
                ProcessorName = GetComponent("Win32_Processor", "Name"),
                VideoController = GetComponent("Win32_VideoController", "Name"),
                SoundDevice = GetComponent("Win32_SoundDevice", "Name"),
                Ip = ipAddress?.ToString() ?? string.Empty
            };

            return res;
        }

        private static string GetComponent(string hwclass, string syntax)
        {
            var mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM " + hwclass);
            var sb = new StringBuilder();
            foreach (var mj in mos.Get())
                sb.AppendLine(Convert.ToString(mj[syntax]));
            return sb.ToString();
        }
    }
}