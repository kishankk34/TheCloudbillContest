using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ContestApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ContestApplication.Data;
using System.ComponentModel.DataAnnotations;
using ContestApplication.Areas.Identity.Data;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text;
using jsreport.Types;
using jsreport.AspNetCore;

namespace ContestApplication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ContestApplicationContext _context;
        private readonly UserManager<ContestApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ContestApplicationContext contestApplicationContext,
            UserManager<ContestApplicationUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
            _context = contestApplicationContext;
        }

        [BindProperty]
        public InputModelIndex Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModelIndex
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "First ContestKey")]
            public string FirstContestKey { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Possible Solution")]
            public string PossibleSolution { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Code")]
            public string Code { get; set; }

        }

        public IActionResult Index()
        {
            var email = _userManager.GetUserName(User);
            var emailparts = email.Split("@");
            ViewBag.Email = emailparts[0];
            var contest1table = _context.contest1Table.Where(p => p.Email == email).Select(s => s.submitted).ToList();
            var contest2table = _context.contest2Table.Where(p => p.Email == email).Select(s => s.submittedfile).ToList();
            if (contest1table.Count != 0)
            {
                ViewBag.submitted = contest1table.FirstOrDefault();
            }
            else
            {
                ViewBag.submitted = false;
            }
            if (contest2table.Count != 0)
            {
                ViewBag.submittedfile = contest2table.FirstOrDefault();
            }
            else
            {
                ViewBag.submittedfile = false;
            }
            return View();
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Contest1()
        {
            var email = _userManager.GetUserName(User);
            if (ModelState.IsValid)
            {
                if (Input.FirstContestKey != null && Input.PossibleSolution != null && Input.Code != null)
                {
                    var user = new contest1Table
                    {
                        FirstContestKey = Input.FirstContestKey,
                        PossibleSolution = Input.PossibleSolution,
                        Code = Input.Code,
                        submitted = true,
                        Email = email
                    };

                    _context.contest1Table.Add(user);
                    _context.SaveChanges();
                }
            }

            // If we got this far, something failed, redisplay form
            return Redirect("/Home/Index");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Contest2(List<IFormFile> files)
        {
            //var Server = "ftp://127.0.0.1";


            long size = files.Sum(f => f.Length);

            var email = _userManager.GetUserName(User);

            foreach (var formFile in files)
            {
                
                //var filename = Guid.NewGuid() + formFile.FileName;

                //string filepath = Path.Combine("ftp://166.62.26.27/", filename);
                //string localpath = Path.Combine("./Files/" + filename);
                //var stream = new FileStream(localpath, FileMode.Create);
                //formFile.CopyTo(stream);
                //stream.Close();
                ////Upload Method.
                //FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(string.Format("{0}", filepath)));
                //request.Method = WebRequestMethods.Ftp.UploadFile;
                //request.Credentials = new NetworkCredential("contest@contest.thecloudbill.com", "wgi3L76?");
                //request.UseBinary = true;
                //request.UsePassive = true;
                //Stream ftpstream = request.GetRequestStream();
                //FileStream fs = System.IO.File.OpenRead(localpath);

                //byte[] buffer = new byte[1024];
                //int byteRead = 0;
                //double read = 0;

                ////FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.ReadWrite);
                //double total = (double)fs.Length;


                //do
                //{
                //    byteRead = fs.Read(buffer, 0, 1024);
                //    if (byteRead > 0)
                //    {
                //        ftpstream.Write(buffer, 0, byteRead);
                //        read += (double)byteRead;
                //    }

                //    //double percentage = read / total * 100;
                //    //backgroundWorker1.ReportProgress((int)percentage);
                //}
                //while (byteRead != 0);
                //fs.Close();
                //ftpstream.Close();
                //System.IO.File.Delete(localpath);
                //if (total != 0)
                //{
                //    var user = new contest2Table
                //    {
                //        Email = email,
                //        fileToUpload = filename,
                //        submittedfile = true
                //    };
                //    _context.contest2Table.Add(user);
                //    _context.SaveChanges();
                //}

                string ftpServer = "ftp://166.62.26.27/";
                string ftpUserName = "contest@contest.thecloudbill.com";
                string ftpUserPass = "wgi3L76?";

                string fileName = Guid.NewGuid().ToString() + formFile.FileName ;

                

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpServer+fileName);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                // This example assumes the FTP site uses anonymous logon.
                request.Credentials = new NetworkCredential(ftpUserName, ftpUserPass);

                // Copy the contents of the file to the request stream.
                byte[] fileContents;
                using (StreamReader sourceStream = new StreamReader(formFile.OpenReadStream()))
                {
                    fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                }

                if(fileContents.Length != 0)
                {
                    request.ContentLength = fileContents.Length;

                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(fileContents, 0, fileContents.Length);
                    }

                    var user = new contest2Table
                    {
                        Email = email,
                        fileToUpload = fileName,
                        submittedfile = true
                    };
                    _context.contest2Table.Add(user);
                    _context.SaveChanges();

                }






            }
            //var filePaths = new List<string>();
            //foreach (var formFile in files)
            //{
            //    if (formFile.Length > 0)
            //    {
            //        // full path to file in temp location
            //        //var filePath = Path.GetTempFileName(); //we are using Temp file name just for the example. Add your own file path.
            //        var filePath = Path.Combine("C:/Users/gunja/source/repos/ContestApplication/ContestApplication/Files/",formFile.FileName);
            //        //filePaths.Add(filePath);

            //        using (var stream = new FileStream(filePath, FileMode.Create))
            //        {
            //            var user = new contest2Table
            //            {
            //                Email = email,
            //                fileToUpload = filePath,
            //                submittedfile = true
            //            };

            //            _context.contest2Table.Add(user);
            //            _context.SaveChanges();

            //            formFile.CopyTo(stream);
            //        }
            //    }
            //}

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Redirect("/Home/Index");

        }

        public IActionResult certificate()
        {
            return View();
        }
        [MiddlewareFilter(typeof(JsReportPipeline))]
        public IActionResult wc()
        {
            HttpContext.JsReportFeature().Recipe(Recipe.ChromePdf);
            return View();
        }

    }
}
