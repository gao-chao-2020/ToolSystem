using FreeSql.DataAnnotations;
using System.ComponentModel;
using System.Data;

namespace ToolApi.Entitys
{
    [Table(Name = "IpAddressInfo")]
    public class IpAddressInfo
    {
        [Column(Position = 1, IsPrimary = true, IsIdentity = true)]
        public long Id { get; set; }
        [Column(StringLength = 128)]
        public string? Name { get; set; }
        [Column(StringLength = 128)]
        public string? Ip { get; set; }
    }
}
