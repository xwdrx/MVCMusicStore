using Microsoft.AspNetCore.Mvc;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using musicStoreMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace musicStoreMVC.Controllers
{
    public class StoreController:Controller
    {
        private readonly MusicStoreEntities _storeDB;
       // MusicStoreEntities storeDB = new MusicStoreEntities();
        public StoreController(MusicStoreEntities storeDB)
        {
            _storeDB = storeDB;
        }
        public ActionResult Index()
        {
            var genres = _storeDB.Genres.ToList();
            return View(genres);
        }
        //
        // GET: /Store/Browse
        /* public string Browse(string genre)
         {
             string message = HttpUtility.HtmlEncode("Store.Browse, Genre = "+ genre);

             return message;
         }
        */
        //
        // GET: /Store/Details
        public ActionResult Details(int id)
        {
            var album = new Album { Title = "Album " + id };
            return View(album);
        }
        public ActionResult Browse(string genre)
        {
            var genreModel = _storeDB.Genres.Include("Albums").FirstOrDefault(g => g.Name == genre);
            return View(genreModel);
        }
        
    

}
}
