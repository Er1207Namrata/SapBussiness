using System.Data;
using System.Data.SqlClient;

namespace SupBusiness.Models
{
    public class MemberRegistration : Common
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string Pk_DesignationId { get; set; }


        public DataSet SaveUserRegistration()
        {
            try
            {

                SqlParameter[] para = {

                                      new SqlParameter("@FirstName",FirstName),
                                      new SqlParameter("@LastName",LastName),
                                      new SqlParameter("@MiddleName",MiddleName),
                                      new SqlParameter("@MobileNo",MobileNumber),
                                      //new SqlParameter("@EmailId",EmailId),
                                      new SqlParameter("@AddedBy",AddedBy),
                                      new SqlParameter("@OpCode",OpCode)

                                  };

                DataSet ds = ConnectionManager.ExecuteQuery(ProcedureName.saveUserregisraion, para);
                return ds;


            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public DataSet SaveLeadRegistration()
        {
            try
            {

                SqlParameter[] para = {

                                      new SqlParameter("@LeadName",FirstName),
                                      new SqlParameter("@EmailId",EmailId),
                                      new SqlParameter("@MobileNo",MobileNumber),
                                      new SqlParameter("@Address",Address),
                                      new SqlParameter("@Fk_DesignationId",Pk_DesignationId),
                                      new SqlParameter("@OpCode",OpCode),
                                      new SqlParameter("@AddedBy",AddedBy)

                                  };

                DataSet ds = ConnectionManager.ExecuteQuery(ProcedureName.saveLeadregisraion, para);
                return ds;


            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}

