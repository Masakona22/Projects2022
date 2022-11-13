using SPNewApi2.Tools;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SPNewApi2.DTO;
using SPNewApi2.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
 

namespace SPNewApi2.Controllers
{
    //enable cors
    [EnableCors("appCors")]

    [Route("api/[controller]")]
    [ApiController]
    public class MerchantsController : ControllerBase
    {
        //for database connection
        private readonly FinalPlugDBContext _context;

        private readonly IConfiguration _configuration;

        //private readonly UserManager<ApplicationUser> _userManager;
        public MerchantsController(FinalPlugDBContext _context, IConfiguration _configuration)
        {
            this._context = _context;
            this._configuration = _configuration;
        }

        //Merchant registrtation
        [HttpPost]
        [Route("Merchantregister")]
        public async Task<IActionResult> Merchantregister([FromBody] MerchantRegister merch)
        {
            try
            {
                var user11 = _context.Merchants.Where(u => u.MerchEmail == merch.MerchEmail).FirstOrDefault();
                if (user11 != null)
                {
                    return BadRequest("Merchant already existed");
                }
                else
                {
                    user11 = new Merchant();

                    user11.MerchName = merch.MerchName;
                    user11.MerchSurname = merch.MerchSurname;
                    user11.MerchEmail = merch.MerchEmail;
                    user11.MerchContactdetails = merch.MerchContactdetails;
                    user11.MerchAddress = merch.MerchAddress;
                    user11.MerchProvince = merch.MerchProvince;
                    user11.MerchCity = merch.MerchCity;
                    user11.MerchType = merch.MerchType;
                    user11.MerchPassword = Password.hashPassword(merch.MerchPassword);
                    user11.MerchVerify = "InActive";
                    user11.MerchStatus = "Active";
                 //   user11.MerchFile = "C:/Users/hatal/OneDrive - University of Johannesburg/D2/Team10/SPApi/SPApi/pdf/blank.pdf";


                    _context.Merchants.Add(user11);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "successful registered a Merchant" });
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        //for log in the user (Merchant)
        [HttpPost]
        [Route("merchantlogin")]
        public async Task<IActionResult> merchantlogin([FromBody] MerchantLogin user)
        {
            try
            {
                String password = Password.hashPassword(user.MerchPassword);
                var user11 = _context.Merchants.Where(u => u.MerchEmail == user.MerchEmail && u.MerchPassword == password).FirstOrDefault();
                if (user11 == null)
                {
                    return BadRequest("Either email or password is incorrect!!");
                }
                else
                {
                    List<Claim> authClaim = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, user11.MerchEmail),
                        new Claim ("merchID",user11.MerchId.ToString())
                    };

                    var token = this.getToken(authClaim);

                    return Ok(new
                    {
                        message = "Merchant log in in the system",
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


        //for log in the user (Merchant)
        [HttpPost]
        [Route("merchantlogin1")]
        public async Task<IActionResult> merchantlogin1([FromBody] MerchantLogin user)
        {
            try
            {
                String password = Password.hashPassword(user.MerchPassword);
                var user11 = _context.Merchants.Where(u => u.MerchEmail == user.MerchEmail && u.MerchPassword == password).FirstOrDefault();
                if (user11 == null)
                {
                    return BadRequest("Either email or password is incorrect!!");
                }
                else
                {
                    List<Claim> authClaim = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, user11.MerchEmail),
                        new Claim ("merchID",user11.MerchId.ToString())
                    };

                    var token = this.getToken(authClaim);

                    return Ok(new
                    {
                        message = "Merchant log in in the system",
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

        //Get all Merchants
        [HttpGet]
        [Route("getmerchants")]
        public async Task<IActionResult> getallMerchant()
        {

            try
            {
                List<Merchant> listuser = _context.Merchants.Where(u => u.MerchVerify == "Active").OrderByDescending(t => t.MerchId).ToList();
                if (listuser != null)
                {
                    return Ok(listuser);
                }
                return Ok("They are no Merchants in database");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        //get merchant by an ID
        [HttpGet]
        [Route("merchby/{id}")]
        public async Task<IActionResult> getmerchantid(int id)
        {

            var user11 = _context.Merchants.Select(t => new
            {
                t.MerchId,
                t.MerchName,
                t.MerchSurname,
                t.MerchEmail,
                t.MerchContactdetails,
                t.MerchAddress,
                t.MerchCity,
                t.MerchProvince,
                t.MerchType,
                t.MerchFile
            }).Where(u => u.MerchId == id).FirstOrDefault();

            if (user11 == null)
            {
                return BadRequest("Cant find the specific user");

            }

            return Ok(user11);
            // return Ok(new { userID = id });

        }

        //Getting current loged merchant in the system
        [HttpGet]
        [Route("merchant/current")]
        public async Task<IActionResult> getcurrentmerchant()
        {
            int id = Convert.ToInt32(HttpContext.User.FindFirstValue("merchID"));

            if (id == null || id <= 0)
            {
                return BadRequest("You are not log in");
            }
            var user11 = _context.Merchants.Select(t => new
            {
                t.MerchId,
                t.MerchName,
                t.MerchSurname,
                t.MerchEmail,
                t.MerchProvince,
                t.MerchType,
                t.MerchAddress,
                t.MerchContactdetails,
                t.MerchCity,   
            }).Where(u => u.MerchId == id).FirstOrDefault();

            return Ok(user11);

            // return Ok(new { userID = id });
        }


        [HttpPut]
        [Route("edituser/{id}")]
        public async Task<ActionResult> userupdate([FromBody] EditMerchant user, int id)
        {

            int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("merchID"));

            //var dbu = _context.Clients.Where(u => u.UserId == id).FirstOrDefault();

            var dbu = await _context.Merchants.FindAsync(id);
            if (userID == null || userID <= 0)
            {
                return BadRequest("YOu are not log in");
            }
            if (dbu == null)
            {
                return BadRequest("User not found");
            }

            dbu.MerchAddress = user.MerchAddress;
            dbu.MerchCity = user.MerchCity;
            dbu.MerchProvince = user.MerchProvince;
            dbu.MerchContactdetails = user.MerchContactdetails;


            await _context.SaveChangesAsync();

            return Ok(await _context.Merchants.ToListAsync());
        }






        //Get Merchants by location and Type
        [HttpGet]
        [Route("MerchantsLocation")]
        public async Task<IActionResult> getMerchantsloc(string location, string type)
        {
            var LMI = new List<Merchant>();
            int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("userID"));

            //var dbu = _context.Clients.Where(u => u.UserId == id).FirstOrDefault();


            if (userID == null || userID <= 0)
            {
                return BadRequest("YOu are not log in");

            }


            if (type == "ROOFING")
            {
                dynamic MI = (from m in _context.Merchants
                              where m.MerchCity == location &&
                              m.MerchType.Equals("ROOFING") && m.MerchVerify == "Active"
                              select m);

                foreach (Merchant i in MI)
                {
                    LMI.Add(i);
                }
            }

            else if (type == "BRICKLAYING")
            {
                dynamic MI = (from m in _context.Merchants
                              where m.MerchCity == location &&
                              m.MerchType.Equals("BRICKLAYING") && m.MerchVerify == "Active"
                              select m);

                foreach (Merchant i in MI)
                {
                    LMI.Add(i);
                }
            }
            else if (type =="PLUMBING")
            {
                dynamic MI = (from m in _context.Merchants
                              where m.MerchCity == location &&
                              m.MerchType.Equals("PLUMBING") && m.MerchVerify == "Active"
                              select m);

                foreach (Merchant i in MI)
                {
                    LMI.Add(i);
                }
            }
            else
            {
                return BadRequest("They are no merchants in database");
            }
            return Ok(LMI);

        }
    }
}
