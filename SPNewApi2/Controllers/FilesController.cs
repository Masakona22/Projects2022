using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using SPNewApi2.DTO;
using SPNewApi2.Models;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace SPNewApi2.Controllers
{

   

    [Route("api/[controller]")]
    [ApiController]
    //enable cors
    [EnableCors("appCors")]
    public class FilesController : ControllerBase
    {
        //for database connection
        private readonly FinalPlugDBContext _context;

        private readonly IConfiguration _configuration;


        //private readonly IHostingEnvironment _host;

        

        private readonly IWebHostEnvironment _webHost;


        private string directoryPath;

        //private readonly UserManager<ApplicationUser> _userManager;
        public FilesController(FinalPlugDBContext _context, IConfiguration _configuration, IWebHostEnvironment _webHost)
        {
            this._context = _context;
            this._configuration = _configuration;
          //  this._host = _host;
            this._webHost = _webHost;
        }

        //    .Merchants.Select(t => new
        //        {
        //            t.MerchId,
        //            t.MerchName,
        //            t.MerchSurname,
        //            t.MerchEmail,
        //            t.MerchContactdetails,
        //            t.MerchAddress,
        //            t.MerchCity,
        //            t.MerchProvince,
        //            t.MerchType,
        //            t.MerchFile
        //})


//        int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("merchID"));

//            //  var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
//            if (combook == null || combook.BookId == null || combook.BookId <= 0)
//            {
//                return BadRequest("you cant edit ");
//    }

//            if (userID == null || userID <= 0)
//            {
//                return BadRequest("Please log in !! Cant book");
//}

//var dbbook = _context.Bookings.Where(t => t.BookId == combook.BookId && t.MerchId == userID && t.UserId == id).FirstOrDefault();
//if (dbbook == null)
//{
//    return BadRequest("Cannot update nothing");
//}
//dbbook.BookStatus = combook.BookStatus;

//_context.Entry(dbbook).State = EntityState.Modified;
//await _context.SaveChangesAsync();


//return Ok("Updated the status of booking");




        //Merchant uploads their pdf files (company files)
        [HttpPut]
        [Route("merchantupload")]
        public async Task<IActionResult> MErch(List<IFormFile> files )
        {
            // [FromForm] PdfUpload user,    ,int id

           // int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("merchID"));

            //if (userID == null || userID <= 0)
            //{
            //    return BadRequest("Please log in !! Cant upload file");
            //}

            var user11 = _context.Merchants.Where(u => u.MerchId == 1).FirstOrDefault();
            
            if (user11 == null)
            {
                return BadRequest("Merchant already uploaded the file !!!");
            }
            else
            {
               // user11 = new Merchant();

                if (files.Count == 0)
                    return BadRequest();
                string directoryPath = Path.Combine(_webHost.ContentRootPath, "pdf");
                foreach (var file in files)
                {
                    string filePath = Path.Combine(directoryPath, file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                     //   user11.MName = user.MName;
                        //     Imageupload imageupload = new Imageupload();
                        user11.MerchFile = filePath;
                    }

                    _context.Entry(user11).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    //_context.Merchants.Add(user11);
                    //await _context.SaveChangesAsync();


                }

                return Ok(new { message = "successful uploaded the file" });

                //     return Ok();

            }
        }


        //Upload the file using another method
        [HttpPut]
        [Route("uploadfile")]
        public async Task<string> Uploadfile(IFormFile file)
        {

            int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("merchID"));

            if (userID == null || userID <= 0)
            {
                return  "Please log in !! Cant upload file";
            }

            var user11 = _context.Merchants.Where(u => u.MerchId == userID).FirstOrDefault();

            if (user11 == null)
            {
                return  "Merchant already uploaded the file !!!";
            }
            else
            {

          

            if (file.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(_webHost.WebRootPath + "\\pdf1\\")) 
                    {
                        Directory.CreateDirectory(_webHost.WebRootPath + "\\pdf1\\");
                       
                     }
                     string   directoryPath1 = Path.Combine(_webHost.WebRootPath, "pdf1");
                        string filePath = Path.Combine(directoryPath1, file.FileName);
                        using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fs);
                      // ;
                            user11.MerchFile = filePath;
                            
                            _context.Entry(user11).State = EntityState.Modified;
                            await _context.SaveChangesAsync();
                            fs.Flush();
                            return "\\pdf1\\" + file.FileName;
                        }

                    
                    }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }
            else
            {
                return "failed to upload";
            }
            }

        }



        public class FileUpload
        {
             public IFormFile files
            {
                get;
                set;
            }
        }

        [HttpPut]
        [Route("uploadimage")]
        public async Task<string> post([FromForm] FileUpload objfile)
        {

            int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("merchID"));

            if (userID == null || userID <= 0)
            {
                return "Please log in !! Cant upload file";
            }

            var user11 = _context.Merchants.Where(u => u.MerchId == userID).FirstOrDefault();

            if (user11 == null)
            {
                return "Merchant already uploaded the file !!!";
            }
            else
            {
                if(objfile.files.Length > 0)
                {
                    try
                    {
                        if (!Directory.Exists(_webHost.WebRootPath+"\\pdf\\"))
                        {
                            Directory.CreateDirectory(_webHost.WebRootPath + "\\pdf\\");
                        }
                        using (FileStream fileStream = System.IO.File.Create(_webHost.WebRootPath + "\\pdf\\" + objfile.files.FileName))
                        {
                            objfile.files.CopyTo(fileStream);
                            fileStream.Flush();

                            user11.MerchFile = "\\pdf\\" + objfile.files.FileName;
                            _context.Entry(user11).State = EntityState.Modified;
                            await _context.SaveChangesAsync();

                            return "\\pdf\\" + objfile.files.FileName;

                        }

                    }
                    catch(Exception e)
                    {
                        return e.ToString();
                    }
                }
                else
                {
                    return "unsucess";
                }
            }


            }
        //download the picture 
        [HttpGet]
        [Route("downloadpicture/{id}")]
        public async Task<ActionResult> Download11(int id)
        {

            var provider = new FileExtensionContentTypeProvider();
            var picture = await _context.Merchants.FindAsync(id);

            if (picture == null)
                return NotFound();

            var file = Path.Combine(_webHost.WebRootPath, "pdf", picture?.MerchFile);

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
            return File(fileBytes, contentType, picture.MerchFile);
        }



        [HttpGet]
        [Route("download/{id}")]
        public async Task<ActionResult> Download(int id)
        {

            var provider = new FileExtensionContentTypeProvider();
            var document = await _context.Merchants.FindAsync(id);

            if (document == null)
                return NotFound();

            var file = Path.Combine(_webHost.WebRootPath, "pdf1", document?.MerchFile);

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


        //get user by an ID
        [HttpGet]
        [Route("users/{id}")]
        public async Task<IActionResult> getuserid(int id)
        {

            var user11 = _context.Merchants.Select(t => new
            {
                t.MerchId,
                t.MerchName,
                t.MerchSurname,
                t.MerchType

            }).Where(u => u.MerchId == id).FirstOrDefault();

            if (user11 == null)
            {
                return BadRequest("Cant find the specific user");

            }



            return Ok(user11);
            // return Ok(new { userID = id });

        }


        //Another methisd for test of upload using the post
        [HttpPost, DisableRequestSizeLimit]
        [Route("postfile")]
        public async Task<IActionResult> Upload()
        {
            try
            {

                int userID = Convert.ToInt32(HttpContext.User.FindFirstValue("merchID"));

                if (userID == null || userID <= 0)
                {
                    return BadRequest("Please log in !! Cant upload file");
                }
                var user11 = _context.Merchants.Where(u => u.MerchId == userID).FirstOrDefault();

                if (user11 == null)
                {
                    return BadRequest("Merchant already uploaded the file !!!");
                }
                else
                {


                    var formCollection = await Request.ReadFormAsync();
                    var file = formCollection.Files.First();

                    // var file = Request.Form.Files[0];
                    var folderName = Path.Combine(_webHost.ContentRootPath, "Resources", "Images");
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        var fullPath = Path.Combine(pathToSave, fileName);
                        var dbPath = Path.Combine(folderName, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        user11.MerchFile = dbPath;

                        _context.Entry(user11).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                        return Ok(new { dbPath });
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }


        }

        //download the file by the codemaze 
        [HttpGet]
        [Route("downloadf/{id}")]
        public async Task<ActionResult> Downloadfile(int id)
        {

            var provider = new FileExtensionContentTypeProvider();
            var picture = await _context.Merchants.FindAsync(id);

            if (picture == null)
                return NotFound();

            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName, picture?.MerchFile);

            var file = Path.Combine(_webHost.ContentRootPath, "Resources", "Images", picture?.MerchFile);

            string contentType;
            if (!provider.TryGetContentType(pathToSave, out contentType))
            {
                contentType = " application/octet-stream";
            }
     ;

            byte[] fileBytes;

            if (System.IO.File.Exists(pathToSave))
            {
                fileBytes = System.IO.File.ReadAllBytes(pathToSave);
            }
            else
            {
                return NotFound();
            }
            return File(fileBytes, contentType, picture.MerchFile);
        }


    }
}
