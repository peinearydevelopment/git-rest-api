namespace PinaryDevelopment.Git.Server.Tests.Systems.Repositories
{
    using System.Net;
    using System.Text.Json;
    using PinaryDevelopment.Git.Server.Tests.Systems;

    internal class CreatedResponse
    {
        internal string? ClientId { get; set; }
        internal string? Message { get; set; }
    }

    [TestClass]
    [TestCategory("Systems")]
    public class Create : BaseTestClass
    {
        private readonly HttpResponseMessage Response;

        public Create()
        {
            Response = HttpClient.PostAsync($"/repositories/{ClientId}", new StringContent(string.Empty))
                                 .ConfigureAwait(false)
                                 .GetAwaiter()
                                 .GetResult();
        }

        [TestMethod("1. Call is successful")]
        public void CallSucceeds()
        {
            try
            {
                Response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                Assert.Fail(ex.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod("2. Http response code is 'created'.")]
        public void ResponseCodeIsCreated()
        {
            Assert.AreEqual(HttpStatusCode.Created, Response.StatusCode);
        }

        [TestMethod("3. Response contains expected location header.")]
        public void ContainsLocationHeader()
        {
            Assert.AreEqual($"{Setup.BaseApiUrl}/repositories/{ClientId}", Response.Headers.Location?.ToString());
        }

        [TestMethod("4. Response contains expected content.")]
        public async Task ContainsContent()
        {
            var content = JsonSerializer.Deserialize<CreatedResponse>(await Response.Content.ReadAsStringAsync());
            Assert.AreEqual(ClientId, content?.ClientId);
            Assert.AreEqual($"Initialized empty Git repository in /repos/{ClientId}/.git/", content?.Message);
        }
    }
}
