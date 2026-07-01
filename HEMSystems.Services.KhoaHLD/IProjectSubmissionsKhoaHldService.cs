using HEMSystems.Entities.KhoaHLD.Models;
using HEMSystems.Services.KhoaHLD.DTOs.Common;
using HEMSystems.Services.KhoaHLD.DTOs.ProjectSubmission;

namespace HEMSystems.Services.KhoaHLD
{
    public interface IProjectSubmissionsKhoaHldService
    {
        Task<List<ProjectSubmissionsKhoaHld>> GetAllProjectSubmissionsAsync();

        Task<ProjectSubmissionGetByIdResponse?> GetProjectSubmissionByIdAsync(int submissionId);

        Task<PagedResult<ProjectSubmissionsKhoaHld>> SearchProjectSubmissionsAsync(
            string? keyword,
            string? teamId,
            string? roundId,
            int pageNumber,
            int pageSize);

        Task<int> CreateProjectSubmissionAsync(ProjectSubmissionCreateRequest submission);

        Task<int> UpdateProjectSubmissionAsync(int submissionId, ProjectSubmissionUpdateRequest submission);

        Task<bool> DeleteProjectSubmissionAsync(int submissionId);
    }
}
