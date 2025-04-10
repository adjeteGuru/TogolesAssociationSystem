﻿namespace TogoleseSolidarity.Application.DTOs;

public class MemberUpdateDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Telephone { get; set; }
    public string? Address { get; set; }
    public string? Postcode { get; set; }
    public string? City { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsEligibleToClaim { get; set; }
    public DateTime? MembershipDate { get; set; }
    public string? NextOfKin { get; set; }
    public string? Relationship { get; set; }
    public string? NextOfKinContact { get; set; }
    public int? TotalClaimRemain { get; set; }
    public List<MembershipContributionToUpdate>? Memberships { get; set; }
    public List<ClaimToUpdate>? Claims { get; set; }
}
