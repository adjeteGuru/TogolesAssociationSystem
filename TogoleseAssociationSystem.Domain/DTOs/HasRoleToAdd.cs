using TogoleseAssociationSystem.Domain.Models;

namespace TogoleseAssociationSystem.Domain.DTOs
{
    public class HasRoleToAdd
    {
        public int RoleId { get; set; }
        public Role? Role { get; set; }
        public int MemberId { get; set; }
        public Member? Member { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
