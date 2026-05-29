using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HEMSystems.Services.KhoaHLD.DTOs
{
    public class GetUserAccountResponse
    {
        public int UserAccountId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string EmployeeCode { get; set; } = string.Empty;

        public int RoleId { get; set; }

        public string RequestCode { get; set; } = string.Empty;

        public DateTime? CreatedDate { get; set; }

        public string ApplicationCode { get; set; } = string.Empty;

        public string CreatedBy { get; set; } = string.Empty;

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}
