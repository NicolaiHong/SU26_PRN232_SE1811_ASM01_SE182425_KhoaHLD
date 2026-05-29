using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HEMSystems.Entities.KhoaHLD.Models;
using HEMSystems.Repositories.KhoaHLD.Base;
using HEMSystems.Repositories.KhoaHLD.DBContext;
using Microsoft.EntityFrameworkCore;

namespace HEMSystems.Repositories.KhoaHLD
{
    public class DevelopmentPlatformsKhoaHldRepository :GenericRepository<DevelopmentPlatformsKhoaHld>
    {
        public DevelopmentPlatformsKhoaHldRepository()
        {
        }
        public DevelopmentPlatformsKhoaHldRepository(HackathonManagementSystemContext context) => _context = context;
        public async Task<List<ProjectSubmissionsKhoaHld>> GetAllProjects()
        {

            return await _context.ProjectSubmissionsKhoaHlds.ToListAsync();
        }
        public async Task<ProjectSubmissionsKhoaHld> GetProjectsByUserId(int submissionId)
        {
            return await _context.ProjectSubmissionsKhoaHlds.Include(p => p.PlatformKhoahld)
                .Where(p => p.SubmissionKhoahldId == submissionId).FirstOrDefaultAsync();
        }
        public async Task<List<ProjectSubmissionsKhoaHld>> GetProjectsByTeamId(string teamId, string roundId)
        {
            return await _context.ProjectSubmissionsKhoaHlds.Where(p => p.TeamId == teamId && p.RoundId == roundId).ToListAsync();
        }
    }
}
