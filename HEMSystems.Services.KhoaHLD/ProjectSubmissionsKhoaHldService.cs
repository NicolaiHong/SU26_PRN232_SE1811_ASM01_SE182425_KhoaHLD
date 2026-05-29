using HEMSystems.Entities.KhoaHLD.Models;
using HEMSystems.Repositories.KhoaHLD;
using HEMSystems.Services.KhoaHLD.DTOs.DevelopmentPlatform;
using HEMSystems.Services.KhoaHLD.DTOs.ProjectSubmission;

namespace HEMSystems.Services.KhoaHLD
{
    public class ProjectSubmissionsKhoaHldService : IProjectSubmissionsKhoaHldService
    {
        private readonly ProjectSubmissionsKhoaHldRepository _repository;

        public ProjectSubmissionsKhoaHldService(ProjectSubmissionsKhoaHldRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateProjectSubmissionAsync(ProjectSubmissionCreateRequest submission)
        {
            try
            {
                var item = MapToEntity(submission);
                return await _repository.CreateAsync(item);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while creating the project submission.", ex);
            }
        }

        public async Task<bool> DeleteProjectSubmissionAsync(int submissionId)
        {
            try
            {
                var submission = await _repository.GetByIdAsync(submissionId);

                if (submission == null)
                    return false;

                return await _repository.RemoveAsync(submission);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting the project submission: {ex.Message}");
            }

            return false;
        }

        public async Task<List<ProjectSubmissionsKhoaHld>> GetAllProjectSubmissionsAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving project submissions: {ex.Message}");
            }

            return [];
        }

        public async Task<ProjectSubmissionGetByIdResponse?> GetProjectSubmissionByIdAsync(int submissionId)
        {
            try
            {
                var submission = await _repository.GetByIdAsync(submissionId);

                return submission == null ? null : MapToGetByIdResponse(submission);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving the project submission: {ex.Message}");
            }

            return null;
        }

        public async Task<List<ProjectSubmissionsKhoaHld>> SearchProjectSubmissionsAsync(
            string? keyword,
            string? teamId,
            string? roundId,
            bool? isDeployed)
        {
            try
            {
                return await _repository.SearchAsync(keyword, teamId, roundId, isDeployed);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while searching project submissions: {ex.Message}");
            }

            return [];
        }

        public async Task<int> UpdateProjectSubmissionAsync(int submissionId, ProjectSubmissionUpdateRequest submission)
        {
            var existingSubmission = await _repository.GetByIdAsync(submissionId);

            if (existingSubmission == null)
            {
                Console.WriteLine("Project submission is not found.");
                return 0;
            }

            var item = MapToEntity(submission);
            item.SubmissionKhoahldId = submissionId;

            return await _repository.UpdateAsync(item);
        }

        private static ProjectSubmissionsKhoaHld MapToEntity(ProjectSubmissionCreateRequest submission)
        {
            return new ProjectSubmissionsKhoaHld
            {
                TeamId = submission.TeamId,
                RoundId = submission.RoundId,
                PlatformKhoahldId = submission.PlatformKhoahldId,
                ProjectName = submission.ProjectName,
                ProjectDescription = submission.ProjectDescription,
                RepositoryUrl = submission.RepositoryUrl,
                DemoUrl = submission.DemoUrl,
                DocumentUrl = submission.DocumentUrl,
                RepositorySizeMb = submission.RepositorySizeMb,
                VersionNumber = submission.VersionNumber,
                SubmittedBy = submission.SubmittedBy,
                SubmissionStatus = submission.SubmissionStatus,
                IsDeployed = submission.IsDeployed,
                IsLateSubmission = submission.IsLateSubmission,
                SubmittedAt = submission.SubmittedAt,
                UpdatedAt = submission.UpdatedAt
            };
        }

        private static ProjectSubmissionsKhoaHld MapToEntity(ProjectSubmissionUpdateRequest submission)
        {
            return new ProjectSubmissionsKhoaHld
            {
                TeamId = submission.TeamId,
                RoundId = submission.RoundId,
                PlatformKhoahldId = submission.PlatformKhoahldId,
                ProjectName = submission.ProjectName,
                ProjectDescription = submission.ProjectDescription,
                RepositoryUrl = submission.RepositoryUrl,
                DemoUrl = submission.DemoUrl,
                DocumentUrl = submission.DocumentUrl,
                RepositorySizeMb = submission.RepositorySizeMb,
                VersionNumber = submission.VersionNumber,
                SubmittedBy = submission.SubmittedBy,
                SubmissionStatus = submission.SubmissionStatus,
                IsDeployed = submission.IsDeployed,
                IsLateSubmission = submission.IsLateSubmission,
                SubmittedAt = submission.SubmittedAt,
                UpdatedAt = submission.UpdatedAt
            };
        }

        private static ProjectSubmissionGetByIdResponse MapToGetByIdResponse(ProjectSubmissionsKhoaHld submission)
        {
            return new ProjectSubmissionGetByIdResponse
            {
                SubmissionKhoahldId = submission.SubmissionKhoahldId,
                TeamId = submission.TeamId,
                RoundId = submission.RoundId,
                PlatformKhoahldId = submission.PlatformKhoahldId,
                ProjectName = submission.ProjectName,
                ProjectDescription = submission.ProjectDescription,
                RepositoryUrl = submission.RepositoryUrl,
                DemoUrl = submission.DemoUrl,
                DocumentUrl = submission.DocumentUrl,
                RepositorySizeMb = submission.RepositorySizeMb,
                VersionNumber = submission.VersionNumber,
                SubmittedBy = submission.SubmittedBy,
                SubmissionStatus = submission.SubmissionStatus,
                IsDeployed = submission.IsDeployed,
                IsLateSubmission = submission.IsLateSubmission,
                SubmittedAt = submission.SubmittedAt,
                UpdatedAt = submission.UpdatedAt,
                Platform = submission.PlatformKhoahld == null
                    ? null
                    : new DevelopmentPlatformRefResponse
                    {
                        PlatformKhoahldId = submission.PlatformKhoahld.PlatformKhoahldId,
                        PlatformName = submission.PlatformKhoahld.PlatformName,
                        PlatformDescription = submission.PlatformKhoahld.PlatformDescription,
                        WebsiteUrl = submission.PlatformKhoahld.WebsiteUrl,
                        SupportEmail = submission.PlatformKhoahld.SupportEmail,
                        IsActive = submission.PlatformKhoahld.IsActive
                    }
            };
        }
    }
}
