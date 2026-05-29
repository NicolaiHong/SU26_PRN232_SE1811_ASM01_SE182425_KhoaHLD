using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HEMSystems.Entities.KhoaHLD.Models;
using HEMSystems.Services.KhoaHLD.DTOs;

namespace HEMSystems.Services.KhoaHLD
{
    public interface ISystemUserAccountService
    {
        Task<GetUserAccountResponse?> GetUserAccount(GetUserAccountRequest request);

    }
}
