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
    public class AddExpenseBL
    {

        public SerializeResponse<Expense> AddingExpense(Expense objEntity)
        {

          
            InsertLog.WriteErrrorLog("AddExpenseBL=>AddingExpense=>Started");
            ConvertDataTable bl = new ConvertDataTable();
            SerializeResponse<Expense> objResponsemessage = new SerializeResponse<Expense>();

            DataSet ds = new DataSet();
            SqlDataProvider objSDP = new SqlDataProvider();
            string query = "SP_AddExpense";

            try
            {

                string Con_str = Connection.GetConnectionString();
                SqlParameter prm1 = objSDP.CreateInitializedParameter("@Comment", DbType.String, objEntity.Comment);
                SqlParameter prm2 = objSDP.CreateInitializedParameter("@CreditOrDebit", DbType.String, objEntity.CreditOrDebit);
                SqlParameter prm3 = objSDP.CreateInitializedParameter("@Amount", DbType.Int64, objEntity.Amount);
                SqlParameter prm4 = objSDP.CreateInitializedParameter("@Email", DbType.String, objEntity.Email);
               



                SqlParameter[] Sqlpara = { prm1, prm2, prm3, prm4};

                
               //if email is not valid so it will return an message not valid email id 
               //otherwise it will return an message that added succesfully and balance of that particular user.
                ds = SqlHelper.ExecuteDataset(Con_str, query, Sqlpara);
                if (ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    if(ds.Tables[0].Columns.Count == 1)
                    {
                        objResponsemessage.Message = Convert.ToString(ds.Tables[0].Rows[0]["Resultmessage"]);
                    }

                    else
                    {
                        //in this we sending only balance from database 
                        objResponsemessage.ArrayOfResponse = bl.ListConvertDataTable<Expense>(ds.Tables[0]);
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
                InsertLog.WriteErrrorLog("AddExpenseBL=>AddingExpense=>Exception" + ex.Message + ex.StackTrace);
            }
            return objResponsemessage;
        }

    }
}
