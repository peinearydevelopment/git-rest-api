namespace PinaryDevelopment.Git.Server.Tests.Systems
{
    using Ductus.FluentDocker.Services;
    using Ductus.FluentDocker.Builders;

    public class Setup : IDisposable
    {
        private bool disposed = false;

        private static ICompositeService? CompositeService { get; set; }

        public static string BaseServiceUrl => "http://localhost:4567";
        public static string BaseApiUrl => $"{BaseServiceUrl}/api";

        public static void Initialize()
        {
            if (CompositeService == null)
            {
                CompositeService = new Builder()
                    .UseContainer()
                    .UseCompose()
                    .FromFile(@"../../docker-compose.yml")
                    .RemoveOrphans()
                    .ForceBuild()
                    .WaitForHttp("git-server", $"{BaseServiceUrl}/status/")
                    .Build()
                    .Start();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    CompositeService?.Stop();
                    CompositeService?.Dispose();
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
