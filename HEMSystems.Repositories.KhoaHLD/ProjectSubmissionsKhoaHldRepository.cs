using HEMSystems.Entities.KhoaHLD.Models;
using HEMSystems.Repositories.KhoaHLD.Base;
using HEMSystems.Repositories.KhoaHLD.DBContext;
using Microsoft.EntityFrameworkCore;

namespace HEMSystems.Repositories.KhoaHLD
{
    public class ProjectSubmissionsKhoaHldRepository : GenericRepository<ProjectSubmissionsKhoaHld>
    {
        public ProjectSubmissionsKhoaHldRepository()
        {
        }

        public ProjectSubmissionsKhoaHldRepository(HackathonManagementSystemContext context) => _context = context;

        public new async Task<List<ProjectSubmissionsKhoaHld>> GetAllAsync()
        {
            return await _context.ProjectSubmissionsKhoaHlds
                .Include(p => p.PlatformKhoahld)
                .ToListAsync();
        }

        public new async Task<ProjectSubmissionsKhoaHld?> GetByIdAsync(int id)
        {
            return await _context.ProjectSubmissionsKhoaHlds
                .Include(p => p.PlatformKhoahld)
                .FirstOrDefaultAsync(p => p.SubmissionKhoahldId == id);
        }

        public async Task<(List<ProjectSubmissionsKhoaHld> Items, int TotalItems)> SearchAsync(
            string? keyword,
            string? teamId,
            string? roundId,
            int pageNumber,
            int pageSize)
        {
            var query = _context.ProjectSubmissionsKhoaHlds
                .Include(p => p.PlatformKhoahld)
                .Where(p =>
                    (string.IsNullOrEmpty(keyword)
                        || p.ProjectName.Contains(keyword)
                        || p.ProjectDescription.Contains(keyword))
                    && (string.IsNullOrEmpty(teamId) || p.TeamId == teamId)
                    && (string.IsNullOrEmpty(roundId) || p.RoundId == roundId));

            var totalItems = await query.CountAsync();

            var items = await query
                .OrderBy(p => p.SubmissionKhoahldId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalItems);
        }
    }
}
