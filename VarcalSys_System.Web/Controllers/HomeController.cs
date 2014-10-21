using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VarcalSys_System.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public HomeController()
        {

        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sobre()
        {
            return View();
        }

        public ActionResult Portifolio()
        {
            return View();
        }

        public ActionResult CSharp()
        {
            return View();
        }

        public ActionResult AspNetMVC()
        {
            return View();
        }

        public ActionResult Cursos()
        {
            return View();
        }
    }
}
