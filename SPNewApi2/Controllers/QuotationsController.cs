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
    public class QuotationsController : ControllerBase
    {
        //for database connection
        private readonly FinalPlugDBContext _context;

        private readonly IConfiguration _configuration;

        //private readonly UserManager<ApplicationUser> _userManager;
        public QuotationsController(FinalPlugDBContext _context, IConfiguration _configuration)
        {
            this._context = _context;
            this._configuration = _configuration;
        }


        //Log of quotation by merchant for a client
        [HttpPost]
        [Route("addquotation")]
        public async Task<IActionResult> Createclientquotation([FromBody] Addquot qot)
        {
            try
            {

                int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("merchID"));

                //var newbook = _context.BookingAddBeta.Where(u => u.BookId == book.BookId ).FirstOrDefault();
                //if (newbook != null)
                //{
                //    return BadRequest("Booking already existed");
                //}
                if (qot == null)
                {
                    return BadRequest("You cant log empty quotation");
                }

                if (userID == null || userID <= 0)
                {
                    return BadRequest("YOu are not log in");
                }

                var newquotation = new Quotation
                {
                    UserId = qot.UserId,
                    BookId = qot.BookId,
                    MerchId = userID,
                    QuotAmount = qot.QuotAmount,
                    QuotDescription = qot.QuotDescription


                };

                _context.Quotations.Add(newquotation);
                await _context.SaveChangesAsync();
                return Ok(new { message = "successful added a quotation" });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //Get all the quotations
        [HttpGet]
        [Route("getquotationbyclient")]
        public async Task<IActionResult> getquotations()
        {
            int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("UserID"));
            //  var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userID == null || userID <= 0)
            {
                return BadRequest("Please log in !! Cant book");
            }
            List<Quotation> listuser = _context.Quotations.Where(t => t.UserId == userID).ToList();
            if (listuser != null)
            {
                return Ok(listuser);
            }
            return BadRequest("They are no quotations in database");
        }

        //get quptations for specific client by the book id 
        [HttpGet]
        [Route("getquotatio/{id}")]
        public async Task<IActionResult> getquot(int id)
        {
            int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("UserID"));
            if (userID == null || userID <= 0)
            {
                return BadRequest("Please log in !! Cant view quotaion");
            }
            List<Quotation> listuser = _context.Quotations.Where(t => t.UserId == userID && t.BookId == id).ToList();
            if (listuser != null)
            {
                return Ok(listuser);
            }
            return BadRequest("They are no quotations in database");

            return Ok();
        }


        //Get all quotations
        [HttpGet]
        [Route("getallquotations")]
        public async Task<IActionResult> getallquotationgsall()
        {

            try
            {
                List<Quotation> listuser = _context.Quotations.ToList();
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
    }
}
