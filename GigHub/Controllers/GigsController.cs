using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
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

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigs = applicationDbContext
                .Attendances.Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gig's I'm attening"
            };

            return View("Gigs", viewModel);
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