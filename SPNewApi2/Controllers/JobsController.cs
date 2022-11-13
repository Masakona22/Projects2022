using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPNewApi2.DTO;
using SPNewApi2.Models;
using System.Security.Claims;

namespace SPNewApi2.Controllers
{
    //enable cors
    [EnableCors("appCors")]

    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        //for database connection
        private readonly FinalPlugDBContext _context;

        private readonly IConfiguration _configuration;

        //private readonly UserManager<ApplicationUser> _userManager;
        public JobsController(FinalPlugDBContext _context, IConfiguration _configuration)
        {
            this._context = _context;
            this._configuration = _configuration;
        }


        //create of booking by mechant
        [HttpPost]
        [Route("addjobs")]
        public async Task<IActionResult> Createclientjob([FromBody] JobTake book)
        {
            try
            {

                int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("merchID"));

                //var newbook = _context.BookingAddBeta.Where(u => u.BookId == book.BookId ).FirstOrDefault();
                //if (newbook != null)
                //{
                //    return BadRequest("Booking already existed");
                //}
                if (book == null)
                {
                    return BadRequest("You cant log job empty");
                }

                if (userID == null || userID <= 0)
                {
                    return BadRequest("YOu are not log in");
                }

                var newbook = new Job
                {
                    MerchId = userID,
                    UserId = book.UserId,

                     BookId = book.BookId,
                    JobStatus = "Active"

                };

                _context.Jobs.Add(newbook);
                await _context.SaveChangesAsync();
                return Ok(new { message = "success added a job" });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //Get all the jobs
        [HttpGet]
        [Route("getjobnbyclient")]
        public async Task<IActionResult> getjobss()
        {
            int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("UserID"));
            //  var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userID == null || userID <= 0)
            {
                return BadRequest("Please log in !! Cant job");
            }
            List<Job> listuser = _context.Jobs.Where(t => t.UserId == userID).ToList();
            if (listuser != null)
            {
                return Ok(listuser);
            }
            return BadRequest("They are no jobs in database");
        }

        //get job for specific client by the book id 
        [HttpGet]
        [Route("getjob/{id}")]
        public async Task<IActionResult> getjobt(int id)
        {
            int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("UserID"));
            if (userID == null || userID <= 0)
            {
                return BadRequest("Please log in !! Cant view jobs");
            }
            List<Job> listuser = _context.Jobs.Where(t => t.UserId == userID && t.JobId == id).ToList();
            if (listuser != null)
            {
                return Ok(listuser);
            }
            return BadRequest("They are no jobs in database");

            return Ok();
        }


        //Get all quotations
        [HttpGet]
        [Route("getalljobs")]
        public async Task<IActionResult> getalljobssall()
        {

            try
            {
                List<Job> listuser = _context.Jobs.ToList();
                if (listuser != null)
                {
                    return Ok(listuser);
                }
                return BadRequest("They are no jobs in database");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //Get all the jobs for specefic merchant 
        [HttpGet]
        [Route("getjobnbymerchant")]
        public async Task<IActionResult> getjobssMerchant()
        {
            int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("merchID"));
            //  var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userID == null || userID <= 0)
            {
                return BadRequest("Please log in !! Cant view jobs");
            }
            List<Job> listuser = _context.Jobs.Where(t => t.MerchId == userID && t.JobStatus=="Done ").ToList();
            if (listuser != null)
            {
                return Ok(listuser);
            }
            return BadRequest("They are no jobs in database for this merchant");
        }

        //Confirm of job Merchant that is done
        [HttpPut]
        [Route(("jobconfirm/{id}"))]
        public async Task<IActionResult> jobconfirm([FromBody] Jobconfirm combook, int id)
        {
            int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("merchID"));

            //  var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (combook == null || combook.JobId == null || combook.JobId <= 0)
            {
                return BadRequest("you cant edit ");
            }

            if (userID == null || userID <= 0)
            {
                return BadRequest("Please log in !! Cant job");
            }

            var dbbook = _context.Jobs.Where(t => t.JobId == combook.JobId && t.MerchId == userID && t.UserId == id).FirstOrDefault();
            if (dbbook == null)
            {
                return BadRequest("Cannot update nothing");
            }
            dbbook.JobStatus = combook.JobStatus;

            _context.Entry(dbbook).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            return Ok("Updated the status of job");
        }

        [HttpGet]
        [Route("getjobnbymerchant11")]
        public async Task<IActionResult> getmerchantjobs()
        {
            int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("merchID"));
            //  var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userID == null || userID <= 0)
            {
                return BadRequest("Please log in !! Cannot view jobs");
            }
            List<Job> listuser = _context.Jobs.Where(t => t.MerchId == userID && t.JobStatus == "Active").ToList();
            if (listuser != null)
            {
                return Ok(listuser);
            }
            return BadRequest("They are no jobs in database");
        }


    }
}
