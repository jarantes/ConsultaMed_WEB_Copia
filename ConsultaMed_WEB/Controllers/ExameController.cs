using System;
using System.Web.Mvc;
using ConsultaMed_WEB.Models;
using ConsultaMed_WEB.Models.Repositorio;

namespace ConsultaMed_WEB.Controllers
{
    public class ExameController : Controller
    {
        //OBJETOS:
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        //
        // GET: /Exame/Adicionar
        [Authorize(Roles = "Medico")]
        public ActionResult Adicionar()
        {
            TempData["Mensagem"] = Session["Mensagem"];
            Session.Remove("Mensagem");
            return View();
        }

        //
        // POST: /Exame/Adicionar
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Medico")]
        public ActionResult Adicionar(Exame model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.ExameRepositorio.Insert(model);
                    _unitOfWork.Save();

                    Session.Add("Mensagem", "Exame adiciondado com sucesso");
                    return RedirectToAction("Adicionar");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Não foi possível adicionar o novo tipo de exame");
                }
                finally
                {
                    _unitOfWork.Dispose();
                }
            }
            return View(model);
        }

    }
}
