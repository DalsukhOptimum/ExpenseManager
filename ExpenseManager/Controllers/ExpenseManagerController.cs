using Library;
using Models;
using BL;

using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System;

namespace ExpenseManager.Controllers
{
    public class ExpenseManagerController : ApiController
    {
        // for Registering  user 
        [Route("api/ExpenseManager/RegisterUser")]
        [HttpPost]

        public SerializeResponse<UserEntity> RegisterUser([FromBody] UserEntity user)
        {
            InsertLog.WriteErrrorLog("api/ExpenseManager/RegisterUser=>API call starte");
            SerializeResponse<UserEntity> Response = new SerializeResponse<UserEntity>();
            try
            {
                UserReBL userbl = new UserReBL();
                return userbl.RegisterUSer(user);
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("api/ExpenseManager/RegisterUser=>Exception" + ex.Message + ex.StackTrace);
                Response.Message = ex.Message;
                return Response;
            }
           
           
        }

        //for adding expense
        [Route("api/ExpenseManager/AddExpense")]
        [HttpPost]

        public SerializeResponse<Expense> AddExpense([FromBody] Expense expense)
        {
            InsertLog.WriteErrrorLog("api/ExpenseManager/AddExpense=>API call starte");
            SerializeResponse<Expense> Response = new SerializeResponse<Expense>();
            try
            {
               
                AddExpenseBL addexpense = new AddExpenseBL();
                return addexpense.AddingExpense(expense);

            }
            catch(Exception ex)
            {
                InsertLog.WriteErrrorLog("api/ExpenseManager/AddExpense=>Exception" + ex.Message + ex.StackTrace);
                Response.Message = ex.Message;
                return Response;
            }
          
        }

        //for ahowing monthly expense 
        [Route("api/ExpenseManager/showMonthlyExpense")]
        [HttpPost]

        public SerializeResponse<MonthlyExpenseEntity> showMonthlyExpense([FromBody] MonthlyExpenseEntity expense)
        {
            InsertLog.WriteErrrorLog("api/ExpenseManager/showMonthlyExpense=>API call start");
            SerializeResponse<MonthlyExpenseEntity> Response = new SerializeResponse<MonthlyExpenseEntity>();
            try
            {
                MonthlyExpenseBL monthly = new MonthlyExpenseBL();
                return monthly.ShowMontlyExpense(expense);
            }
            catch(Exception ex)
            {
                InsertLog.WriteErrrorLog("api/ExpenseManager/AddExpense=>Exception" + ex.Message + ex.StackTrace);
                Response.Message = ex.Message;
                return Response;
            }
          
        }

        // for showing all the expenses if user not enter email otherwise showing that particular user expense
        [Route("api/ExpenseManager/ShowAllExpense")]
        [HttpPost]

        public SerializeResponse<Expense> showMonthlyExpense([FromBody] Expense expense)
        {
            InsertLog.WriteErrrorLog("api/ExpenseManager/ShowAllExpense=>API call starte");
            SerializeResponse<Expense> Response = new SerializeResponse<Expense>();
            try
            {
                ShowAllExpenseBL showAllExpenseBL = new ShowAllExpenseBL();
                return showAllExpenseBL.showAllExpense(expense);
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("api/ExpenseManager/ShowAllExpense" + ex.Message + ex.StackTrace);
                Response.Message = ex.Message;
                return Response;
            }

        }



    }
}
