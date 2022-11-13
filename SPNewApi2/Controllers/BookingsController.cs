using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPNewApi2.DTO;
using SPNewApi2.Models;
using System.Security.Claims;

namespace SPNewApi2.Controllers
{



    [Route("api/[controller]")]
    [ApiController]


    //enable cors
    [EnableCors("appCors")]
    public class BookingsController : ControllerBase
    {

        //for database connection
        private readonly FinalPlugDBContext _context;

        private readonly IConfiguration _configuration;

        //private readonly UserManager<ApplicationUser> _userManager;
        public BookingsController(FinalPlugDBContext _context, IConfiguration _configuration)
        {
            this._context = _context;
            this._configuration = _configuration;
        }

        //create of booking by mechant
        [HttpPost]
        [Route("addbookings")]
        public async Task<IActionResult> Createclientbooking([FromBody] TakeBooking  book)
        {
            try
            {

                int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("userID"));

                //var newbook = _context.BookingAddBeta.Where(u => u.BookId == book.BookId ).FirstOrDefault();
                //if (newbook != null)
                //{
                //    return BadRequest("Booking already existed");
                //}
                if (book == null)
                {
                    return BadRequest("You cant book empty");
                }

                if (userID == null || userID <= 0)
                {
                    return BadRequest("YOu are not log in");
                }

                var newbook = new Booking
                {
                    UserId = userID,
                    MerchId = book.MerchId,
                    BookDate = book.BookDate,
                    BookTime = book.BookTime,
                    BookMessage = book.BookMessage,
                    BookStatus = "InActive"

                };

                _context.Bookings.Add(newbook);
                await _context.SaveChangesAsync();
                return Ok(new { message = "success added a booking" });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //Get all the bookings
        [HttpGet]
        [Route("getbookingsbyclient")]
        public async Task<IActionResult> getbookings()
        {
            int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("UserID"));

            //  var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userID == null || userID <= 0)
            {
                return BadRequest("Please log in !! Cant book");
            }
            List<Booking> listuser = _context.Bookings.Where(t => t.UserId == userID).ToList();
            if (listuser != null)
            {
                return Ok(listuser);
            }
            return BadRequest("They are no users in database");
        }



