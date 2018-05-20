using Dinner.Models.Home;
using Dinner.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Dinner.Controllers
{
    public class HomeController : Controller
    {
        public HomeService Service { get; set; }

        [Authorize]
        public ActionResult Index()
        {
            return View(new PaymentsListViewModel { Items = Service.Payments(), Debts = Service.GetDebts(), Users = Service.GetUsers().ToList() });
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddPayment(PaymentViewModel payment)
        {
            Service.AddPayment(System.Web.HttpContext.Current.User.Identity.Name, payment);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = Service.GetUsers().FirstOrDefault(it => it.Name.ToLower() == model.Login?.ToLower() && it.Password == model.Password);
            if (user == null)
                return View(model);

            FormsAuthentication.SetAuthCookie(user.Name, true);

            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}