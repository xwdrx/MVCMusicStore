using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using musicStoreMVC.Models;
using System.Net;

namespace musicStoreMVC.Controllers
{
    public class StoreManagerController:Controller
    {
        private readonly MusicStoreEntities _storeDB;
        // MusicStoreEntities storeDB = new MusicStoreEntities();
        public StoreManagerController(MusicStoreEntities storeDB)
        {
            _storeDB = storeDB;
        }
        public ActionResult Index()
        {
            var albums = _storeDB.Albums.Include(a => a.Artist).Include(a => a.Genre);
            return View(albums.ToList());
        }
        public ViewResult Details(int id)
        {
            Album album = _storeDB.Albums.Find(id);
            return View(album);
        }
        // GET: /StoreManager/Create
        public ActionResult Create()
        {
            ViewBag.GenreId = new SelectList(_storeDB.Genres, "GenreId", "Name");
            ViewBag.ArtistId = new SelectList(_storeDB.Artist, "ArtistId", "Name");
            return View();
        }
        // POST: /StoreManager/Create
        [HttpPost]
        public ActionResult Create(Album album)
        {
            if (ModelState.IsValid)
            {
                _storeDB.Albums.Add(album);
                _storeDB.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenreId = new SelectList(_storeDB.Genres, "GenreId", "Name", album.GenreId);
            ViewBag.ArtistId = new SelectList(_storeDB.Artist, "ArtistId", "Name", album.ArtistId);
            return View(album);
        }

        // GET: /StoreManager/Edit/5
        [HttpPost]
        public ActionResult Edit(Album album)
        {
            if (ModelState.IsValid)
            {
                _storeDB.Entry(album).State = EntityState.Modified;
                _storeDB.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenreId = new SelectList(_storeDB.Genres, "GenreId", "Name", album.GenreId);
            ViewBag.ArtistId = new SelectList(_storeDB.Artist, "ArtistId", "Name", album.ArtistId);
            return View(album);
        }
        // GET: /StoreManager/Delete/5

        public ActionResult Delete(int id)
        {
            Album album = _storeDB.Albums.Find(id);
            return View(album);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = _storeDB.Albums.Find(id);
            _storeDB.Albums.Remove(album);
            _storeDB.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
