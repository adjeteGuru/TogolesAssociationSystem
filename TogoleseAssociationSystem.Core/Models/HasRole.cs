namespace TogoleseAssociationSystem.Core.Models
{
    public class HasRole : BaseEntity
    {
        //public int Id { get; set; }
        public Guid RoleId { get; set; }
        //public Role? Role { get; set; }
        public Guid MemberId { get; set; }
        //public Member? Member { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
