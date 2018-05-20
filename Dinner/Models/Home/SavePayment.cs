using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dinner.Models.Home
{
    public class SavePayment
    {
        public DateTime date;
        public int payerId;
        public SaveUserPayment[] payments;
    }

    public class SaveUserPayment
    {
        public int userId;
        public decimal amount;
    }
}