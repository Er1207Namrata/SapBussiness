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
        public IActionResult UserRegistration(MemberRegistration model, string save,string Id,string delid)
        {
            try
            {
                DataSet ds = new DataSet();
                if(!string.IsNullOrEmpty(Id))
                {
                    model.Pk_Id = Id;
                    model.OpCode = 4;
                    ds = model.SaveUserRegistration();
                    if(ds!= null&&ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
                    {
                        model.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                        model.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                        model.MiddleName = ds.Tables[0].Rows[0]["MiddleName"].ToString();
                        model.MobileNumber = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                        model.Pk_Id = ds.Tables[0].Rows[0]["Pk_Id"].ToString();
                    }
                }
                if(!string.IsNullOrEmpty(delid))
                {
                    model.Pk_Id = delid;
                    model.OpCode = 3;
                    model.AddedBy= HttpContext.Session.GetString("Fk_MemId");
                    ds = model.SaveUserRegistration();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["flag"].ToString() == "1")
                        {
                            TempData["Message"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                            return RedirectToAction("UserRegistrationList", "Admin");
                        }
                    }
                }
                if (!string.IsNullOrEmpty(save))
                {
                    model.AddedBy = HttpContext.Session.GetString("Fk_MemId");
                    if(save=="Save")
                    {
                        model.OpCode = 1;
                    }
                    else
                    {
                        model.OpCode = 2;
                    }
                    
                    ds = model.SaveUserRegistration();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["flag"].ToString() == "1")
                        {
                            TempData["Message"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                            return RedirectToAction("UserRegistrationList", "Admin");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(model);
        }
        public IActionResult SiteMaster(SiteMaster model, string Save, string editId)
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
                    model.AddedBy = HttpContext.Session.GetString("Fk_MemId");
                    ds = model.SaveSiteMaster();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["flag"].ToString() == "1")
                        {
                            TempData["Msg"] = ds.Tables[0].Rows[0]["msg"].ToString();
                            return RedirectToAction("SiteMasterList", "Admin");
                        }
                        else
                        {
                            TempData["Msg"] = ds.Tables[0].Rows[0]["msg"].ToString();
                            return RedirectToAction("SiteMasterList", "Admin");
                        }
                    }

                }
                if (!string.IsNullOrEmpty(editId))
                {
                    model.Pk_Id = editId;
                    ds = model.SaveSiteMaster();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        model.SiteName = ds.Tables[0].Rows[0]["SiteName"].ToString();
                        model.Pk_Id = ds.Tables[0].Rows[0]["Pk_Id"].ToString();
                    }
                }
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
        public IActionResult SiteMasterList(SiteMaster model, string Save,string Delete)
        {
            try
            {
                DataSet ds = new DataSet();
                if(!string.IsNullOrEmpty(Delete))
                {
                    model.Pk_Id = model.SiteId;
                    
                    model.AddedBy= HttpContext.Session.GetString("Fk_MemId");
                    model.OpCode = 3;
                    ds = model.SaveSiteMaster();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["flag"].ToString() == "1")
                        {
                            TempData["Msg"] = ds.Tables[0].Rows[0]["msg"].ToString();
                            return RedirectToAction("SiteMasterList", "Admin");
                        }
                        else
                        {
                            TempData["Msg"] = ds.Tables[0].Rows[0]["msg"].ToString();
                            return RedirectToAction("SiteMasterList", "Admin");
                        }
                    }

                }
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
        public IActionResult UserTaskList(UserTask model, string Save, string DelId)

        {
            try
            {
                DataSet ds = new DataSet();
                if (!string.IsNullOrEmpty(DelId))
                {
                    model.Pk_Id = DelId;
                    model.OpCode = 3;
                    model.AddedBy = HttpContext.Session.GetString("Fk_MemId"); 
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
            catch (Exception)
            {
                throw;
            }
            return View(model);
        }

        public ActionResult AddUserTask(UserTask model, string Save, string Id, string siteId)
        {
            try
            {
                DataSet ds = new DataSet();
                List<SelectListItem> ddluser = new List<SelectListItem>();
                model.OpCode = 3;
                ds = model.GetMasterData();
                ddluser.Add(new SelectListItem { Text = "--Select User--", Value = "0" });
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ddluser.Add(new SelectListItem { Text = ds.Tables[0].Rows[i]["Name"].ToString(), Value = ds.Tables[0].Rows[i]["Id"].ToString() });
                    }
                }
                ViewBag.ddluser = ddluser;
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
                    model.StartDate = string.IsNullOrEmpty(model.StartDate) ? null : Common.ConvertToSystemDate(model.StartDate, "dd/MM/yyyy");
                    model.EndDate = string.IsNullOrEmpty(model.EndDate) ? null : Common.ConvertToSystemDate(model.EndDate, "dd/MM/yyyy");
                    model.AddedBy = HttpContext.Session.GetString("Fk_MemId");
                    ds = model.SaveTaskList();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["flag"].ToString() == "1")
                        {
                            TempData["Msg"] = ds.Tables[0].Rows[0]["msg"].ToString();
                            if(Save=="Save")
                            {
                                Save = null;
                                return RedirectToAction("AddUserTask", "Admin");
                            }
                            else
                            {
                                
                                return RedirectToAction("UserTaskList", "Admin");
                            }

                        }
                        else
                        {
                            TempData["Msg"] = ds.Tables[0].Rows[0]["flag"].ToString();
                        }
                    }
                }
                if (!string.IsNullOrEmpty(Id))
                {
                    model.Pk_Id = Id;
                    model.OpCode = 4;
                    ds = model.SaveTaskList();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        model.TaskName = ds.Tables[0].Rows[0]["TaskName"].ToString();
                        model.TaskDetails = ds.Tables[0].Rows[0]["TaskDetails"].ToString();
                         model.LoginId = ds.Tables[0].Rows[0]["FK_UserId"].ToString();
                        model.SiteName = ds.Tables[0].Rows[0]["SiteName"].ToString();
                        model.Pk_Id = ds.Tables[0].Rows[0]["Pk_Id"].ToString();
                        model.SiteId = ds.Tables[0].Rows[0]["Fk_SiteId"].ToString();
                        model.EndDate = ds.Tables[0].Rows[0]["EndDate"].ToString();
                        model.StartDate = ds.Tables[0].Rows[0]["StartDate"].ToString();
                        model.TotalDays = ds.Tables[0].Rows[0]["totaldays"].ToString();
                        ViewBag.SiteName = ds.Tables[0].Rows[0]["SiteName"].ToString();
                        
                        TempData["Fk_UserId"] = ds.Tables[0].Rows[0]["FK_UserId"].ToString();
                    }
                }

                if (!string.IsNullOrEmpty(siteId))
                {
                    model.Pk_Id = siteId;
                    model.OpCode = 4;
                    ds = model.GetSiteList();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ViewBag.SiteName = ds.Tables[0].Rows[0]["SiteName"].ToString();
                        model.SiteId = ds.Tables[0].Rows[0]["Pk_Id"].ToString();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return View(model);
        }
        public ActionResult GetUserDetails(UserTask model, string LoginId)
        {
            try
            {

                DataSet ds = new DataSet();
                model.LoginId = LoginId;
                ds = model.GetUserDetails();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
            }
            catch (Exception)
            {

            }
            return Json(model);
        }


        public ActionResult UserRegistrationList(MemberRegistration model)
        {
            try
            {
                DataSet ds = new DataSet();
                model.OpCode = 4;
                ds = model.SaveUserRegistration();
                model.dtDetails = ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
            return View(model);
        }

        public ActionResult TopUp(MemberTopUp model, string Save)
        {
            try
            {
                DataSet dataSet = new DataSet();
                #region ddlPaymentMode
                List<SelectListItem> ddlpaymentmode = new List<SelectListItem>();
                model.OpCode = 1;
                dataSet = model.GetMasterData();
                ddlpaymentmode.Add(new SelectListItem { Text = "--Select Payment mode--", Value = "0" });
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        ddlpaymentmode.Add(new SelectListItem { Text = dataSet.Tables[0].Rows[i]["Name"].ToString(), Value = dataSet.Tables[0].Rows[i]["Id"].ToString() });
                    }
                }
                ViewBag.ddlpaymentmode = ddlpaymentmode;
                #endregion ddlPaymentmode
                #region ddlUser
                List<SelectListItem> ddluser = new List<SelectListItem>();
                model.OpCode = 3;
                dataSet = model.GetMasterData();
                ddluser.Add(new SelectListItem { Text = "--Select User--", Value = "0" });
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        ddluser.Add(new SelectListItem { Text = dataSet.Tables[0].Rows[i]["Name"].ToString(), Value = dataSet.Tables[0].Rows[i]["Id"].ToString() });
                    }
                }
                ViewBag.ddluser = ddluser;
                #endregion ddlUser
                if (!string.IsNullOrEmpty(Save))
                {
                    DataSet ds = new DataSet();
                    model.AddedBy = HttpContext.Session.GetString("Fk_MemId");
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
            catch (Exception ex)
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

        public ActionResult UpdateTaskstatus(UserTask model, string pk_id,string FK_UserId)
        {
            try
            {
                DataSet ds = new DataSet();
                model.OpCode = 5;
                model.Pk_Id = pk_id;
                model.LoginId = FK_UserId;
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
                if (!string.IsNullOrEmpty(Save))
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
            catch (Exception ex)
            {
                throw;
            }
            return View(model);
        }


    }
}
