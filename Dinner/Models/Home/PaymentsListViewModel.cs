using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dinner.Models.Home
{
    public class PaymentsListViewModel
    {
        public List<PaymentDetail> Items;
        public List<Debt> Debts;
        public List<User> Users;
    }
}