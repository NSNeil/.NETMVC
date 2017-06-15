using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; 

namespace BusinessLogicLayer.Models
{
    public class AccountantViewModels
    {
        private DataTable forDisplay;
        public DataTable ForDisplay
        {
            get;
            set;
        }

        private Decimal totalForAllDistricts; 
        public Decimal TotalForAllDistricts
        {
            get;
            set; 
        }
    }
}
