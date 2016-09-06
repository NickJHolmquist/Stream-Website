using Newtonsoft.Json;
using StreamWebsite.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace StreamWebsite.MVC.Controllers
{
    public class GamesController : Controller
    {
        // GET: Games
        public ActionResult Index()
        {
            return View(GamesFetcher());
        }

        public ActionResult BeatenGames()
        {
            var games = new List<string>();

            games.Add("Dark Souls 3");
            games.Add("Gears of War 4");
            games.Add("Skyrim");

            return View("BeatenGames", games);
        }

        [HttpGet]
        public IEnumerable<GamesViewModel> GamesFetcher()
        {
            var games = new List<GamesViewModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.twitch.tv/kraken/search/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync("games?q=star&type=suggest").Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;
                    dynamic blah = JsonConvert.DeserializeObject(responseString);
                    var oneDeep = blah.games;

                    foreach (var item in oneDeep)
                    {
                        var tempUser = new GamesViewModel();
                        tempUser.box = item.box.medium;
                        tempUser.name = item.name;
                        games.Add(tempUser);
                    }
                }
            }

            return games;
        }
    }
}