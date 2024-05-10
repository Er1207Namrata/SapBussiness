using NuGet.Protocol.Plugins;
using System.Data;
using System.Data.SqlClient;

namespace SupBusiness.Models
{
    public class Login:Common
    {
        public string LoginId { get; set; }
        public string Password { get; set; }

        public DataSet GetAdminLogin()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@LoginId",LoginId),
                                      new SqlParameter("@Password",Password),

                                  };

                DataSet ds = ConnectionManager.ExecuteQuery(ProcedureName.AdminLogin, para);
                return ds;

            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
