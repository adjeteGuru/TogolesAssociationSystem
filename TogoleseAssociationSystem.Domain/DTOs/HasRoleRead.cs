namespace TogoleseAssociationSystem.Domain.DTOs
{
    public class HasRoleRead
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public string? RoleName { get; set; }
        //public Role? Role { get; set; }
        public Guid MemberId { get; set; }
        public string? MemberName { get; set; }
        //public Member? Member { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}