using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StreamWebsite.MVC.ViewModels
{
    public class MainPageViewModel
    {
        public StreamViewModel StreamData { get; set; }
        public IEnumerable<UsersViewModel> UserData { get; set; }
        public HighlightViewModel HighlightData { get; set; }
    }
}