using System;
using System.Web.Http;
using System.Web.Http.Cors;
using PCN.BL.DTO;
using PCN.Server.Models;

namespace PCN.Server.Controllers
{
    [EnableCors(origins: "http://localhost:22699", headers: "*", methods: "*")]
    //[RoutePrefix("api/measure/{userid}")]
    public class MeasureController : ApiController
    {
        [HttpGet]
        [Route("api/measure")]
        public IHttpActionResult GetComputersInfo()
        {
            return Ok(StaticStorage.Instance.ComputerIps);
        }

        [Route("api/measure/{userid}/ram")]
        [HttpPost]
        public IHttpActionResult CreateRam([FromUri] Guid userid, [FromBody] RamDto ramInfo)
        {
            StaticStorage.Instance.CreateRamInfo(userid, ramInfo);
            return Created("", "");
        }

        [Route("api/measure/{userid}/cpu")]
        [HttpPost]
        public IHttpActionResult CreateCpu([FromUri] Guid userid, [FromBody] CpuDto cpuInfo)
        {
            StaticStorage.Instance.CreateCpuInfo(userid, cpuInfo);
            return Created("", "");
        }

        [Route("api/measure/{userid}/compinfo")]
        [HttpPost]
        public IHttpActionResult CreateCompInfo([FromUri] Guid userid, [FromBody] ComputerInfoDto compInfo)
        {
            StaticStorage.Instance.CreateComputerInfo(userid, compInfo);
            return Created("", "");
        }
    }
}
