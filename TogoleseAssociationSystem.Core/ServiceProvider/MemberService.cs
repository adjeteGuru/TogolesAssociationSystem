using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using TogoleseSolidarity.Core.DTOs;
using TogoleseSolidarity.Core.Models;
using TogoleseSolidarity.Core.ServiceProvider.Interfaces;

namespace TogoleseSolidarity.Core.ServiceProvider;

public class MemberService : IMemberService
{
    private readonly HttpClient httpClient;
    private readonly IAlertService alertService;
    private readonly ILogger<MemberService> logger;
    private static string RequestUri = "api/member";
    private static string MembershipUri = "api/membership";
    private static string ClaimUri = "api/claim";

    public MemberService(HttpClient httpClient, IAlertService alertService, ILogger<MemberService> logger)
    {
        this.httpClient = httpClient;
        this.alertService = alertService;
        this.logger = logger;
    }

    public async Task<MemberRead> GetMemberByIdAsync(Guid id)
    {
        logger.LogInformation("Request received to get member by ID: {Id}", id);
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
                logger.LogInformation("Successfully retrieved member by ID: {Id}", memberRead.Id.ToString());
                return memberRead;
            }
            if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                alertService.ShowAlert("Bad request.");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting member by ID: {Id}", id);
            throw new Exception(ex.Message);
        }

        return null;
    }

    public async Task<IEnumerable<MemberRead>> GetMembersAsync(int currentPage, int ItemsPerPage, string? filter = null)
    {
        logger.LogInformation("Request received to get members with filter: {Filter}, currentPage: {CurrentPage}, itemsPerPage: {ItemsPerPage}", filter, currentPage, ItemsPerPage);
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
                logger.LogInformation("Successfully retrieved members");
                return paginatedMembers;
            }
            if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                alertService.ShowAlert("Bad request.");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting members with filter: {Filter}, currentPage: {CurrentPage}, itemsPerPage: {ItemsPerPage}", filter, currentPage, ItemsPerPage);
            throw new Exception(ex.Message);
        }

        return null;
    }

    public async Task<MemberRead> CreateMemberAsync(MemberToAdd memberToAdd)
    {
        logger.LogInformation("Request received to create member: {MemberToAdd}", memberToAdd);
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
                logger.LogInformation("Successfully created member with ID: {Id}", member.Id.ToString());
                return member;
            }
            if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                alertService.ShowAlert("Bad request.");
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error creating member: {MemberToAdd}", memberToAdd);
            alertService.ShowAlert(e.Message);
        }

        return null;
    }

    public async Task<MembershipContributionReadDto> CreateMembershipAsync(MembershipContributionToAdd contributionToAdd)
    {
        logger.LogInformation("Request received to create membership contribution: {ContributionToAdd}", contributionToAdd);
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
                logger.LogInformation("Successfully created membership contribution with ID: {Id}", membership.MemberId.ToString());
                return membership;
            }
            if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                alertService.ShowAlert("Bad request.");
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error creating membership contribution: {ContributionToAdd}", contributionToAdd);
            alertService.ShowAlert(e.Message);
        }

        return null;
    }

    public async Task<MemberRead> UpdateMemberDetails(MemberUpdateDto member)
    {
        logger.LogInformation("Request received to update member details: {Member}", member);
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
                logger.LogInformation("Successfully updated member with ID: {Id}", memberUpdated.Id.ToString());
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
                alertService.ShowAlert(failureResponse);
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error updating member details: {Member}", member);
            alertService.ShowAlert(e.Message);
        }

        return null;
    }

    public async Task<IEnumerable<MemberRead>> GetAllExistingMembersAsync()
    {
        logger.LogInformation("Request received to get all existing members");
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
                logger.LogInformation("Successfully retrieved all existing members.");
                return membersRead;
            }
            if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                alertService.ShowAlert("Bad request.");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting all existing members");
            alertService.ShowAlert(ex.Message);
        }

        return null;
    }

    public async Task<IEnumerable<MembershipContributionReadDto>> GetAllMembershipsAsync()
    {
        logger.LogInformation("Request received to get all memberships");
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
                logger.LogInformation("Successfully retrieved memberships.");
                return membershipsRead;
            }
            if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                alertService.ShowAlert("Bad request.");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting all memberships");
            alertService.ShowAlert(ex.Message);
        }

        return null;
    }

    public async Task<ClaimReadDto> CreateClaimAsync(ClaimToAdd claimToAdd)
    {
        logger.LogInformation("Request received to create claim: {ClaimToAdd}", claimToAdd);
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
                logger.LogInformation("Successfully created claim with ID: {Id}", claim.MemberId.ToString());
                return claim;
            }
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                alertService.ShowAlert("Bad request.");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating claim: {ClaimToAdd}", claimToAdd);
            throw new Exception(ex.Message);
        }

        return null;
    }

    public async Task<ClaimReadDto> GetClaimByIdAsync(Guid id)
    {
        logger.LogInformation("Request received to get claim by ID: {Id}", id);
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
                logger.LogInformation("Successfully retrieved member's claim with ID: {Id}", claimRead.MemberId.ToString());
                return claimRead;
            }
            if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                alertService.ShowAlert("Bad request.");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting claim by ID: {Id}", id);
            throw new Exception(ex.Message);
        }

        return null;
    }

    public async Task UpdateClaimAsync(ClaimToUpdate claimToAdd)
    {
        logger.LogInformation("Request received to update claim: {ClaimToAdd}", claimToAdd);
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

                logger.LogInformation("Successfully updated member's claim with ID: {Id}", updatedClaim.MemberId.ToString());
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
            logger.LogError(ex, "Error updating claim: {ClaimToAdd}", claimToAdd);
            alertService.ShowAlert(ex.Message);
        }
    }

    public async Task<IEnumerable<ClaimReadDto>> GetClaimsByMemberIdAsync(Guid id)
    {
        logger.LogInformation("Request received to get claims by member ID: {Id}", id);
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
                logger.LogInformation("Successfully retrieved member's claims with ID: {Id}", id.ToString());
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
            logger.LogError(ex, "Error getting claims by member ID: {Id}", id);
            alertService.ShowAlert(ex.Message);
        }

        return [];
    }
}
