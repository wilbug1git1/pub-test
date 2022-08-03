using Octokit;

namespace BreachPathAPI.Services
{
    public class GitHubAdminService
    {

        public string RefreshRepositories(string org)
        {
            return "Organization Added";
        }

        public void AddNewConnection()
        {
            var gitHubClient = new GitHubClient(new ProductHeaderValue("BreachPath"));
        }
    }
}
