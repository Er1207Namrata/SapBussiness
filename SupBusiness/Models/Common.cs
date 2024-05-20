using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace SupBusiness.Models
{
    public class Common
    {
        public string AddedBy { get; set; }
        public string Fk_MemId { get; set; }
        public string Name { get; set; }
        public string LoginId { get; set; }
        public string Pk_Id { get; set; }
        public int OpCode { get; set; }
        public string flag { get; set; }
        public string msg { get; set; }
        public string Value { get; set; }
        public DataTable dtDetails { get; set; }
        public DataTable dtDetails1 { get; set; }
        public DataSet GetMasterData()
        {
            try
            {

                SqlParameter[] para = {

                                      new SqlParameter("@Values",Value),
                                      new SqlParameter("@OpCode",OpCode)
                                  };

                DataSet ds = ConnectionManager.ExecuteQuery(ProcedureName.GetMasterData, para);
                return ds;


            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static string ConvertToSystemDate(string InputDate, string InputFormat)
        {
            string[] DatePart = InputDate.Split(new string[] { "-", @"/" }, StringSplitOptions.None);

            string DateString;
            if (InputFormat == "dd-MMM-yyyy" || InputFormat == "dd/MMM/yyyy" || InputFormat == "dd/MM/yyyy" || InputFormat == "dd-MM-yyyy" || InputFormat == "DD/MM/YYYY" || InputFormat == "dd/mm/yyyy")
            {
                string Day = DatePart[0];
                string Month = DatePart[1];
                string Year = DatePart[2];

                if (Month.Length > 2)
                    DateString = InputDate;
                else
                    DateString = Year + "-" + Month + "-" + Day;
            }
            else if (InputFormat == "MM/dd/yyyy" || InputFormat == "MM-dd-yyyy")
            {
                DateString = InputDate;
            }
            else
            {
                throw new Exception("Invalid Date");
            }

            try
            {
                //Dt = DateTime.Parse(DateString);
                //return Dt.ToString("MM/dd/yyyy");
                return DateString;
            }
            catch
            {
                throw new Exception("Invalid Date");
            }
        }
    }

    public class ProcedureName
    {
        public static string? AdminLogin = "AdminLogin";
        public static string? saveUserregisraion = "saveUserregisraion";
        public static string? GetUserDetails = "GetUserDetails";
        public static string? SaveUserTask = "SaveUserTask";
        public static string? SaveSiteMaster = "SaveSiteMaster";
        public static string? saveLeadregisraion = "saveLeadregisraion";
        public static string? GetMasterData = "GetMasterData";
        public static string? GetDesignationMaster = "GetDesignationMaster";
        public static string? SaveTopUp = "SaveTopUp";
        public static string? SavePaymentMode = "SavePaymentMode";
      
    }
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
         
    }
}
