using System.Web.Mvc;

namespace ConsultaMed_WEB.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/Index
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Home/EnviarSolicitacao
        public ActionResult EnviarSolicitacao()
        {
            return View();
        }

    }
}
