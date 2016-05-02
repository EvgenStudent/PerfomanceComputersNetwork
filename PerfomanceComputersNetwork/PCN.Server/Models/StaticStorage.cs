using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using PCN.BL.DTO;

namespace PCN.Server.Models
{
    public sealed class StaticStorage
    {
        private static volatile StaticStorage instance;
        private static readonly object syncRoot = new object();

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

        public void CreateRamInfo(Guid id, RamDto info)
        {
            info.DateTime = DateTime.Now;
            if (!RamInfo.ContainsKey(id))
                RamInfo.Add(id, new List<RamDto> { info });
            else
                RamInfo[id].Add(info);
        }

        public void CreateCpuInfo(Guid id, CpuDto info)
        {
            info.DateTime = DateTime.Now;
            if (!CpuInfo.ContainsKey(id))
                CpuInfo.Add(id, new List<CpuDto> { info });
            else
                CpuInfo[id].Add(info);
        }

        public void InitTestData()
        {
            var rand = new Random();

            for (int i = 1; i < 6; i++)
            {
                var guid = Guid.NewGuid();
                ComputersInfo.Add(guid, new ComputerInfoDto { Ip = $"{rand.Next(1, 255)}.{rand.Next(1, 255)}.{rand.Next(1, 255)}.{rand.Next(1, 255)}", ProcessorName = "Intel i" + i, SoundDevice = "Realtek v" + i, VideoController = "Xamarin" });
            }

        }
    }
}