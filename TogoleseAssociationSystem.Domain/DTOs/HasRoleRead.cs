namespace TogoleseAssociationSystem.Domain.DTOs
{
    public class HasRoleRead
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        //public Role? Role { get; set; }
        public int MemberId { get; set; }
        public string? MemberName { get; set; }
        //public Member? Member { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}