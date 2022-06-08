using Microsoft.AspNetCore.Mvc;
using musicStoreMVC.Models;
using System.Diagnostics;

namespace musicStoreMVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly MusicStoreEntities _storeDB;
        public HomeController(MusicStoreEntities storeDB)
        {
            _storeDB = storeDB;
        }
        public ActionResult Index()
        {
            // Get most popular albums
            var albums = GetTopSellingAlbums(5);

            return View(albums);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private List<Album> GetTopSellingAlbums(int count)
        {
            // Group the order details by album and return
            // the albums with the highest count
            return _storeDB.Albums
                .OrderByDescending(a => a.OrderDetails.Count())
                .Take(count)
                .ToList();
        }


    }
}