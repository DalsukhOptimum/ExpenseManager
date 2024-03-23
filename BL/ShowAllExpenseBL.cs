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
    public class ShowAllExpenseBL
    {

        public SerializeResponse<Expense> showAllExpense (Expense objEntity)
        {

            InsertLog.WriteErrrorLog("ShowAllExpenseBL=>showAllExpense=>Started");
            ConvertDataTable bl = new ConvertDataTable();
            SerializeResponse<Expense> objResponsemessage = new SerializeResponse<Expense>();

            DataSet ds = new DataSet();
            SqlDataProvider objSDP = new SqlDataProvider();
            string query = "SP_ShowAllExpenses";

            try
            {

                string Con_str = Connection.GetConnectionString();
                SqlParameter prm1 = objSDP.CreateInitializedParameter("@Email", DbType.String, objEntity.Email);

                SqlParameter[] Sqlpara = { prm1 };

               
               //if we don't send email so it will return an whole expense table
               //if we send email so it will return an expense of that particular user
               //if we send an emailid and it is not valid so it will return an message that not valid  
                ds = SqlHelper.ExecuteDataset(Con_str, query, Sqlpara);
                if (ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    
         
                    if (ds.Tables[0].Columns.Count == 1)
                    {
                        objResponsemessage.Message = Convert.ToString(ds.Tables[0].Rows[0]["ResponseMessage"]);
                    }
                    else
                    {
                        objResponsemessage.ArrayOfResponse = bl.ListConvertDataTable<Expense>(ds.Tables[0]);
                        objResponsemessage.Message = "200|Data Found";

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
                InsertLog.WriteErrrorLog("ShowAllExpenseBL=>ShowMontlyExpense=>Exception" + ex.Message + ex.StackTrace);
            }
            return objResponsemessage;
        }
    }
}
