using System.Threading.Tasks;
using GithubSample.Models;

namespace GithubSample.Services
{
    /// <summary>
    /// Interface for the GitHub service
    /// </summary>
    public interface IGithubService
    {
        /// <summary>
        /// Get github user for respective username
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>User object</returns>
        Task<User> GetUserAsync(string username);

        /// <summary>
        /// Get github popular repositories for respective user
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="userObject">The user object.</param>
        /// <returns>User object with popular repositories</returns>
        Task<User> GetPopularRepositoriesAsync(string username, User userObject);
    }
}