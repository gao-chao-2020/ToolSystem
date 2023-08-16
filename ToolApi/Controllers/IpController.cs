using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using ToolApi.Services;

namespace ToolApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class IpController : ControllerBase
    {
        public IIpService _ipService { get; set; }

        public IpController(IIpService ipService)
        {
            _ipService = ipService;
        }

        [HttpGet(Name = "GetIp")]
        public async Task<string> GetIp()
        {
            var ip = await _ipService.GetIpAddress();
            return ip;
        }
        [HttpGet(Name = "SelectIp")]
        public async Task<string> SelectIp(string name)
        {
            var ip = await _ipService.SelectIpAddress(name);
            return ip;
        }
        [HttpGet(Name = "UpdateIp")]
        public async Task<string> UpdateIp(string name)
        {
            var ip = await _ipService.UpdateIpAddress(name);
            return ip;
        }
    }
}