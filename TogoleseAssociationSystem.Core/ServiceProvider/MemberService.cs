using Newtonsoft.Json;
using TogoleseAssociationSystem.Core.DTOs;
using TogoleseAssociationSystem.Core.Models;

namespace TogoleseAssociationSystem.Core.ServiceProvider
{
    public class MemberService : IMemberService
    {
        private readonly HttpClient httpClient;
        private static string RequestUri = "api/member";
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
    }
}
