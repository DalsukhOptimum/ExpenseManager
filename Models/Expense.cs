using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    //model for fetching and storing expens data
    public class Expense
    {

        public string Comment { get; set; }
        public string CreditOrDebit { get; set; }
        public int Amount { get; set; }
        public string ExpenseTime { get; set; }
        public int balance { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
 

    }
}
