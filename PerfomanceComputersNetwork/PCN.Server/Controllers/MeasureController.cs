using System;
using System.Web.Http;
using System.Web.Http.Cors;
using PCN.BL.DTO;
using PCN.Server.Models;

namespace PCN.Server.Controllers
{
    [EnableCors(origins: "http://pcnwebadminpanel.azurewebsites.net", headers: "*", methods: "*")]
    //[RoutePrefix("api/measure/{userid}")]
    public class MeasureController : ApiController
    {
        [HttpGet]
        [Route("api/measure")]
        public IHttpActionResult GetComputersInfo()
        {
            return Ok(StaticStorage.Instance.ComputerIps);
        }

        // GET

        [Route("api/measure/{userid}/ram")]
        [HttpGet]
        public IHttpActionResult GetRam([FromUri] Guid userid)
        {
            return Ok(StaticStorage.Instance.GetRamInfo(userid));
        }

        [Route("api/measure/{userid}/cpu")]
        [HttpGet]
        public IHttpActionResult GetCpu([FromUri] Guid userid)
        {
            return Ok(StaticStorage.Instance.GetCpuInfo(userid));
        }

        [Route("api/measure/{userid}/compinfo")]
        [HttpGet]
        public IHttpActionResult GetCompInfo([FromUri] Guid userid)
        {
            return Ok(StaticStorage.Instance.GetComputerInfo(userid));
        }

        // POST

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
