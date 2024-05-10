using System.Data.SqlClient;
using System.Data;

namespace SupBusiness.Models
{
    public class SiteMaster:Common
    {
        public string SiteId { get; set; }
        public string SiteName { get; set; }
        public DataSet SaveSiteMaster()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@SiteName",SiteName),
                                      new SqlParameter("@AddedBy",AddedBy),
                                      new SqlParameter("@Pk_Id",Pk_Id),
                                      new SqlParameter("@Opcode",OpCode),


                                  };

                DataSet ds = ConnectionManager.ExecuteQuery(ProcedureName.SaveSiteMaster, para);
                return ds;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
