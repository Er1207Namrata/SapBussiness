using Microsoft.AspNetCore.Mvc;
using SupBusiness.Models;
using System.Data;
using System.Reflection;

namespace SupBusiness.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult TaskList(UserTask model)
        {
            try
            {
                DataSet ds = new DataSet();
                model.Pk_Id = HttpContext.Session.GetString("Fk_MemId");
                model.OpCode = 4;
                ds = model.SaveTaskList();
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

        public IActionResult UserProfile()
        {
            return View();
        }

    }
}
