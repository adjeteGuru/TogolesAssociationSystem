﻿namespace TogoleseAssociationSystem.Domain.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsChair { get; set; } = false;
        public string? PhotoUrl { get; set; } = "https://via.placeholder.com/300x300";
        public DateTime? MembershipDate { get; set; }
    }
}