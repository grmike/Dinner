using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dinner.Models.Home
{
    public class PaymentDetail
    {
        public DateTime Date;
        public User Payer;
        public UserPayment[] Payments;

        public string PaymentsText {
            get {
                return string.Join(", ", Payments.Select(it => $"{it.User.Name}: {it.Amount}"));
            }
        }
    }
}