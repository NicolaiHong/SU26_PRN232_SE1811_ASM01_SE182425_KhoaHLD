using System.ComponentModel.DataAnnotations;

namespace HEMSystems.Services.KhoaHLD.DTOs.ProjectSubmission;

public class ProjectSubmissionSearchRequest
{
    [StringLength(255, ErrorMessage = "Keyword cannot exceed 255 characters.")]
    public string? Keyword { get; set; }

    [StringLength(255, ErrorMessage = "TeamId cannot exceed 255 characters.")]
    public string? TeamId { get; set; }

    [StringLength(255, ErrorMessage = "RoundId cannot exceed 255 characters.")]
    public string? RoundId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "PageNumber must be at least 1.")]
    public int PageNumber { get; set; } = 1;

    [Range(1, 100, ErrorMessage = "PageSize must be between 1 and 100.")]
    public int PageSize { get; set; } = 10;
}
