﻿using GigHub.Models;
using GigHub.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext applicationDbContext;

        public HomeController()
        {
            applicationDbContext = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var upcomingGigs = applicationDbContext
                .Gigs.Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(predicate: g => g.DateTime > DateTime.Now);

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs"
            };

            return View("Gigs", viewModel);
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
    }
}