using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HEMSystems.Entities.KhoaHLD.Models;

namespace HEMSystems.Services.KhoaHLD
{
    public interface IDevelopmentPlatformsKhoaHldSevice
    {
        Task<List<ProjectSubmissionsKhoaHld>> GetAll();
    }
}
