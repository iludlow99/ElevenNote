using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.Web.Controllers
{
    //The first word in our controller will be in our path. Our path will be localhost:xxxx/Note
    [Authorize]
    public class NoteController : Controller
    {
    //ActionResult is a return type. It basically allows us to return a View() method. That View() method will return a view that corresponds to the NoteController
        // GET: Note
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);
            var model = service.GetNotes();

            return View(model);
        }

    //When running this app we can go to localhost:xxxx/Note/Index. Notice the pattern her for the path. It is the name of the controller (without the word controller), then the name of the method, which is Index

        //GET
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            NoteService service = CreateService();

            service.CreateNote(model);

            return RedirectToAction("Index");
        }

        private NoteService CreateService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);
            return service;
        }
    }
}