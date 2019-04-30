using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GithubSample.Models;
using GithubSample.Services;
using Serilog;

namespace GithubSample.Controllers
{
    public class HomeController : Controller
    {

        private readonly IGithubService _githubService;

        public HomeController(IGithubService githubService)
        {
            _githubService = githubService;
        }

        public async Task<IActionResult> Index(string username)
        {
            //more logging required while processing
            try
            {
                if (username == null || username == string.Empty)
                {
                    return View();
                }

                User userInfo = await _githubService.GetUserAsync(username).ConfigureAwait(false);
                userInfo = await _githubService.GetPopularRepositoriesAsync(username, userInfo).ConfigureAwait(false);
                return View(userInfo);
            }
            catch (Exception e)
            {
                Log.Error(e, $"{nameof(Index)} - error occured.");
                throw;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            //better exception handling require i.e status code and user frindly description
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
