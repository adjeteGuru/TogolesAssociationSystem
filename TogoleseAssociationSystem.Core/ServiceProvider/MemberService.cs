using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using TogoleseAssociationSystem.Core.DTOs;
using TogoleseAssociationSystem.Core.Models;
using TogoleseAssociationSystem.Core.ServiceProvider.Interfaces;

namespace TogoleseAssociationSystem.Core.ServiceProvider
{
    public class MemberService : IMemberService
    {
        private readonly HttpClient httpClient;
        private IAlertService alertService;
        private static string RequestUri = "api/member";
        private static string MembershipUri = "api/membership";
        public MemberService(HttpClient httpClient, IAlertService alertService)
        {
            this.httpClient = httpClient;
            this.alertService = alertService;
        }
        public async Task<MemberRead> GetMemberByIdAsync(Guid id)
        {
            var httpResponse = await httpClient.GetAsync($"{RequestUri}/{id}");
            try
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    var stringContent = await httpResponse.Content.ReadAsStringAsync();
                    var memberRead = JsonConvert.DeserializeObject<MemberRead>(stringContent);
                    if (memberRead == null)
                    {
                       alertService.ShowAlert("No member found");
                    }
                    return memberRead;
                }
                if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                {
                    alertService.ShowAlert("Bad request.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return null;
        }
       
        public async Task<IEnumerable<MemberRead>> GetMembersAsync(string? filter = null)
        {
            var httpResponse = await httpClient.GetAsync($"{RequestUri}?filter={filter}");
            try
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    var stringContent = await httpResponse.Content.ReadAsStringAsync();
                    var membersRead = JsonConvert.DeserializeObject<IEnumerable<MemberRead>>(stringContent);
                    if (membersRead == null || !membersRead.Any())
                    {                        
                        return [];
                    }
                    return membersRead;
                }
                if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                {
                    alertService.ShowAlert("Bad request.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //alertService.ShowAlert(ex.Message);
            }

            return null;
        }

        public async Task<MemberRead> CreateMemberAsync(MemberToAdd memberToAdd)
        {
            var httpResponse = await httpClient.PostAsJsonAsync($"{RequestUri}", memberToAdd);
            try
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    var stringContent = await httpResponse.Content.ReadAsStringAsync();

                    var member = JsonConvert.DeserializeObject<MemberRead>(stringContent);

                    if (member == null)
                    {
                        alertService.ShowAlert("No member found");
                    }

                    return member;
                }
                if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                {
                    alertService.ShowAlert("Bad request.");
                }
            }
            catch (Exception e)
            {
                //throw new Exception(e.Message);
                alertService.ShowAlert(e.Message);
            }

            return null;
        }

        public async Task<MembershipContributionReadDto> CreateMembershipAsync(MembershipContributionToAdd contributionToAdd)
        {
            var httpResponse = await httpClient.PostAsJsonAsync($"{MembershipUri}", contributionToAdd);
            try
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    var stringContent = await httpResponse.Content.ReadAsStringAsync();

                    var membership = JsonConvert.DeserializeObject<MembershipContributionReadDto>(stringContent);

                    if (membership == null)
                    {
                        alertService.ShowAlert("No member found");
                    }

                    return membership;
                }
                if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                {
                    alertService.ShowAlert("Bad request.");
                }
            }
            catch (Exception e)
            {
                //throw new Exception(e.Message);
                alertService.ShowAlert(e.Message);
            }

            return null;
        }

        public async Task<MemberRead> UpdateMemberDetails(MemberUpdateDto member)
        {
            var json = JsonConvert.SerializeObject(member);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var newRequestUri = $"api/member/" + member.Id;

            var httpResponse = await httpClient.PutAsync(newRequestUri, content);

            try
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();

                    var memberUpdated = JsonConvert.DeserializeObject<MemberRead>(responseContent);

                    if (memberUpdated == null)
                    {
                        alertService.ShowAlert("No member found");
                    }

                    return memberUpdated;
                }
                if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                {
                    alertService.ShowAlert("Bad request.");
                }

                var errorContent = await httpResponse.Content.ReadAsStringAsync();

                var failureResponse = JsonConvert.DeserializeObject<FailureResponseModel>(errorContent)?.Detail;

                if (failureResponse != null)
                {
                    //throw new Exception(failureResponse);
                    alertService.ShowAlert(failureResponse);
                }
            }
            catch (Exception e)
            {
                //throw new Exception(e.Message);
                alertService.ShowAlert(e.Message);
            }

            return null;
        }

        public async Task<IEnumerable<MemberRead>> GetAllExistingMembersAsync()
        {
            var httpResponse = await httpClient.GetAsync($"{MembershipUri}");
            try
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    var stringContent = await httpResponse.Content.ReadAsStringAsync();
                    var membersRead = JsonConvert.DeserializeObject<IEnumerable<MemberRead>>(stringContent);
                    if (membersRead == null)
                    {
                        return [];
                    }
                    return membersRead;
                }
                if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                {
                    alertService.ShowAlert("Bad request.");
                }
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                alertService.ShowAlert(ex.Message);
            }

            return null;
        }

        public async Task<IEnumerable<MembershipContributionReadDto>> GetAllMembershipsAsync()
        {
            var httpResponse = await httpClient.GetAsync($"{MembershipUri}");
            try
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    var stringContent = await httpResponse.Content.ReadAsStringAsync();
                    var membershipsRead = JsonConvert.DeserializeObject<IEnumerable<MembershipContributionReadDto>>(stringContent);
                    if (membershipsRead == null || !membershipsRead.Any())
                    {
                        alertService.ShowAlert("No member found");
                    }
                    return membershipsRead;
                }
                if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                {
                    alertService.ShowAlert("Bad request.");
                }
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                alertService.ShowAlert(ex.Message);
            }

            return null;
        }
    }
}