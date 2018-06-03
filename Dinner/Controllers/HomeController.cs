using Dinner.Global.Attributes;
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
        public AuthService AuthService { get; set; }

        [CustomAuthorize]
        public ActionResult Index()
        {
            var model = new PaymentsListViewModel {
                Items = Service.Payments(),
                Debts = null,
                Users = Service.GetUsers().ToList()
            };

            Service.CalculateStat();
            return View(model);
        }

        [HttpPost]
        [CustomAuthorize]
        public ActionResult AddPayment(PaymentViewModel payment)
        {
            int userId = (int)System.Web.HttpContext.Current.Items["UserId"];
            Service.AddPayment(userId, payment);
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

            var token = AuthService.Authorize(user.Id);
            //FormsAuthentication.SetAuthCookie(user.Name, false);
            Response.Cookies.Add(new HttpCookie("DinnerProAuth", token)
            {
                Path = "/",
#if (!DEBUG)
                Secure = true,
#endif
                HttpOnly = true,
                Domain = Request.Url.Host,
                Expires = DateTime.Now.AddDays(7)
            });

            return RedirectToAction("Index");
        }

        public ActionResult Payments()
        {
            var model = new PaymentsListViewModel
            {
                Items = Service.Payments(),
                Debts = null,
                Users = Service.GetUsers().ToList()
            };

            Service.CalculateStat();
            return View(model);
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