﻿namespace TogoleseAssociationSystem.Core.Models
{
    public class MembershipContribution
    {
        public int Id { get; set; }
        public string? ContributionName { get; set; }
        public decimal Amount { get; set; }
        public DateTime? DateOfContribution { get; set; }
        public bool? IsAnnualContribution { get; set; }
        public int MemberId { get; set; }
        //public Member? Member { get; set; }
    }
}