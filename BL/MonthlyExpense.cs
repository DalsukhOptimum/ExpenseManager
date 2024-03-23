using Library;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class MonthlyExpenseBL
    {

        public SerializeResponse<MonthlyExpenseEntity> ShowMontlyExpense(MonthlyExpenseEntity objEntity)
        {
            
            InsertLog.WriteErrrorLog("MonthlyExpenseBL=>ShowMontlyExpense=>Started");
            ConvertDataTable bl = new ConvertDataTable();
            SerializeResponse<MonthlyExpenseEntity> objResponsemessage = new SerializeResponse<MonthlyExpenseEntity>();

            DataSet ds = new DataSet();
            SqlDataProvider objSDP = new SqlDataProvider();
            string query = "SP_ShowExpenseMonthWise";

            try
            {

                string Con_str = Connection.GetConnectionString();
                SqlParameter prm1 = objSDP.CreateInitializedParameter("@Email", DbType.String, objEntity.Email);

                SqlParameter[] Sqlpara = { prm1};

                    //this will return monthly expense table
                    //and if not valid user so it will send a message that you are not valid user
                    //if column count is one that means it is returning an message of not valid user 
                ds = SqlHelper.ExecuteDataset(Con_str, query, Sqlpara);
                if (ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    if ( ds.Tables[0].Columns.Count == 1)
                    {
                        objResponsemessage.Message = Convert.ToString(ds.Tables[0].Rows[0]["ResponseMessage"]);
                    }

                    else
                    {
                     
                        objResponsemessage.ArrayOfResponse = bl.ListConvertDataTable<MonthlyExpenseEntity>(ds.Tables[0]);
                        if(ds?.Tables.Count > 1)
                        {
                            objResponsemessage.Message = "200|Data Found|Balance Is: " + Convert.ToString(ds.Tables[1].Rows[0]["ResponseMessage"]);
                        }
                       

                    }
                }
                else
                {
                    objResponsemessage.Message = "400|No Data Found";
                }

                

            }
            catch (Exception ex)
            {
                objResponsemessage.Message = "500|Exception Occurred";
                InsertLog.WriteErrrorLog("MonthlyExpenseBL=>ShowMontlyExpense=>Exception" + ex.Message + ex.StackTrace);
            }
            return objResponsemessage;
        }
    }
}




