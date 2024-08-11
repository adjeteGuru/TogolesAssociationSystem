namespace TogoleseAssociationSystem.IntegrationTests.Helpers
{
    public class WebRequestHelper
    {
        public async Task<HttpResponseMessage> GetAsync(Uri uri)
        {
            var httpClient = new HttpClient();
            return await httpClient.GetAsync(uri);
        }

        public async Task<HttpResponseMessage> PostAsync(Uri uri, HttpContent content)
        {
            var httpClient = new HttpClient();
            return await httpClient.PostAsync(uri, content);
        }
    }
}
