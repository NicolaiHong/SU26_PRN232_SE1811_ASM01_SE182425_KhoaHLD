using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HEMSystems.Entities.KhoaHLD.Models;
using HEMSystems.Repositories.KhoaHLD;

namespace HEMSystems.Services.KhoaHLD
{
    public class DevelopmentPlatformsKhoaHldSevice : IDevelopmentPlatformsKhoaHldSevice
    {
        private readonly DevelopmentPlatformsKhoaHldRepository _repository;
        public DevelopmentPlatformsKhoaHldSevice(DevelopmentPlatformsKhoaHldRepository repository) => _repository = repository;
        public async Task<List<ProjectSubmissionsKhoaHld>> GetAll()
        {
            return await Task.Run(() => _repository.GetAllProjects());
        }
    }
}
