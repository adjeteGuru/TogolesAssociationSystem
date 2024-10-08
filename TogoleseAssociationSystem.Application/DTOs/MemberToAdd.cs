﻿namespace TogoleseAssociationSystem.Application.DTOs
{
    public class MemberToAdd
    {
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Telephone { get; set; }
        public string? Address { get; set; }
        public string? Postcode { get; set; }
        public string? City { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsChair { get; set; } = false;
        public byte[] PhotoUrl { get; set; }
        public DateTime? MembershipDate { get; set; }
        public string? NextOfKin { get; set; }
        public string? Relationship { get; set; }
        public List<MembershipContributionToAdd>? Memberships { get; set; }       
        public MemberToAdd()
        {
            Memberships = new List<MembershipContributionToAdd>();            
        }
    }
}
