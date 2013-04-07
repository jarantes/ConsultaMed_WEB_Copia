using System;
using System.Web.Mvc;
using System.Web.Security;
using ConsultaMed_WEB.Models;

namespace ConsultaMed_WEB.Controllers
{
    public class PerfilController : Controller
    {
        //
        // GET: /Perfil/Gerenciar
        [Authorize(Roles = "Administrador")]
        public ActionResult Gerenciar()
        {
            return View();
        }
        //
        // POST: /Perfil/AddPerfil
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Gerenciar(Role model)
        {
            try
            {
                if (Roles.RoleExists(model.RoleName))
                {
                    Roles.DeleteRole(model.RoleName);
                }
                Roles.CreateRole(model.RoleName);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Não foi possível adicionar o novo perfil!");
                return View(model);
            }
            TempData["Mensagem"] = "Perfil adiciondado com sucesso";
            return RedirectToAction("Gerenciar");
        }

    }
}
