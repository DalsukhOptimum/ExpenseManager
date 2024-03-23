using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{

    // model for showing data month wise 
    public class MonthlyExpenseEntity
    {

       
        public int Month { get; set; } 
        public int CreditAmount { get; set; }

        public int DebitAmount { get; set; }

        public int Total { get; set; }

        public string Email { get; set; }

        public int Balance {  get; set; }


    }
}
