using System.Data;
using System.Data.SqlClient;

namespace SupBusiness.Models
{
    public class UserTask:Common
    {
        public string TaskName { get; set; }
        public string TaskDetails { get; set; }

        public DataSet GetUserDetails()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@LoginId",LoginId)
                                     

                                  };

                DataSet ds = ConnectionManager.ExecuteQuery(ProcedureName.GetUserDetails, para);
                return ds;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SaveTaskList()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@LoginId",LoginId),
                                      new SqlParameter("@TaskName",TaskName),
                                      new SqlParameter("@TaskDetails",TaskDetails),
                                      new SqlParameter("@AddedBy",AddedBy),
                                      new SqlParameter("@Pk_Id",Pk_Id),
                                      new SqlParameter("@Opcode",OpCode),
                                     

                                  };

                DataSet ds = ConnectionManager.ExecuteQuery(ProcedureName.SaveUserTask, para);
                return ds;

            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
