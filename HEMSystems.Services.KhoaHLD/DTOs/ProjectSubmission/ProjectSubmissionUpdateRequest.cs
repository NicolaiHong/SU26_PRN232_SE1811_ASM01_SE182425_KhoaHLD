using System.ComponentModel.DataAnnotations;

namespace HEMSystems.Services.KhoaHLD.DTOs.ProjectSubmission;

public class ProjectSubmissionUpdateRequest
{
    [Required(ErrorMessage = "TeamId is required.")]
    [StringLength(255, MinimumLength = 1, ErrorMessage = "TeamId must be between 1 and 255 characters.")]
    public string TeamId { get; set; } = string.Empty;

    [Required(ErrorMessage = "RoundId is required.")]
    [StringLength(255, MinimumLength = 1, ErrorMessage = "RoundId must be between 1 and 255 characters.")]
    public string RoundId { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "PlatformKhoahldId must be a positive number.")]
    public int? PlatformKhoahldId { get; set; }

    [Required(ErrorMessage = "ProjectName is required.")]
    [StringLength(255, MinimumLength = 1, ErrorMessage = "ProjectName must be between 1 and 255 characters.")]
    public string ProjectName { get; set; } = string.Empty;

    public string ProjectDescription { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "RepositoryUrl cannot exceed 500 characters.")]
    public string RepositoryUrl { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "DemoUrl cannot exceed 500 characters.")]
    public string DemoUrl { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "DocumentUrl cannot exceed 500 characters.")]
    public string DocumentUrl { get; set; } = string.Empty;

    [Range(typeof(decimal), "0", "99999999.99", ErrorMessage = "RepositorySizeMb must be between 0 and 99999999.99.")]
    public decimal? RepositorySizeMb { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "VersionNumber must be a positive number.")]
    public int? VersionNumber { get; set; }

    [Required(ErrorMessage = "SubmittedBy is required.")]
    [StringLength(255, MinimumLength = 1, ErrorMessage = "SubmittedBy must be between 1 and 255 characters.")]
    public string SubmittedBy { get; set; } = string.Empty;

    [Required(ErrorMessage = "SubmissionStatus is required.")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "SubmissionStatus must be between 1 and 100 characters.")]
    public string SubmissionStatus { get; set; } = string.Empty;

    public bool? IsDeployed { get; set; }

    public bool? IsLateSubmission { get; set; }

    public DateTime? SubmittedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
