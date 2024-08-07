using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using TogoleseAssociationSystem.Core.DTOs;
using TogoleseAssociationSystem.Core.Models;

namespace TogoleseAssociationSystem.Core.ServiceProvider
{
    public class MemberService : IMemberService
    {
        private readonly HttpClient httpClient;
        private static string RequestUri = "api/member";
        private static string MembershipUri = "api/membership";
        public MemberService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<Member> GetMemberByIdAsync(int id)
        {
            var httpResponse = await httpClient.GetAsync($"{RequestUri}/{id}");
            try
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    var stringContent = await httpResponse.Content.ReadAsStringAsync();
                    var memberRead = JsonConvert.DeserializeObject<Member>(stringContent);
                    if (memberRead == null)
                    {
                        throw new Exception("Not found!");
                    }
                    return memberRead;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return null;
        }

        public async Task<Member> GetMemberByNameAsync(string name)
        {            
            return default(Member);
        }

        public async Task<IEnumerable<Member>> GetMembersAsync(string? filter = null)
        {
            var httpResponse = await httpClient.GetAsync($"{RequestUri}");
            try
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    var stringContent = await httpResponse.Content.ReadAsStringAsync();
                    var membersRead = JsonConvert.DeserializeObject<IEnumerable<Member>>(stringContent);
                    if (membersRead == null)
                    {
                        throw new Exception("Not found!");
                    }
                    return membersRead;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return null;
        }

        public async Task<Member> CreateMemberAsync(MemberToAdd memberToAdd)
        {
            var response = await httpClient.PostAsJsonAsync($"{RequestUri}", memberToAdd);
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var stringContent = await response.Content.ReadAsStringAsync();

                    var member = JsonConvert.DeserializeObject<Member>(stringContent);

                    if (member == null)
                    {
                        throw new Exception("Not found!");
                    }
                  
                    return member;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return null;
        }

        public async Task<MembershipContribution> CreateMembershipAsync(MembershipContributionToAdd contributionToAdd)
        {
            var response = await httpClient.PostAsJsonAsync($"{MembershipUri}", contributionToAdd);
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var stringContent = await response.Content.ReadAsStringAsync();

                    var membership = JsonConvert.DeserializeObject<MembershipContribution>(stringContent);

                    if (membership == null)
                    {
                        throw new Exception("Not found!");
                    }

                    return membership;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return null;
        }

        public async Task<Member> UpdateMemberDetails(Member member)
        {
            var json = JsonConvert.SerializeObject(member);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var newRequestUri = $"api/member/" + member.Id;

            var response = await httpClient.PutAsync(newRequestUri, content);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var memberUpdated = JsonConvert.DeserializeObject<Member>(responseContent);

                    if (memberUpdated == null)
                    {
                        throw new Exception("Not found!");
                    }                 

                    return memberUpdated;
                }

                var errorContent = await response.Content.ReadAsStringAsync();

                var failureResponse = JsonConvert.DeserializeObject<FailureResponseModel>(errorContent)?.Detail;

                if (failureResponse != null)
                {
                    throw new Exception(failureResponse);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return null;
        }
    }
}
