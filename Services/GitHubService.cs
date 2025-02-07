using ChatbotApi.Models;
using System.Text.Json;

namespace ChatbotApi.Services
{
    public class GitHubService
    {
        private readonly HttpClient _httpClient;
        private const string GitHubApiUrl = "https://api.github.com/orgs/takenet/repos?per_page=100";
        private const string BlipAvatarUrl = "https://avatars.githubusercontent.com/u/4369522?v=4";

        public GitHubService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0");
        }

        public async Task<List<object>> GetOldestCSharpRepositoriesAsync()
        {
            string response = await _httpClient.GetStringAsync(GitHubApiUrl);
            List<GitHubRepository>? repositories = JsonSerializer.Deserialize<List<GitHubRepository>>(response);

            if (repositories == null)
            {
                return new List<object>();
            }

            List<object> csharpRepos = repositories
                .Where(r => r.Language != null && r.Language.Equals("C#", StringComparison.OrdinalIgnoreCase)) // Garante que seja C#
                .OrderBy(r => r.CreatedAt)
                .Take(5)
                .Select(r => new
                {
                    Name = r.FullName,
                    Description = r.Description ?? "Sem descrição",
                    AvatarUrl = BlipAvatarUrl
                })
                .ToList<object>();

            return csharpRepos;
        }
    }
}
