using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using SPNewApi2.DTO;
using SPNewApi2.Models;

namespace SPNewApi2.Controllers
{


    //enable cors
    [EnableCors("appCors")]


    [Route("api/[controller]")]
    [ApiController]
    public class ManagersController : ControllerBase
    {
        //for database connection
        private readonly FinalPlugDBContext _context;

        private readonly IConfiguration _configuration;

        private readonly IWebHostEnvironment _webHost;

        //private readonly UserManager<ApplicationUser> _userManager;
        public ManagersController(FinalPlugDBContext _context, IConfiguration _configuration, IWebHostEnvironment _webHost)
        {
            this._context = _context;
            this._configuration = _configuration;
            this._webHost = _webHost;
        }



        //  var LMI = new List<Merchant>();
        //int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("userID"));



        //if (userID == null || userID <= 0)
        //{
        //    return BadRequest("YOu are not log in");

        //}


        //if (status == "NAN")
        //{
        //    dynamic MI = (from m in _context.Merchant
        //                  where m.MerchVcode == status 
        //                  select m);

        //    foreach (Merchant i in MI)
        //    {
        //        LMI.Add(i);
        //    }
        //}
        //return Ok(LMI);


        //To get merchants that are not verified 
        [HttpGet]
        [Route("getNotverifiedMerch")]
        public async Task<IActionResult> getNotVerifiedMerc()
        {
            try
            {
                List<Merchant> listuser = _context.Merchants.Where(u => u.MerchVerify == "InActive").ToList();
                if (listuser != null)
                {
                    return Ok(listuser);
                }
                return Ok("They are no Merchants that are not verified ");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        //Get the merchant by id not verified 
        [HttpGet]
        [Route("singnotverified/{id}")]
        public async Task<IActionResult> getsingnotverified(int id)
        {

            var userNot = _context.Merchants.Select(t => new
            {
                t.MerchId,
                t.MerchName,
                t.MerchSurname,
                t.MerchEmail,
                t.MerchContactdetails,
                t.MerchAddress,
                t.MerchProvince,
                t.MerchCity,
                t.MerchType,
                t.MerchVerify,
                t.MerchFile,
            }).Where(u => u.MerchId == id && u.MerchVerify == "InActive").FirstOrDefault();

            if (userNot == null)
            {
                return BadRequest("Cant find that merchant not verified");

            }
            return Ok(userNot);
        }

        //get pdf for specific merchants 
        [HttpGet]
        [Route("downloadpdf/{id}")]
        public async Task<ActionResult> Downloadthefile(int id)
        {

            var provider = new FileExtensionContentTypeProvider();
            var document = await _context.Merchants.FindAsync(id);

            if (document == null)
                return NotFound();

            var file = Path.Combine(_webHost.ContentRootPath, "pdf", document?.MerchFile);

            string contentType;
            if (!provider.TryGetContentType(file, out contentType))
            {
                contentType = " application/octet-stream";
            }
          ;

            byte[] fileBytes;

            if (System.IO.File.Exists(file))
            {
                fileBytes = System.IO.File.ReadAllBytes(file);
            }
            else
            {
                return NotFound();
            }
            return File(fileBytes, contentType, document.MerchFile);
        }

        //Edit to verify the merchant 
        [HttpPut]
        [Route("editmerchstatus/{id}")]
        public async Task<ActionResult> userupdate([FromBody] Merchanteditstatus merch, int id)
        {

            //  int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("userID"));



            var dbu = await _context.Merchants.FindAsync(id);
            //if (userID == null || userID <= 0)
            //{
            //    return BadRequest("YOu are not log in");
            //}
            if (dbu == null)
            {
                return BadRequest("Merchant  not found the one you want");
            }

            dbu.MerchVerify = merch.MerchVerify;
           // dbu.MerchType = merch.MerchType;


            await _context.SaveChangesAsync();

            return Ok(await _context.Merchants.ToListAsync());
        }
    }
}
