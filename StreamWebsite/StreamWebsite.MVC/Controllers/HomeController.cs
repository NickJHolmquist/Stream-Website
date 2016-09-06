using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StreamWebsite.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StreamWebsite.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var ViewModels = new MainPageViewModel();
            ViewModels.StreamData = FollowerCount();
            ViewModels.UserData = UserFetcher();
            ViewModels.HighlightData = Highlight();

            return View(ViewModels);
        }

        [HttpPost]
        public ActionResult UserProcessor(IEnumerable<UsersViewModel> users)
        {
            return Json(users);
        }

        [HttpGet]
        public IEnumerable<UsersViewModel> UserFetcher()
        {
            var users = new List<UsersViewModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.twitch.tv/kraken/channels/xkillzx/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync("follows").Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;
                    dynamic blah = JsonConvert.DeserializeObject(responseString);
                    var oneDeep = blah.follows;

                    foreach (var item in oneDeep)
                    {
                        var tempUser = new UsersViewModel();
                        tempUser._id = item.user._id;
                        tempUser.name = item.user.name;
                        users.Add(tempUser);
                    }
                }
            }

            return users;
        }

        [HttpGet]
        public StreamViewModel FollowerCount()
        {
            var totalFollowers = new StreamViewModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.twitch.tv/kraken/channels/xkillzx/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync("follows").Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;
                    dynamic blah = JsonConvert.DeserializeObject(responseString);
                    totalFollowers.TotalFollowers = blah._total;
                }
            }

            return totalFollowers;
        }

        [HttpGet]
        public HighlightViewModel Highlight()
        {
            var highlight = new HighlightViewModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.twitch.tv/kraken/channels/xkillzx/videos");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync("videos").Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;
                    dynamic blah = JsonConvert.DeserializeObject(responseString);
                    var videos = blah.videos;
                    foreach (var item in videos)
                    {
                        if (item.broadcast_type == "highlight")
                        {
                            highlight.VideoUrl = item.embed;
                            break;
                        }
                    }
                }
            }

            return highlight;
        }

        private void FollowersOfTheDay()
        {
            int followerCount = 0;
            var lastFollower = UserFetcher().Last();

        }
        
        [HttpPost]
        public ActionResult UserSender()
        {
            return View("UsersView");
        }

        public ActionResult TwitchView()
        {
            return PartialView("TwitchView");
        }
    }
}