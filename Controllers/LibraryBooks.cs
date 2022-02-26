using LibraryCoreProject.Context;
using LibraryCoreProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCoreProject.Controllers
{
    public class LibraryBooks : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private MyDBContext db;
        private IHostingEnvironment _HostEnvironment;
        public LibraryBooks(MyDBContext _db, IHostingEnvironment HostEnvironment)
        {
            db = _db;
            _HostEnvironment = HostEnvironment;
        }
        public JsonResult InsertDept(Category stu_Guard)
        {
            Category a = new Category();
            a.catid = stu_Guard.catid;
            a.Categoryname = stu_Guard.Categoryname;
            a.Department = stu_Guard.Department;
            db.Categories.Add(a);
            db.SaveChanges();
            return Json(a);
        }
        public JsonResult InsertItems(Book stu_Guard)
        {
            Book a1 = new Book();
            a1.Bookcode = stu_Guard.Bookcode;
            a1.Bookname = stu_Guard.Bookname;
            a1.date = DateTime.Parse(stu_Guard.date.ToShortDateString());
            a1.picture = stu_Guard.picture;
            a1.cost = (decimal)stu_Guard.cost;
            a1.rate = (decimal)stu_Guard.rate;
            db.Books.Add(a1);
            db.SaveChanges();
            return Json(a1);
        }

        public JsonResult DeleteAll(string id)
        {

            List<Book> st5 = db.Books.Where(xx => xx.catid == id).ToList();
            db.Books.RemoveRange(st5);

            Category st6 = db.Categories.Find(id);
            if (st6 != null)
            {
                db.Categories.Remove(st6);
            }
            db.SaveChanges();
            return Json("OK");
        }


        public JsonResult GetDept(string id)
        {
            var a = (from d in db.Categories where d.catid == id select new { d.catid, d.Categoryname, d.Department });
            return Json(a);
        }
        public JsonResult GetItems(string id)
        {
            var a = (from d in db.Books where d.catid == id select new { d.Bookcode, d.Bookname, d.cost, d.rate, d.date, d.picture });
            return Json(a);
        }

        public ActionResult ShowMe()
        {
            IEnumerable<Category> s = db.Categories.ToList();
            return View(s);
        }


        public ActionResult Create2(string sid = "0")
        {
            ViewBag.sid = sid;
            return View();
        }



        [HttpPost]
        public ActionResult UploadFiles()
        {
            // Checking no of files injected in Request object  
            if (Request.Form.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    var files = Request.Form.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        IFormFile file = files[i];
                        string fname;

                        fname = file.FileName;

                        // Get the complete folder path and store the file inside it.  
                        //fname = Path.Combine(Server.MapPath("~/Uploads/"), fname);
                        string webRootPath = _HostEnvironment.WebRootPath;
                        string fname1 = "";
                        fname1 = Path.Combine(webRootPath, "Uploads/" + fname);
                        file.CopyTo(new FileStream(fname1, FileMode.Create));
                    }
                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public string GenerateCodeDP()
        {
            string a1 = "";
            string b1 = "";

            try
            {
                var a = (from det in db.Categories select det.catid.Substring(3)).Max();
                int b = int.Parse(a.ToString()) + 1;
                if (b < 10)
                {
                    b1 = "000" + b.ToString();
                }
                else if (b < 100)
                {
                    b1 = "00" + b.ToString();
                }
                else if (b < 1000)
                {
                    b1 = "0" + b.ToString();
                }
                else
                {
                    b1 = b.ToString();
                }
                a1 = "DP-" + b1.ToString();
            }
            catch (Exception ex)
            {
                a1 = "AC-0001";
            }
            return a1;
        }
        public string GenerateCodeItems()
        {
            string a1 = "";
            string b1 = "";

            try
            {
                var a = (from det in db.Books select det.Bookcode.Substring(3)).Max();
                int b = int.Parse(a.ToString()) + 1;
                if (b < 10)
                {
                    b1 = "000" + b.ToString();
                }
                else if (b < 100)
                {
                    b1 = "00" + b.ToString();
                }
                else if (b < 1000)
                {
                    b1 = "0" + b.ToString();
                }
                else
                {
                    b1 = b.ToString();
                }
                a1 = "IT-" + b1.ToString();
            }
            catch (Exception ex)
            {
                a1 = "IT-0001";
            }
            return a1;
        }
    }

}
