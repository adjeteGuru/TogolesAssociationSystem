namespace TogoleseSolidarity.Domain.Models;


public class Claim : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }        
    public Guid MemberId { get; set; }
    public ClaimType ClaimType { get; set; }
    public string? NextOfKinName { get; set; }
    public string? NextOfKinContact { get; set; }
    public DateTime? ClaimDate { get; set; } = DateTime.Today;
}