        //Get all Merchants
        [HttpGet]
        [Route("getallbookings")]
        public async Task<IActionResult> getallbookingsall()
        {

            try
            {
                List<Booking> listuser = _context.Bookings.ToList();
                if (listuser != null)
                {
                    return Ok(listuser);
                }
                return BadRequest("They are no bookings in database");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        //get the booking request for each client by merchant
        [HttpGet]
        [Route("bookingnot")]
        public async Task<IActionResult> getbookRequest()
        {

            try
            {
                int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("merchID"));



                if (userID == null || userID <= 0)
                {
                    return BadRequest("Please log in !! Cant book");
                }

                var quolist = _context.Bookings.Select(t => new
                {
                    t.BookId,
                    t.UserId,
                    t.MerchId,
                    t.BookStatus,
                    t.BookDate,
                    t.BookTime,
                    t.BookMessage,
                }).Where(t => t.MerchId == userID && t.BookStatus == "InActive").ToList();

                return Ok(quolist);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //get the booking request for each client by merchant
        [HttpGet]
        [Route("getbookingsrequest")]
        public async Task<IActionResult> getbookRequestallthem()
        {

            try
            {
                int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("merchID"));



                if (userID == null || userID <= 0)
                {
                    return BadRequest("Please log in !! Cant book");
                }

                var quolist = _context.Bookings.Select(t => new
                {
                    t.BookId,
                    t.UserId,
                    t.MerchId,
                    t.BookStatus,
                    t.BookDate,
                    t.BookTime
                }).Where(t => t.MerchId == userID).ToList();

                return Ok(quolist);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("booking")]
        public async Task<IActionResult> getActiveRequests()
        {

            try
            {
                int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("merchID"));



                if (userID == null || userID <= 0)
                {
                    return BadRequest("Please log in !! Cant book");
                }

                var quolist = _context.Bookings.Select(t => new
                {
                    t.BookId,
                    t.UserId,
                    t.MerchId,
                    t.BookStatus,
                    t.BookDate,
                    t.BookTime
                }).Where(t => t.MerchId == userID && t.BookStatus == "Active").ToList();

                return Ok(quolist);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("TotRejectedRequests")]
        public async Task<IActionResult> getRejectedRequests()
        {

            try
            {
                int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("merchID"));



                if (userID == null || userID <= 0)
                {
                    return BadRequest("Please log in !! Cant book");
                }

                var quolist = _context.Bookings.Select(t => new
                {
                    t.BookId,
                    t.UserId,
                    t.MerchId,
                    t.BookStatus,
                    t.BookDate,
                    t.BookTime
                }).Where(t => t.MerchId == userID && t.BookStatus == "Declined").ToList();

                return Ok(quolist.Count);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("totRequestPerMonth")]
        public async Task<IActionResult> getTotalRequestsPerMonth(int month)
        {

            try
            {
                int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("merchID"));



                if (userID == null || userID <= 0)
                {
                    return BadRequest("Please log in !! Cant book");
                }

                var quolist = _context.Bookings.Select(t => new
                {
                    t.BookId,
                    t.UserId,
                    t.MerchId,
                    t.BookStatus,
                    t.BookDate,
                    t.BookTime
                }).Where(t => t.MerchId == userID && t.BookStatus == "InActive" && t.BookDate.Month == month).ToList();

                return Ok(quolist.Count);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        //get all 
        [HttpGet]
        [Route("bookingnumbers")]
        public async Task<IActionResult> getActiveRequestsnumbers()
        {

            try
            {
                int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("merchID"));



                if (userID == null || userID <= 0)
                {
                    return BadRequest("Please log in !! Cant book");
                }

                var quolist = _context.Bookings.Select(t => new
                {
                    t.BookId,
                    t.UserId,
                    t.MerchId,
                    t.BookStatus,
                    t.BookDate,
                    t.BookTime
                }).Where(t => t.MerchId == userID && t.BookStatus == "Active").ToList();

                return Ok(quolist.Count);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("bookingnumbersMonth")]
        public async Task<IActionResult> getActiveRequestsPerMonth(int month)
        {

            try
            {
                int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("merchID"));



                if (userID == null || userID <= 0)
                {
                    return BadRequest("Please log in !! Cant book");
                }

                var quolist = _context.Bookings.Select(t => new
                {
                    t.BookId,
                    t.UserId,
                    t.MerchId,
                    t.BookStatus,
                    t.BookDate,
                    t.BookTime
                }).Where(t => t.MerchId == userID && t.BookStatus == "Active" && t.BookDate.Month == month).ToList();

                return Ok(quolist.Count);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("JobsDonenumbers")]
        public async Task<IActionResult> getJobsDoneNumbers()
        {

            try
            {
                int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("merchID"));
                if (userID == null || userID <= 0)
                {
                    return BadRequest("Please log in !! Cant book");
                }

                var quolist = _context.Jobs.Select(t => new
                {
                    t.BookId,
                    t.UserId,
                    t.MerchId,
                    t.JobStatus,
                    t.JobId
                }).Where(t => t.MerchId == userID && t.JobStatus == "Service Done").ToList();

                return Ok(quolist.Count);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("JobsDonenumbersPerMonth")]
        public async Task<IActionResult> getJobsDoneNumbersPerMonth(int month)
        {

            try
            {
                int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("merchID"));
                if (userID == null || userID <= 0)
                {
                    return BadRequest("Please log in !! Cant book");
                }

                var quolist = _context.Jobs.Select(t => new
                {
                    t.BookId,
                    t.UserId,
                    t.MerchId,
                    t.JobStatus,
                    t.JobDateend,
                    t.JobId
                }).Where(t => t.MerchId == userID && t.JobStatus == "Service Done" && t.JobDateend.Month == month).ToList();

                return Ok(quolist.Count);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //get all booking request
        [HttpGet]
        [Route("bookingnumberstotal")]
        public async Task<IActionResult> getAllRequestsnumbers()
        {

            try
            {
                int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("merchID"));



                if (userID == null || userID <= 0)
                {
                    return BadRequest("Please log in !! Cant book");
                }

                var quolist = _context.Bookings.Select(t => new
                {
                    t.BookId,
                    t.UserId,
                    t.MerchId,
                    t.BookStatus,
                    t.BookDate,
                    t.BookTime
                }).Where(t => t.MerchId == userID).ToList();

                return Ok(quolist.Count);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        //Confirm of booking Merchant 
        [HttpPut]
        [Route(("bookconfirm/{id}"))]
        public async Task<IActionResult> bookconfirm([FromBody] Bookconfirm combook, int id)
        {
            int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("merchID"));

            //  var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (combook == null || combook.BookId == null || combook.BookId <= 0)
            {
                return BadRequest("you cant edit ");
            }

            if (userID == null || userID <= 0)
            {
                return BadRequest("Please log in !! Cant book");
            }

            var dbbook = _context.Bookings.Where(t => t.BookId == combook.BookId && t.MerchId == userID && t.UserId == id).FirstOrDefault();
            if (dbbook == null)
            {
                return BadRequest("Cannot update nothing");
            }
            dbbook.BookStatus = combook.BookStatus;

            _context.Entry(dbbook).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            return Ok("Updated the status of booking");
        }

        //get the book by specific merchant
        [HttpGet]
        [Route("getUserBookid/{id}")]
        public async Task<IActionResult> getuserbookid(int id)
        {
            try
            {
                int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("merchID"));



                if (userID == null || userID <= 0)
                {
                    return BadRequest("Please log in !! Cant book");
                }
                if (id == null || id <= 0)
                {
                    return BadRequest("This booking is not available");
                }

                var booklist = _context.Bookings.Select(t => new
                {
                    t.BookId,
                    t.UserId,
                    t.MerchId,
                    t.BookStatus,
                    t.BookDate,
                    t.BookTime
                }).Where(t => t.MerchId == userID && t.BookId == id).FirstOrDefault();

                return Ok(booklist);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //Delete the booking by the merchant
        [HttpDelete]
        [Route("deletemerchantbook/{id}")]
        public async Task<IActionResult> deletebooking(int id)
        {
            try
            {
                int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("merchID"));



                if (userID == null || userID <= 0)
                {
                    return BadRequest("Please log in !! Cant book");
                }
                if (id == null || id <= 0)
                {
                    return BadRequest("This booking is not available");
                }

                var booklist = _context.Bookings.Where(t => t.MerchId == userID && t.BookId == id).FirstOrDefault();

                if (booklist == null)
                {
                    return BadRequest("you cant remove booking not avalable");
                }

                _context.Bookings.Remove(booklist);
                await _context.SaveChangesAsync();

                return Ok("Successuful removed the booking");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //get the book by  client 
        [HttpGet]
        [Route("getclientBooking/{id}")]
        public async Task<IActionResult> getclientbookid(int id) //the id is for the client it get along with function of current user
        {
            try
            {
                int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("userID"));



                if (userID == null || userID <= 0)
                {
                    return BadRequest("Please log in !! Cant book");
                }
                if (id == null || id <= 0)
                {
                    return BadRequest("This booking is not available");
                }

                var booklist = _context.Bookings.Select(t => new
                {
                    t.BookId,
                    t.UserId,
                    t.MerchId,
                    t.BookStatus,
                    t.BookDate,
                    t.BookTime
                }).Where(t => t.UserId == userID).FirstOrDefault();

                return Ok(booklist);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //get the specific client booking by the merchant 
        [HttpGet]
        [Route("getsinglebook/{id}")]
        public async Task<IActionResult> getsing(int id)
        {

            int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("merchID"));
            if (userID == null || userID <= 0)
            {
                return BadRequest("Please log in !! Cant view");
            }




            return Ok();
        }


        //delete specific user
        //[HttpDelete]
        //[Route("deleteuser/{id}")]
        //public async Task<IActionResult> DeleteUser(int id)
        //{

        //    int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("userID"));

        //    //var dbu = _context.Clients.Where(u => u.UserId == id).FirstOrDefault();

        //    var ttt = await _context.Bookings.Select(t => t.BookStatus).FindAsync(id);
        //    if (userID == null || userID <= 0)
        //    {
        //        return BadRequest("YOu are not log in");
        //    }

        //    if (ttt == null)
        //    {
        //        return BadRequest("user not found");
        //    }

        //    _context.Clients.Remove(ttt);
        //    await _context.SaveChangesAsync();

        //    return Ok(new { message = "user deleted" });
        //}

        ////get status by an book ID
        //[HttpGet]
        //[Route("statusby/{id}")]

        //public async Task<IActionResult> getstatusconfirmAveId(int id)
        //{
        //    int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("userID"));

        //    if (userID == null || userID <= 0)
        //    {
        //        return BadRequest("You are not log in, Please log in");
        //    }

        //    var kool1 = _context.Bookings.Select(t => new
        //    {
        //        t.BookId,
        //        t.UserId,
        //        t.BookStatus,

        //    }).Where(u => u.UserId == userID && u.BookStatus == "Active" && u.BookId == id).ToList();

        //    return Ok(kool1);
        //}

    }
}
