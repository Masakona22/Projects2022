using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPNewApi2.DTO;
using SPNewApi2.Models;
using System.Security.Claims;

namespace SPNewApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //enable cors
    [EnableCors("appCors")]
    public class ReviewsController : ControllerBase
    {
        //for database connection
        private readonly FinalPlugDBContext _context;

        private readonly IConfiguration _configuration;

        //private readonly UserManager<ApplicationUser> _userManager;
        public ReviewsController(FinalPlugDBContext _context, IConfiguration _configuration)
        {
            this._context = _context;
            this._configuration = _configuration;
        }

        //create of booking by mechant
        //[HttpPost]
        //[Route("addreview")]
        //public async Task<IActionResult> CreateclientReview([FromBody] ReviewTake book)
        //{
        //    try
        //    {

        //        int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("userID"));

        //        //var newbook = _context.BookingAddBeta.Where(u => u.BookId == book.BookId ).FirstOrDefault();
        //        //if (newbook != null)
        //        //{
        //        //    return BadRequest("Booking already existed");
        //        //}
        //        if (book == null)
        //        {
        //            return BadRequest("You cant review  empty");
        //        }

        //        if (userID == null || userID <= 0)
        //        {
        //            return BadRequest("YOu are not log in");
        //        }

        //        var newbook = new Review
        //        {
        //            UserId = userID,
        //            MerchId = book.MerchId,

        //            JobId = book.JobId,
        //            ReviewRating = book.ReviewRating,
        //            ReviewMessage = book.ReviewMessage

        //        };

        //        _context.Reviews.Add(newbook);
        //        await _context.SaveChangesAsync();
        //        return Ok(new { message = "success added a rating" });

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        //create of booking by mechant
        [HttpPost]
        [Route("addreview")]
        public async Task<IActionResult> CreateclientReview([FromBody] ReviewTake book)
        {
            try
            {

                int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("UserID"));


                if (book == null)
                {
                    return BadRequest("You cant review  empty");
                }

                if (userID == null || userID <= 0)
                {
                    return BadRequest("YOu are not log in");
                }

                var newbook = new Review
                {
                    UserId = userID,
                    MerchId = book.MerchId,

                   // JobId = book.JobId,
                    ReviewRating = book.ReviewRating,
                    ReviewMessage = book.ReviewMessage

                };

                _context.Reviews.Add(newbook);
                await _context.SaveChangesAsync();
                return Ok(new { message = "success added a rating" });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //get all the reviews 
        [HttpGet]
        [Route("getallreviews")]
        public async Task<IActionResult> getallreviews()
        {

            try
            {
                List<Review> listuser = _context.Reviews.ToList();
                if (listuser != null)
                {
                    return Ok(listuser);
                }
                return BadRequest("They are no Quotations in database");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //get the reviews by client for specific merchant 
        [HttpGet]
        [Route("getreview/{id}")]
        public async Task<IActionResult> getmerchantreview(int id)
        {
            try
            {
                int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("merchID"));



                if (userID == null || userID <= 0)
                {
                    return BadRequest("Please log in !! Cant see the reviews");
                }
                if (id == null || id <= 0)
                {
                    return BadRequest("This Review is not available");
                }

                var Reviewlist = _context.Reviews.Select(t => new
                {
                    t.ReviewId,
                    t.UserId,
                    t.MerchId,
                    t.ReviewRating,
                    t.ReviewMessage,

                }).Where(t => t.MerchId == id).ToList();

                return Ok(Reviewlist);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //Get all the quotations
        [HttpGet]
        [Route("getreviewclient/{id}")]
        public async Task<IActionResult> getreviews(int id)
        {
            int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("UserID"));
            //  var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userID == null || userID <= 0)
            {
                return BadRequest("Please log in !! Cant view yhe reviews");
            }
            List<Review> listuser = _context.Reviews.Where(t => t.MerchId == id).ToList();
            if (listuser != null)
            {
                return Ok(listuser);
            }
            return BadRequest("They are no Reviews in database");
        }

    }
}
