﻿using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public GigsController()
        {
            applicationDbContext = new ApplicationDbContext();
        }
        // GET: Gigs
        [Authorize]
        public ActionResult Create()
        {

            var viewModel = new GigFormViewModel
            {
                Genres = applicationDbContext.Genres.ToList()
            };
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            //var artistId = User.Identity.GetUserId();
            //var artist = applicationDbContext.Users.Single(u => u.Id == artistId);
            //var genre = applicationDbContext.Genres.Single(g => g.Id == viewModel.Genre);
            if (!ModelState.IsValid)
            {
                viewModel.Genres = applicationDbContext.Genres.ToList();
                return View("Create", viewModel);
            }

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            applicationDbContext.Gigs.Add(gig);
            applicationDbContext.SaveChanges();

            return RedirectToAction("Index", "Home");

        }
    }
}