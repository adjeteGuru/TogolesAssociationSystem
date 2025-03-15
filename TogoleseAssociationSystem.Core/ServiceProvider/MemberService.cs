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
        private static string ClaimUri = "api/claim";
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

       public async Task<IEnumerable<MemberRead>> GetMembersAsync(int currentPage, int ItemsPerPage, string? filter = null)
        {
            var httpResponse = await httpClient.GetAsync($"{RequestUri}?filter={filter}&currentPage={currentPage}&itemsPerPage={ItemsPerPage}");
            try
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    var stringContent = await httpResponse.Content.ReadAsStringAsync();
                    var paginatedMembers = JsonConvert.DeserializeObject<IEnumerable<MemberRead>>(stringContent);
                    
                    if (paginatedMembers == null || !paginatedMembers.Any())
                    {
                        return [];
                    }
                    return paginatedMembers;
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

        public async Task<ClaimReadDto> CreateClaimAsync(ClaimToAdd claimToAdd)
        {
          var response = await httpClient.SendAsync(new HttpRequestMessage
          {
              Method = HttpMethod.Post,
              RequestUri = new Uri(httpClient.BaseAddress + ClaimUri),
              Content = JsonContent.Create(claimToAdd)
          });

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var stringContent = await response.Content.ReadAsStringAsync();
                    var claim = JsonConvert.DeserializeObject<ClaimReadDto>(stringContent);
                    if (claim == null)
                    {
                        alertService.ShowAlert("No claim found");
                    }
                    return claim;
                }
                if (response.StatusCode == HttpStatusCode.BadRequest)
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

        public async Task<ClaimReadDto> GetClaimByIdAsync(Guid id)
        {
            var httpResponse = await httpClient.GetAsync($"{ClaimUri}/{id}");
            try
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    var stringContent = await httpResponse.Content.ReadAsStringAsync();
                    var claimRead = JsonConvert.DeserializeObject<ClaimReadDto>(stringContent);
                    if (claimRead == null)
                    {
                        alertService.ShowAlert("No claim found");
                    }
                    return claimRead;
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

        public async Task UpdateClaimAsync(ClaimToUpdate claimToAdd)
        {
            var json = JsonConvert.SerializeObject(claimToAdd);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var newRequestUri = $"{ClaimUri}/{claimToAdd.Id}";

            var httpResponse = await httpClient.PutAsync(newRequestUri, content);

            try
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    var updatedClaim = JsonConvert.DeserializeObject<ClaimReadDto>(responseContent);

                    if (updatedClaim == null)
                    {
                        alertService.ShowAlert("No claim found");
                    }
                }
                else if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                {
                    alertService.ShowAlert("Bad request.");
                }
                else
                {
                    var errorContent = await httpResponse.Content.ReadAsStringAsync();
                    var failureResponse = JsonConvert.DeserializeObject<FailureResponseModel>(errorContent)?.Detail;

                    if (failureResponse != null)
                    {
                        alertService.ShowAlert(failureResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                alertService.ShowAlert(ex.Message);
            }
        }

        public async Task<IEnumerable<ClaimReadDto>> GetClaimsByMemberIdAsync(Guid id)
        {
            var httpResponse = await httpClient.GetAsync($"{ClaimUri}/member/{id}");
            try
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    var stringContent = await httpResponse.Content.ReadAsStringAsync();
                    var claimsRead = JsonConvert.DeserializeObject<IEnumerable<ClaimReadDto>>(stringContent);
                    if (claimsRead == null || !claimsRead.Any())
                    {
                        alertService.ShowAlert("No claim found");
                        return [];
                    }
                    return claimsRead;
                }
                if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                {
                    alertService.ShowAlert("Bad request.");
                    return [];
                }
            }
            catch (Exception ex)
            {
                alertService.ShowAlert(ex.Message);
            }

            return [];
        }
    }
}