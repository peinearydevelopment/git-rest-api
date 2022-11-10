namespace PinaryDevelopment.Git.Server.Apis.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Contracts;
    using static Contracts.GitResponse;

    [ApiController]
    [Route("api/[controller]")]
    public class RepositoriesController : ControllerBase
    {
        private readonly IRepositoriesService _repositoriesService;

        public RepositoriesController(IRepositoriesService repositoriesService)
        {
            _repositoriesService = repositoriesService;
        }

        [HttpGet("{clientId}")]
        public IActionResult GetRepository(Guid clientId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        [HttpPost("{clientId}")]
        public IActionResult CreateRepository(Guid clientId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var gitResponse = _repositoriesService.CreateRepository(clientId, cancellationToken);

            return gitResponse switch
            {
                GitSuccessResponse
                    => CreatedAtRoute(routeName: string.Empty, routeValues: new { clientId }, value: new { clientId, gitResponse.Message }),
                GitErrorResponse
                    => BadRequest(gitResponse),
                _
                    => StatusCode(500)
            };
        }
    }
}