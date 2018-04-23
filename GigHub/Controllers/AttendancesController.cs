using GigHub.DTO;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(/*[FromBogy] int GigId*/AttendanceDTO attendanceDTO)
        {
            var userId = User.Identity.GetUserId();
            var exists = _context.Attendances
                .Any(a => a.AttendeeId == userId && a.GigId == attendanceDTO.GigId);

            if (exists)
                return BadRequest("The attendance is already exist");


            //var attendance = new Attendance
            //{
            //    GigId = gigId,
            //    AttendeeId = userId
            //};

            var attendance = new Attendance
            {
                GigId = attendanceDTO.GigId,
                AttendeeId = userId
            };

            _context.Attendances.Add(attendance);

            _context.SaveChanges();

            return Ok();
        }
    }
}
