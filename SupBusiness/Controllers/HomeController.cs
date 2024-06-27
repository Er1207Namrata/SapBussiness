using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SupBusiness.Models;
using System.Data;
using System.Diagnostics;

namespace SupBusiness.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }

        public IActionResult Index(MemberRegistration model)
        {
            try
            {
                DataSet dataSet = new DataSet();
                List<SelectListItem> ddldesignation = new List<SelectListItem>();
                //model.OpCode = 2;
                //dataSet = model.GetMasterData();
                //ddldesignation.Add(new SelectListItem { Text = "--Select Designation--", Value = "0" });
                //if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                //{
                //    for (int i =0;i< dataSet.Tables[0].Rows.Count;i++)
                //    {
                //        ddldesignation.Add(new SelectListItem { Text = dataSet.Tables[0].Rows[i]["Name"].ToString(), Value = dataSet.Tables[0].Rows[i]["Id"].ToString() });
                //    }
                //}
                //ViewBag.ddldesignation = ddldesignation;
            }  
            catch(Exception)
            {
                throw;
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Login model)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = model.GetAdminLogin();
                if(ds != null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
                {
                    if (ds.Tables[0].Rows[0]["flag"].ToString()=="1")
                    {
                        if(ds.Tables[0].Rows[0]["Type"].ToString() =="Admin")
                        {
                            model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                           
                            model.LoginId = ds.Tables[0].Rows[0]["loginid"].ToString();
                            model.Fk_MemId = ds.Tables[0].Rows[0]["Pk_Id"].ToString();
                            HttpContext.Session.SetString("Name", model.Name);
                            HttpContext.Session.SetString("loginid", model.LoginId);
                            HttpContext.Session.SetString("Fk_MemId", model.Fk_MemId);
                            return RedirectToAction("Dashboard", "Admin");
                        }
                        else
                        {
                            model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                            model.LoginId = ds.Tables[0].Rows[0]["loginid"].ToString();
                           
                            model.Fk_MemId = ds.Tables[0].Rows[0]["Pk_Id"].ToString();
                            HttpContext.Session.SetString("Name", model.Name);
                            HttpContext.Session.SetString("loginid", model.LoginId);
                            HttpContext.Session.SetString("Fk_MemId", model.Fk_MemId);
                            return RedirectToAction("Dashboard", "User");
                           
                        }


                    }
                    else
                    {
                        TempData["Msg"] = ds.Tables[0].Rows[0]["msg"].ToString();
                    }
                }
            }
            catch(Exception)
            {
                throw;
            }

            return View();
        }
       

        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();

        }
        [HttpPost]
        public ActionResult SaveUserRegistration(string FistName,string MiddleName,string LastName,string MobileNumber)
        {
            MemberRegistration model = new MemberRegistration();
            DataSet ds = new DataSet();
            try
            {
                model.FirstName = FistName;
                model.LastName = LastName;
                model.MiddleName = MiddleName;
                model.MobileNumber = MobileNumber;
                model.OpCode = 1;
                model.AddedBy = "1";
                ds = model.SaveUserRegistration();
                if(ds!=null && ds.Tables.Count>0&& ds.Tables[0].Rows.Count>0)
                {
                    if (ds.Tables[0].Rows[0]["Flag"].ToString()=="1")
                    {
                        model.msg = ds.Tables[0].Rows[0]["Msg"].ToString();
                        model.flag = ds.Tables[0].Rows[0]["Flag"].ToString();
                        
                        model.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        model.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }
            return Json(model);
        }


        public ActionResult LeadRegistration()
        {
            return View();
        }
        public ActionResult SaveLeadRegistration(MemberRegistration model,string firstname,string EmailId, string PhoneNo, string txtAddress)
        {
            try
            {
                DataSet ds = new DataSet();
                model.Address = txtAddress;
                model.FirstName = firstname;
                model.EmailId = EmailId;
                model.MobileNumber = PhoneNo;
                model.Pk_DesignationId = "0";
                model.AddedBy = "1";
                model.OpCode = 1;
                ds = model.SaveLeadRegistration();
                if(ds!=null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
                {
                    model.flag = ds.Tables[0].Rows[0]["Flag"].ToString();
                    model.msg = ds.Tables[0].Rows[0]["msg"].ToString();
                }

                
            }
            catch(Exception)
            {
                throw;
            }

            return Json(model);
        }
  
       public ActionResult Logout()
        {
            return RedirectToAction("Login","Home");
        }
      
        public ActionResult UserRegistration()
        {
            return View();
        }
     
        public ActionResult Protfolio()
        {
            return View();
        }
    }
}