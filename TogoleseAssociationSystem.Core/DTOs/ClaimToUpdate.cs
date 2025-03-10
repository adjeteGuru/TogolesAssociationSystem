﻿using TogoleseAssociationSystem.Core.Models;

namespace TogoleseAssociationSystem.Core.DTOs
{
    public class ClaimToUpdate
    {        
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid MemberId { get; set; }
        public ClaimType ClaimType { get; set; }
        public string? NextOfKinName { get; set; }
        public string? NextOfKinContact { get; set; }
        public DateTime? ClaimDate { get; set; } = DateTime.Today;
        //public int ClaimRemain { get; set; }
        //public int TotalClaimPerMember { get; set; }
    }
}
