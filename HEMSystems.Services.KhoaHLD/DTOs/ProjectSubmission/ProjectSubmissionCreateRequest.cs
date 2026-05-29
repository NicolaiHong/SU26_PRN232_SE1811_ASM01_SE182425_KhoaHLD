namespace HEMSystems.Services.KhoaHLD.DTOs.ProjectSubmission;

public class ProjectSubmissionCreateRequest
{
    public string TeamId { get; set; } = string.Empty;

    public string RoundId { get; set; } = string.Empty;

    public int? PlatformKhoahldId { get; set; }

    public string ProjectName { get; set; } = string.Empty;

    public string ProjectDescription { get; set; } = string.Empty;

    public string RepositoryUrl { get; set; } = string.Empty;

    public string DemoUrl { get; set; } = string.Empty;

    public string DocumentUrl { get; set; } = string.Empty;

    public decimal? RepositorySizeMb { get; set; }

    public int? VersionNumber { get; set; }

    public string SubmittedBy { get; set; } = string.Empty;

    public string SubmissionStatus { get; set; } = string.Empty;

    public bool? IsDeployed { get; set; }

    public bool? IsLateSubmission { get; set; }

    public DateTime? SubmittedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
