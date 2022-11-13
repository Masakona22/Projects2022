using SPNewApi2.Tools;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SPNewApi2.DTO;
using SPNewApi2.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SPNewApi2.Controllers
{
    //enable cors
    [EnableCors("appCors")]

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //for database connection
        private readonly FinalPlugDBContext _context;

        private readonly IConfiguration _configuration;
 
        public UsersController(FinalPlugDBContext _context, IConfiguration _configuration)
        {
            this._context = _context;
            this._configuration = _configuration;
        }

        //Getting all users from database (client)
        [HttpGet]
        [Route("Users")]
        public async Task<IActionResult> getUsers()
        {
            try
            {
                List<Client> listuser = _context.Clients.ToList();
                if (listuser != null)
                {
                    return Ok(listuser);
                }
                return Ok("They are no users in database");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        //for log in the user (client)
        [HttpPost]
        [Route("clientlogin")]
        public async Task<IActionResult> userlogin([FromBody] UserLogin user)
        {
            try
            {
                String password = Password.hashPassword(user.UserPassword);
                var user11 = _context.Clients.Where(u => u.UserEmail == user.UserEmail && u.UserPassword == password).FirstOrDefault();
                if (user11 == null)
                {
                    return BadRequest("Either email or password is incorrect!!");
                }
                else
                {
                    List<Claim> authClaim = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, user11.UserEmail),
                        new Claim ("userID",user11.UserId.ToString()),
                        new Claim(ClaimTypes.Role,"Client")
                      //  new Claim (ClaimTypes.Role, user11.userrole),
                    };

                    var token = this.getToken(authClaim);

                    return Ok(new
                    {
                        message = "User log in in the system",
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
                   
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //for creating the jwt token
        private JwtSecurityToken getToken(List<Claim> authClaim)
        {

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken
            (
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(24),
                claims: authClaim,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        //for client regestering 
        [HttpPost]
        [Route("clientregister")]
        public async Task<IActionResult> userregister([FromBody] UserRegister user)
        {
            try
            {
                var user11 = _context.Clients.Where(u => u.UserEmail == user.UserEmail).FirstOrDefault();
                if (user11 != null)
                {
                    return BadRequest("Client already existed !!!");
                }
                else
                {
                    user11 = new Client();

                    user11.UserName = user.UserName;
                    user11.UserSurname = user.UserSurname;
                    user11.UserEmail = user.UserEmail;
                    user11.UserContactdetails = user.UserContactdetails;
                    user11.UserAddress = user.UserAddress;
                    user11.UserProvince = user.UserProvince;
                    user11.UserCity = user.UserCity;
                    user11.UserStatus = "Active";
                    user11.UserPassword = Password.hashPassword(user.UserPassword);

                    _context.Clients.Add(user11);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "successful registered" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //get user by an ID
        [HttpGet]
        [Route("users/{id}")]
        public async Task<IActionResult> getuserid(int id)
        {

            var user11 = _context.Clients.Select(t => new
            {
                t.UserId,
                t.UserName,
                t.UserSurname,
                t.UserEmail,
                t.UserContactdetails,
                t.UserAddress,
                t.UserProvince, 
                t.UserCity
            }).Where(u => u.UserId == id).FirstOrDefault();

            if (user11 == null)
            {
                return BadRequest("Cant find the specific user");

            }



            return Ok(user11);
            // return Ok(new { userID = id });

        }

        //Getting current loged user in the system
        [HttpGet]
        [Route("users/current")]
        public async Task<IActionResult> getcurrentuser()
        {
            int id = Convert.ToInt32(HttpContext.User.FindFirstValue("userID"));

            if (id == null || id <= 0)
            {
                return BadRequest("YOu are not log in");
            }
            var user11 = _context.Clients.Select(t => new
            {
                t.UserId,
                t.UserName,
                t.UserSurname,
                t.UserEmail,
                t.UserProvince,
                t.UserCity
            }).Where(u => u.UserId == id).ToList();

            return Ok(user11);

            // return Ok(new { userID = id });
        }



        //Get your booking status from the merchantt
        [HttpGet]
        [Route("getstatusbook")]
        public async Task<IActionResult> getstatusconfirm()
        {
            int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("userID"));

            if (userID == null || userID <= 0)
            {
                return BadRequest("Yuu are not log in, Please log");
            }
            //var quolist = _context.BookingSunday.Select(t => new
            //{
            //    t.BookId,
            //    t.UserId,
            //    t.MerchId,
            //    t.BookStatus,
            //    t.BookDate
            //}).Where(t => t.MerchId == userID && t.BookStatus == "InActive").ToList();
            var user11 = _context.Bookings.Select(t => new
            {
                t.BookId,
                t.UserId,
                t.MerchId,
                t.BookStatus,
                t.BookDate,
                t.BookTime
            }).Where(u => u.UserId == userID).ToList(); //i have just changed here 

            return Ok(user11);
        }


        //Get your booking status from the merchantt which is only active
        [HttpGet]
        [Route("getstatusactive")]
        public async Task<IActionResult> getstatusconfirmActive()
        {
            int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("userID"));

            if (userID == null || userID <= 0)
            {
                return BadRequest("You are not log in, Please log in");
            }
            
            var kool1 = _context.Bookings.Select(t => new
            {
                t.BookId,
                t.UserId,
                t.MerchId,
                t.BookStatus,
                t.BookDate,
                t.BookTime
            }).Where(u => u.UserId == userID && u.BookStatus == "Active").ToList();

            return Ok(kool1);
        }

        //get status by an book ID
        [HttpGet]
        [Route("statusby/{id}")]
      
        public async Task<IActionResult> getstatusconfirmAveId(int id )
        {
            int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("userID"));

            if (userID == null || userID <= 0)
            {
                return BadRequest("You are not log in, Please log in");
            }

            var kool1 = _context.Bookings.Select(t => new
            {
                t.BookId,
                t.UserId,
                t.BookStatus,
               
            }).Where(u => u.UserId == userID && u.BookStatus == "Active" && u.BookId == id).ToList();

            return Ok(kool1);
        }


        [HttpPut]
        [Route("edituser/{id}")]
        public async Task<ActionResult> userupdate([FromBody] Edituser user, int id)
        {

            int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("userID"));

            //var dbu = _context.Clients.Where(u => u.UserId == id).FirstOrDefault();

            var dbu = await _context.Clients.FindAsync(id);
            if (userID == null || userID <= 0)
            {
                return BadRequest("YOu are not log in");
            }
            if (dbu == null)
            {
                return BadRequest("User not found");
            }

            dbu.UserName = user.UserName;
            dbu.UserSurname = user.UserSurname;
            dbu.UserContactdetails = user.UserContactdetails;
            dbu.UserAddress = user.UserAddress;
            dbu.UserProvince = user.UserProvince;
            dbu.UserCity = user.UserCity;

            await _context.SaveChangesAsync();

            return Ok(await _context.Clients.ToListAsync());
        }

        //delete specific user
        [HttpDelete]
        [Route("deleteuser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {

            int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("userID"));

            //var dbu = _context.Clients.Where(u => u.UserId == id).FirstOrDefault();

            var ttt = await _context.Clients.FindAsync(id);
            if (userID == null || userID <= 0)
            {
                return BadRequest("YOu are not log in");
            }

            if (ttt == null)
            {
                return BadRequest("user not found");
            }

            _context.Clients.Remove(ttt);
            await _context.SaveChangesAsync();

            return Ok(new { message = "user deleted" });
        }




        //get the book by  client 
        [HttpGet]
        [Route("getbokstatus/{id}")]
        public async Task<IActionResult> getclientbookid(int id)   
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
                    
                    t.BookStatus,
                  
                }).Where(t => t.UserId == userID && t.BookId == id).FirstOrDefault();

                return Ok(booklist);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        //get the job by  client 
        [HttpGet]
        [Route("getjobstatus/{id}")]
        public async Task<IActionResult> getclientjobid(int id)
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

                var booklist = _context.Jobs.Select(t => new
                {
                    t.BookId,
                    t.UserId,

                    t.JobStatus,

                }).Where(t => t.UserId == userID && t.BookId == id).FirstOrDefault();

                return Ok(booklist);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
