using System;

namespace PCN.BL.DTO
{
    public class RamDto
    {
        public DateTime DateTime { get; set; }
        public ulong Usage { get; set; }
        public ulong Total { get; set; }
    }
}