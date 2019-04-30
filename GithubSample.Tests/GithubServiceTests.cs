using GithubSample.Models;
using GithubSample.Services;
using Serilog;
using Xunit;

namespace GithubSample.Tests
{
    public class GithubServiceTests
    {
        private readonly GithubService _githubService;

        public GithubServiceTests()
        {
            _githubService = new GithubService();
        }

        // need more test

        [Fact]
        public void ReturnUserIsNotNull()
        {
            var result = _githubService.GetUserAsync("davidfowl");

            Assert.NotNull(result);
        }

        [Fact]
        public void ReturnRepositoriesAreNotNull()
        {
            User userObj = _githubService.GetUserAsync("davidfowl").Result;

            var result = _githubService.GetPopularRepositoriesAsync("davidfowl", userObj);

            Assert.NotNull(result);
        }
    }
}
