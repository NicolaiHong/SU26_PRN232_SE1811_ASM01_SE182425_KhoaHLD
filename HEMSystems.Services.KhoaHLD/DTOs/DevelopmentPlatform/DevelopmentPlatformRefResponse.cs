namespace HEMSystems.Services.KhoaHLD.DTOs.DevelopmentPlatform;

public class DevelopmentPlatformRefResponse
{
    public int PlatformKhoahldId { get; set; }

    public string PlatformName { get; set; } = string.Empty;

    public string PlatformDescription { get; set; } = string.Empty;

    public string WebsiteUrl { get; set; } = string.Empty;

    public string SupportEmail { get; set; } = string.Empty;

    public bool? IsActive { get; set; }
}
