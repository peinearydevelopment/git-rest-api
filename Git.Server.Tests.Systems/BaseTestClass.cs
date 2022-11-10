namespace PinaryDevelopment.Git.Server.Tests.Systems
{
    public class BaseTestClass
    {
        public Guid ClientId { get; private set; }
        public HttpClient HttpClient { get; private set; }

        public BaseTestClass()
        {
            Setup.Initialize();

            ClientId = Guid.NewGuid();
            HttpClient = new HttpClient
            {
                BaseAddress = new Uri(Setup.BaseApiUrl)
            };
        }
    }
}
