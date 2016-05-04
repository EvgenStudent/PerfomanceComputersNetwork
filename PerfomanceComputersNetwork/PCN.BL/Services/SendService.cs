using System;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using PCN.BL.DTO;

namespace PCN.BL.Services
{
    public class SendService
    {
        private readonly Uri _baseApiUri;

        public SendService(Uri baseApiUri)
        {
            _baseApiUri = baseApiUri;

            Guid macAddressGuid;
            var networkInterface = NetworkInterface.GetAllNetworkInterfaces();
            var id = networkInterface.FirstOrDefault().Id;
            Guid.TryParse(id, out macAddressGuid);
            NetworkGuid = macAddressGuid;
        }

        public async Task SendComputerInfo(ComputerInfoDto info)
        {
            using (var client = new HttpClient())
            {
                var result = await client.PostAsJsonAsync(new Uri(_baseApiUri + $"/measure/{NetworkGuid}/compinfo"), info);
            }
        }

        public async Task SendCpu(CpuDto info)
        {
            using (var client = new HttpClient())
            {
                var result = await client.PostAsJsonAsync(new Uri(_baseApiUri + $"/measure/{NetworkGuid}/cpu"), info);
            }
        }

        public async Task SendRam(RamDto info)
        {
            using (var client = new HttpClient())
            {
                var result = await client.PostAsJsonAsync(new Uri(_baseApiUri + $"/measure/{NetworkGuid}/ram"), info);
            }
        }

        public Guid NetworkGuid { get; }
    }
}