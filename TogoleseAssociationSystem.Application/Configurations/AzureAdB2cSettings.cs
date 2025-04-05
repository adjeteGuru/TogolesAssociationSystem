namespace TogoleseSolidarity.Application.Configurations;

public class AzureAdB2cSettings
{
    public string? Instance { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string TenantId { get; set; } = string.Empty;
    public string Domain { get; set; } = string.Empty;
    public string SignUpSignInPolicyId { get; set; } = string.Empty;
    public string ResetPasswordPolicyId { get; set; } = string.Empty;
    public string EditProfilePolicyId { get; set; } = string.Empty;

}
