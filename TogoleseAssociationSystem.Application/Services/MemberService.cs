using TogoleseAssociationSystem.Application.Repositories;
using TogoleseAssociationSystem.Domain.DTOs;
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
        public Task<MembershipContribution> AddContributionAsync(MembershipContributionToAdd contributionToAdd)
        {
            throw new NotImplementedException();
        }

        public void CreateMember(Member member)
        {
            memberRepository.CreateMember(member);
        }

        public async Task<IEnumerable<MembershipContribution>> GetContributionsAsync()
        {
            return await memberRepository.GetContributionsAsync();
        }

        public async Task<Member> GetMemberByIdAsync(int id)
        {
            var member = await memberRepository.GetMemberByIdAsync(id);

            if (member == null)
            {
                throw new Exception($"member with id:{id} is not found!");
            }
            return member;
        }

        public Task<Member> GetMemberByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MembershipContribution>> GetMemberContributionsByIdAsync(int memberId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HasRole>> GetMemberRolesByIdAsync(int memberId)
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

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
