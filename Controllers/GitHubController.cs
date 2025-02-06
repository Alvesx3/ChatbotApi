using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatbotApi.Services;

namespace ChatbotApi.Controllers
{
    [Route("api/github")]
    [ApiController]
    public class GitHubController : ControllerBase
    {
        private readonly GitHubService _gitHubService;

        public GitHubController(GitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        [HttpGet("repos")]
        public async Task<IActionResult> GetRepositories()
        {
            try
            {
                var repositories = await _gitHubService.GetOldestCSharpRepositoriesAsync();
                return Ok(repositories);
            }
            catch
            {
                return StatusCode(500, "Erro ao buscar os repositórios do GitHub.");
            }
        }
    }
}
