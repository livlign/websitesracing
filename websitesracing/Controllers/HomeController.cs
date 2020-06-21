using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace websitesracing.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public class Website
        {
            public int CurrentWidth { get; set; }
            public string Name { get; set; }
            public int DataLength { get; set; }
            public long LoadingTime { get; set; }
            public string Result { get; set; }
        }

        [HttpPost]
        public async Task<JsonResult> LoadWebsites(List<Website> websites)
        {
            List<Task> tasks = new List<Task>();
            foreach (var site in websites)
            {
                tasks.Add(Task.Run(() => DownloadWebsite(site)));
            }

            await Task.WhenAll(tasks);

            return Json(websites);
        }

        private void DownloadWebsite(Website site)
        {
            var client = new WebClient();
            var watch = Stopwatch.StartNew();
            var result = client.DownloadString(site.Name);
            watch.Stop();
            site.DataLength = result.Length;
            site.LoadingTime = watch.ElapsedMilliseconds;

            site.Result = site.LoadingTime.ToString("N0") + " ms - " + site.DataLength.ToString("N0") + " Chars";
        }
    }
}