using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Auth.DTO
{
    public class UserInfoModel
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public IEnumerable<ClaimModel>? Claims { get; set; }
        public class ClaimModel
        {
            public string Type { get; set; } = null!;
            public string Value { get; set; } = null!;
        }
    }
}
