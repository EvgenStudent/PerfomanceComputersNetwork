using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using PCN.BL.DTO;
using PCN.Server.Extensions;

namespace PCN.Server.Models
{
    public sealed class StaticStorage
    {
        private static volatile StaticStorage instance;
        private static readonly object syncRoot = new object();

        private const int RetriweMaxCount = 50;

        private StaticStorage()
        {
        }

        public static StaticStorage Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new StaticStorage();
                    }
                }

                return instance;
            }
        }

        private static readonly IDictionary<Guid, ComputerInfoDto> ComputersInfo = new ConcurrentDictionary<Guid, ComputerInfoDto>();
        private static readonly IDictionary<Guid, IList<RamDto>> RamInfo = new ConcurrentDictionary<Guid, IList<RamDto>>();
        private static readonly IDictionary<Guid, IList<CpuDto>> CpuInfo = new ConcurrentDictionary<Guid, IList<CpuDto>>();

        public dynamic ComputerIps
        {
            get
            {
                var list = new List<dynamic>();
                foreach (var dto in ComputersInfo)
                    list.Add(new { id = dto.Key.ToString("D"), ip = dto.Value.Ip });
                return list;
            }
        }

        public void CreateComputerInfo(Guid id, ComputerInfoDto info)
        {
            if (!ComputersInfo.ContainsKey(id))
                ComputersInfo.Add(id, info);
            else
                ComputersInfo[id] = info;
        }

        public ComputerInfoDto GetComputerInfo(Guid id)
        {
            return ComputersInfo.ContainsKey(id) ? ComputersInfo[id] : null;
        }

        public IEnumerable<RamDto> GetRamInfo(Guid id)
        {
            return RamInfo[id].TakeLast(RetriweMaxCount);
        }

        public IEnumerable<CpuDto> GetCpuInfo(Guid id)
        {
            return CpuInfo[id].TakeLast(RetriweMaxCount);
        }

        public void CreateRamInfo(Guid id, RamDto info)
        {
            if (!RamInfo.ContainsKey(id))
                RamInfo.Add(id, new List<RamDto> { info });
            else
                RamInfo[id].Add(info);
        }

        public void CreateCpuInfo(Guid id, CpuDto info)
        {
            if (!CpuInfo.ContainsKey(id))
                CpuInfo.Add(id, new List<CpuDto> { info });
            else
                CpuInfo[id].Add(info);
        }

        public void InitTestData()
        {
            var rand = new Random();
            const int count = 50;

            for (int i = 1; i < 6; i++)
            {
                var guid = Guid.NewGuid();
                ComputersInfo.Add(guid, new ComputerInfoDto { Ip = $"{rand.Next(1, 255)}.{rand.Next(1, 255)}.{rand.Next(1, 255)}.{rand.Next(1, 255)}", ProcessorName = "Intel i" + i, SoundDevice = "Realtek v" + i, VideoController = "Xamarin" });

                var ramList = new List<RamDto>();
                for (var j = 0; j < count; j++)
                    ramList.Add(new RamDto { DateTime = DateTime.Now.AddMinutes(i + j), Total = 8589934592, Usage = 1048576 * (ulong)rand.Next(20, 8192) });
                RamInfo.Add(guid, ramList);

                var cpuList = new List<CpuDto>();
                for (var j = 0; j < count; j++)
                    cpuList.Add(new CpuDto {DateTime = DateTime.Now.AddMinutes(i+j), Value = rand.Next(1, 101)});
                CpuInfo.Add(guid, cpuList);
            }

        }
    }
}