using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SupBusiness.Models;
using System.Data;
using System.Reflection;


namespace SupBusiness.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult UserRegistration(MemberRegistration model,string save)
        {
            try
            {
                DataSet ds = new DataSet();

                if (!string.IsNullOrEmpty(save))
                {
                    model.AddedBy = "1";
                    model.OpCode = 1;
                    ds = model.SaveUserRegistration();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["flag  ehp[0"].ToString() == "1")
                        {
                            TempData["Message"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }
            return View();
        }
        public IActionResult SiteMaster(SiteMaster model,string Save)
        {
            try
            {
                DataSet ds = new DataSet();
                if(!string.IsNullOrEmpty(Save))
                {
                    if(Save=="Save")
                    {
                        model.OpCode = 1;
                    }
                    else
                    {
                        model.OpCode = 2;
                    }
                    model.AddedBy = "1";
                    ds = model.SaveSiteMaster();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["flag"].ToString() == "1")
                        {
                            TempData["Msg"] = ds.Tables[0].Rows[0]["msg"].ToString();
                        }
                        else
                        {
                            TempData["Msg"] = ds.Tables[0].Rows[0]["msg"].ToString();
                        }
                    }
                    
                }
                model.OpCode = 4;
                ds = model.SaveSiteMaster();
                model.dtDetails = ds.Tables[0];
            }
            catch(Exception)
            {
                throw;
            }
            return View(model);
        }
        public IActionResult SiteMasterList(SiteMaster model, string Save)
        {
            try
            {
                DataSet ds = new DataSet();
                
                model.OpCode = 4;
                ds = model.SaveSiteMaster();
                model.dtDetails = ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
            return View(model);
        }
        public IActionResult UserTaskList(UserTask model,string Save,string DelId)
        
        {
            try
            {
                DataSet ds = new DataSet();
                if (!string.IsNullOrEmpty(DelId))
                {
                    model.Pk_Id = DelId;
                    model.OpCode = 3;
                    model.AddedBy = "1";
                    ds = model.SaveTaskList();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["flag"].ToString() == "1")
                        {
                            TempData["Msg"] = ds.Tables[0].Rows[0]["msg"].ToString();
                            return RedirectToAction("UserTaskList", "Admin");
                        }
                        else
                        {
                            TempData["Msg"] = ds.Tables[0].Rows[0]["flag"].ToString();
                            return RedirectToAction("UserTaskList", "Admin");
                        }
                    }
                }
            
                
                model.OpCode = 4;
                ds = model.SaveTaskList();
                model.dtDetails = ds.Tables[0];
                model.dtDetails1 = ds.Tables[1];
                
            }
            catch(Exception)
            {
                throw;
            }
            return View(model);
        }
        
        public ActionResult  AddUserTask(UserTask model, string Save,string Id)
        {
            try
            {
                DataSet ds = new DataSet();
                if (!string.IsNullOrEmpty(Save))
                {
                    if (Save == "Save")
                    {
                        model.OpCode = 1;
                    }
                    else
                    {
                        model.OpCode = 2;
                    }

                    model.AddedBy = "1";
                    ds = model.SaveTaskList();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["flag"].ToString() == "1")
                        {
                            TempData["Msg"] = ds.Tables[0].Rows[0]["msg"].ToString();
                        }
                        else
                        {
                            TempData["Msg"] = ds.Tables[0].Rows[0]["flag"].ToString();
                        }
                    }
                }
                if(!string.IsNullOrEmpty(Id))
                {
                    model.Pk_Id = Id;
                    model.OpCode = 4;
                    ds = model.SaveTaskList();
                    if(ds!=null&&ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
                    {
                        model.TaskName = ds.Tables[0].Rows[0]["TaskName"].ToString();
                        model.TaskDetails = ds.Tables[0].Rows[0]["TaskDetails"].ToString();
                       // model.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        //model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                        model.Pk_Id = ds.Tables[0].Rows[0]["Pk_Id"].ToString();
                    }
                }
            }
            catch(Exception)
            {
                throw;
            }
            return View(model);
        }
        public ActionResult GetUserDetails(UserTask model,string LoginId)
        {
            try
            {
                
                DataSet ds= new DataSet();
                model.LoginId = LoginId;
                ds=model.GetUserDetails();
                if(ds!=null && ds.Tables.Count>0 && ds.Tables [0].Rows.Count > 0)
                {
                    model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
            }
            catch(Exception)
            {

            }
            return Json(model);
        }


        public ActionResult UserRegistrationList(MemberRegistration model)
        {
            try
            {
                DataSet ds = new DataSet();
                model.OpCode=4;
                ds = model.SaveUserRegistration();
                model.dtDetails = ds.Tables[0];
            }
            catch(Exception)
            {
                throw;
            }
            return View(model);
        }
        
        public ActionResult TopUp(MemberTopUp model,string Save)
        {
            try
            {
                DataSet dataSet = new DataSet();
                List<SelectListItem> ddldesignation = new List<SelectListItem>();
                model.OpCode = 2;
                dataSet = model.GetMasterData();
                ddldesignation.Add(new SelectListItem { Text = "--Select Designation--", Value = "0" });
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        ddldesignation.Add(new SelectListItem { Text = dataSet.Tables[0].Rows[0]["Name"].ToString(), Value = dataSet.Tables[0].Rows[0]["Id"].ToString() });
                    }
                }
                ViewBag.ddldesignation = ddldesignation;
                if (!string.IsNullOrEmpty(Save))
                {
                    DataSet ds = new DataSet();
                    ds = model.SaveTopUp();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Flag"].ToString() == "1")
                        {
                            TempData["Msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                        }
                        else
                        {
                            TempData["Msg"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                        }
                    }
                }
              
            }
            catch(Exception ex)
            {
                throw;
            }
            return View(model);
        }

        public IActionResult DesignationMaster(Designation model, string Save)
        {
            try
            {
                DataSet ds = new DataSet();
                if (!string.IsNullOrEmpty(Save))
                {
                    if (Save == "Save")
                    {
                        model.OpCode = 1;
                    }
                    else
                    {
                        model.OpCode = 2;
                    }
                    model.AddedBy = "1";
                    ds = model.SaveDesignationMaster();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["flag"].ToString() == "1")
                        {
                            TempData["Msg"] = ds.Tables[0].Rows[0]["msg"].ToString();
                        }
                        else
                        {
                            TempData["Msg"] = ds.Tables[0].Rows[0]["msg"].ToString();
                        }
                    }

                }
                model.OpCode = 4;
                ds = model.SaveDesignationMaster();
                model.dtDetails = ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
            return View(model);
        }

        public ActionResult LeadRegistrationList(MemberRegistration model)
        {
            try
            {
                DataSet ds = new DataSet();
                model.OpCode = 4;
                ds = model.SaveLeadRegistration();
                model.dtDetails = ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
            return View(model);
        }

        public ActionResult UpdateTaskstatus(UserTask model, string pk_id)
        {
            try
            {
                DataSet ds = new DataSet();
                model.OpCode = 5;
                model.Pk_Id = pk_id;
                model.AddedBy = HttpContext.Session.GetString("Fk_MemId");
                ds = model.SaveTaskList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.msg = ds.Tables[0].Rows[0]["msg"].ToString();
                    model.flag = ds.Tables[0].Rows[0]["flag"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Json(model);
        }
       
        public ActionResult PaymentModeMaster(MemberTopUp model, string Save)
        {
            try
            {
                DataSet ds = new DataSet();
                if(!string.IsNullOrEmpty(Save))
                {
                    model.AddedBy = HttpContext.Session.GetString("Fk_MemId");
                    model.OpCode = 1;
                    ds = model.GetPaymentMode();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["flag"].ToString() == "1")
                        {
                            TempData["Msg"] = ds.Tables[0].Rows[0]["msg"].ToString();
                            
                        }
                        else
                        {
                            TempData["Msg"] = ds.Tables[0].Rows[0]["flag"].ToString();
                           
                        }
                    }
                }
                model.OpCode = 4;
                ds = model.GetPaymentMode();
                model.dtDetails = ds.Tables[0];
            }
            catch(Exception ex)
            {
                throw;
            }
            return View(model);
        }
       
    
    }
}
