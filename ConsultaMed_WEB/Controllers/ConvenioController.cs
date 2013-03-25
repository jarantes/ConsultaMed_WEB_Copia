using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConsultaMed_WEB.Models;
using ConsultaMed_WEB.Models.Repositorio;

namespace ConsultaMed_WEB.Controllers
{
    public class ConvenioController : Controller
    {
        //OBJETOS:
        public string Sucesso;
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        //
        // GET: /Convenio/Adicionar
        [Authorize(Roles = "Administrador")]
        public ActionResult Adicionar()
        {
            TempData["Mensagem"] = Session["Mensagem"];
            Session.Remove("Mensagem");
            return View();
        }

        //
        // POST: /Convenio/Adicionar
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Adicionar(Convenio model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.ConvenioRepositorio.Insert(model);
                    _unitOfWork.Save();
                    _unitOfWork.Dispose();

                    Session.Add("Mensagem", "Convenio adicionado com sucesso");
                    return RedirectToAction("Adicionar");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Não foi possível adicionar o convênio");
            }
            return View(model);
        }

        //
        // GET: /Convenio/Deletar
        [Authorize(Roles = "Administrador")]
        public ActionResult Deletar(int id)
        {
            try
            {
                //deletando Convênio
                _unitOfWork.ConvenioRepositorio.Delete(id);
                _unitOfWork.Save();
                _unitOfWork.Dispose();

                Session.Add("DeleteConvenio", "Convênio removido com sucesso");
                return RedirectToAction("Gerenciar");
            }
            catch (Exception)
            {
                Session.Add("Erro", "Não foi possível remover o convênio");
            }
            return RedirectToAction("Gerenciar");
        }

        //
        // GET: /Convenio/Gerenciar
        [Authorize(Roles = "Administrador")]
        public ActionResult Gerenciar()
        {
            TempData["Mensagem"] = Session["DeleteConvenio"];
            TempData["Erro"] = Session["Erro"];
            //limpar sessão
            Session.Remove("DeleteConvenio");
            Session.Remove("Erro");

            var model = _unitOfWork.ConvenioRepositorio.Get();
            _unitOfWork.Dispose();
            return View(model);
        }

       

    }
}
