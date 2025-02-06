using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using ChatbotApi.Models;

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
            var response = await _httpClient.GetStringAsync(GitHubApiUrl);
            var repositories = JsonSerializer.Deserialize<List<GitHubRepository>>(response);

            if (repositories == null) return new List<object>();

            var csharpRepos = repositories
                .Where(r => r.Language != null && r.Language.Equals("C#", StringComparison.OrdinalIgnoreCase)) // Garante que seja C#
                .OrderBy(r => r.CreatedAt)  // Ordena do mais antigo para o mais novo
                .Take(5)  // Pega apenas os 5 mais antigos
                .Select(r => new
                {
                    Name = r.FullName,
                    Description = r.Description ?? "Sem descrição",
                    Url = r.Url,
                    AvatarUrl = BlipAvatarUrl
                })
                .ToList<object>();

            return csharpRepos;
        }
    }
}
