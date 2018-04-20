using GigHub.Models;
using GigHub.ViewModels;
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
        public ActionResult Create()
        {

            var viewModel = new GigFormViewModel
            {
                Genres = applicationDbContext.Genres.ToList()
            };
            return View(viewModel);
        }
    }
}