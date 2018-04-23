using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers
{
    [Authorize]
    public class FollowingController : ApiController
    {
        private ApplicationDbContext _context;

        public FollowingController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult Folow(FollowingDTO dto)
        {
            var userId = User.Identity.GetUserId();
            var exist = _context.Followings.Any(f => f.FolloweeId == userId && f.FolloweeId == dto.FolloweeId);

            if (exist)
                return BadRequest("Following already exists");

            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };
            _context.Followings.Add(following);
            _context.SaveChanges();

            return Ok();
        }
    }
}
