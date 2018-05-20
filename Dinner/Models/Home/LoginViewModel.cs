using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dinner.Models.Home
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "обязательное поле")]
        public string Login { get; set; }
        [Required(ErrorMessage = "обязательное поле")]
        public string Password { get; set; }
}
}