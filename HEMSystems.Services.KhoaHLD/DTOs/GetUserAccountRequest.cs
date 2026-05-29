using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HEMSystems.Services.KhoaHLD.DTOs
{
    public class GetUserAccountRequest
    {
        public string UserName { get; set; } = default!;

        public string Password { get; set; } = default!;
    }
}
