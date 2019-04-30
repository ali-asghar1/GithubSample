using GithubSample.Models;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GithubSample.Services
{
    /// <summary>
    /// Github service to get user and popular repositories
    /// </summary>
    public class GithubService : IGithubService
    {

        private readonly string getUserEndpoint = "https://api.github.com/users/";
        private readonly string repositoryString = "/repos";

        /// <inheritdoc/>
        public async Task<User> GetUserAsync(string username)
        {
            if (username == null || username == string.Empty)
            {
                return null;
            }
            try
            {
                Log.Information($"Starting {nameof(GetUserAsync)}.");

                using (var webClient = new WebClient())
                {
                    webClient.Headers.Add("User-Agent", "request");
                    var userJSON = await webClient.DownloadStringTaskAsync($"{getUserEndpoint}{username}").ConfigureAwait(false);
                    User userInfo = JsonConvert.DeserializeObject<User>(userJSON);

                    Log.Information($"{nameof(GetUserAsync)} completed.");

                    return userInfo;
                }

            }
            catch (Exception e)
            {
                Log.Error(e, $"{nameof(GetUserAsync)} - error occured.");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<User> GetPopularRepositoriesAsync(string username, User userObject)
        {
            if (username == null || username == string.Empty)
            {
                return null;
            }

            try
            {
                Log.Information($"Starting {nameof(GetPopularRepositoriesAsync)}.");

                using (var webClient = new WebClient())
                {
                    webClient.Headers.Add("User-Agent", "request");
                    var repoJSON = await webClient.DownloadStringTaskAsync($"{getUserEndpoint}{username}{repositoryString}").ConfigureAwait(false);
                    IList<Repository> repositories = JsonConvert.DeserializeObject<List<Repository>>(repoJSON);
                    userObject.Repositories = repositories.OrderByDescending(r => r.Stargazers_count).Take(5).ToList();

                    Log.Information($"{nameof(GetPopularRepositoriesAsync)} completed.");

                    return userObject;
                }

            }
            catch (Exception e)
            {
                Log.Error(e, $"{nameof(GetPopularRepositoriesAsync)} - error occured.");
                throw;
            }

           

        }
    }
}
