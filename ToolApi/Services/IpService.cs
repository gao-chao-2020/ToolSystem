using System.Net;
using ToolApi.Entitys;

namespace ToolApi.Services
{
    public interface IIpService
    {
        public Task<string> GetIpAddress();
        public Task<string> SelectIpAddress(string name);
        public Task<string> UpdateIpAddress(string name);
    }
    public class IpService : IIpService
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IFreeSql _freeSql;
        public IpService(
            IHttpContextAccessor accessor, IFreeSql freeSql)
        {
            _accessor = accessor;
            _freeSql = freeSql;
        }
        /// <summary>
        /// 获取IP
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetIpAddress()
        {
            //解决ngix、docker等 获取ip问题
            var ip = _accessor.HttpContext?.Request.Headers["x-forwarded-for"].ToString();
            if (string.IsNullOrEmpty(ip))
            {
                //无代理
                ip = _accessor.HttpContext?.Connection?.RemoteIpAddress?.MapToIPv4().ToString();
                if (string.IsNullOrEmpty(ip))
                {//无代理同上再查一次，换写法
                    ip = _accessor.HttpContext?.Request.Headers["remote-addr"].ToString();
                }
            }
            return await Task.FromResult(ip ?? string.Empty);
        }

        public async Task<string> SelectIpAddress(string name)
        {
            var result = _freeSql.Select<IpAddressInfo>().WhereIf(!string.IsNullOrWhiteSpace(name), p => p.Name == name).First();
            return await Task.FromResult((result?.Ip) ?? "");
        }
        public async Task<string> UpdateIpAddress(string name)
        {
            var ip = await GetIpAddress();
            if (!string.IsNullOrWhiteSpace(ip))
            {
                var result = _freeSql.Select<IpAddressInfo>().Where(p => p.Name == name).First();
                if (result != null)
                {
                    result.Ip = ip;
                    var upd = _freeSql.Update<IpAddressInfo>().SetSource(result).Set(p => p.Ip == ip).ExecuteAffrows();
                }
                else
                {
                    IpAddressInfo ipAddressInfo = new IpAddressInfo() { Name = name, Ip = ip };
                    var ins = _freeSql.Insert<IpAddressInfo>(ipAddressInfo).ExecuteAffrows();
                }
            }
            return ip;
        }
    }
}
