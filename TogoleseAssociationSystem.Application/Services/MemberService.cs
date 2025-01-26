using TogoleseAssociationSystem.Domain.Interfaces;
using TogoleseAssociationSystem.Domain.Models;

namespace TogoleseAssociationSystem.Application.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            this.memberRepository = memberRepository;
        }

        public void CreateMember(Member member)
        {
            memberRepository.CreateMember(member);
        }

        public void CreateMembership(MembershipContribution membership)
        {
            memberRepository.CreateMembership(membership);           
        }

        public async Task<IEnumerable<MembershipContribution>> GetContributionsAsync()
        {
            return await memberRepository.GetContributionsAsync();
        }

        public async Task<Member> GetMemberByIdAsync(Guid id)
        {
            var member = await memberRepository.GetMemberByIdAsync(id);

            if (member == null)
            {
                throw new Exception($"member with id:{id} is not found!");
            }
            return member;
        }

        public async Task<Member> RetrieveMember(string firsname, string lastname)
        {
            var member = await memberRepository.RetrieveMember(firsname, lastname);
            if (member == null)
            {
                throw new Exception($"member with id:{member.FirstName} is not found!");
            }
            return member;
        }

        public Task<IEnumerable<MembershipContribution>> GetMemberContributionsByIdAsync(Guid memberId)
        {
            throw new NotImplementedException();
        }
       
        public async Task<IEnumerable<Member>> GetMembersAsync(string? filter = null)
        {
            var members = await memberRepository.GetMembersAsync(filter);

            if (members.Any())
            {
                return members.ToList();
            }

            throw new Exception("There is no match members found in the db!");
        }

        public async Task<MembershipContribution> GetMembershipByIdAsync(Guid id)
        {
           return await memberRepository.GetMembershipByIdAsync(id);
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateMember(Member member)
        {
            memberRepository.UpdateMember(member);
        }

        public async Task<IEnumerable<Member>> GetAllExisitingMembersAsync()
        {
            return await memberRepository.GetAllExisitingMembersAsync();
        }

        public async Task<Claim> GetClaimByIdAsync(Guid id)
        {
            return await memberRepository.GetClaimByIdAsync(id);
        }

        public async Task<IEnumerable<Claim>> GetClaimsAsync()
        {
            return await memberRepository.GetClaimsAsync();
        }

        public Task CreateClaimAsync(Claim claim)
        {
            return memberRepository.CreateClaimAsync(claim);
        }
    }
}
