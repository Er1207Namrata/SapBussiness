using System.Data;
using System.Data.SqlClient;

namespace SupBusiness.Models
{
    public class MemberTopUp : Common
    {
        public string Amount { get; set; }
        public string Document { get; set; }
        public string Recipt { get; set; }
        public string Pk_PaymentId { get; set; }
        public string bankAccountNo { get; set; }
        public string BankName { get; set; }
        public string IFSCCode { get; set; }
        public string PaymentMode { get; set; }

        public DataSet SaveTopUp()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@FK_MemId",Fk_MemId),
                                      new SqlParameter("@FK_PaymentId",Pk_PaymentId),
                                      new SqlParameter("@Amount",Amount),
                                      new SqlParameter("@BankName",BankName),
                                      new SqlParameter("@bankAccountNo",bankAccountNo),
                                      new SqlParameter("@IFScCode",IFSCCode),
                                      new SqlParameter("@Doc",Document),
                                      new SqlParameter("@AddedBy",AddedBy)


                                  };

                DataSet ds = ConnectionManager.ExecuteQuery(ProcedureName.SaveTopUp, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetPaymentMode()
        {
            try
            {
                SqlParameter[] para = {

                                      new SqlParameter("@Pk_Id",Pk_Id),
                                      new SqlParameter("@PaymentMode",PaymentMode),
                                      new SqlParameter("@OpCode",OpCode),
                                      new SqlParameter("@AddedBy",AddedBy)


                                  };

                DataSet ds = ConnectionManager.ExecuteQuery(ProcedureName.SavePaymentMode, para);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
