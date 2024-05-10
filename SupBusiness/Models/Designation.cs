using System.Data.SqlClient;
using System.Data;

namespace SupBusiness.Models
{
    public class Designation:Common
    {
        public string DesignationName { get; set; }
        public DataSet SaveDesignationMaster()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@DesignationName",DesignationName),
                                      new SqlParameter("@AddedBy",AddedBy),
                                      new SqlParameter("@Pk_Id",Pk_Id),
                                      new SqlParameter("@Opcode",OpCode),


                                  };

                DataSet ds = ConnectionManager.ExecuteQuery(ProcedureName.GetDesignationMaster, para);
                return ds;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
