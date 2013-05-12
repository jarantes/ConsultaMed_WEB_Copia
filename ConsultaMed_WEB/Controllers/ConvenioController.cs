using System;
using System.Linq;
using System.Web.Mvc;
using ConsultaMed_WEB.Models;
using ConsultaMed_WEB.Models.Repositorio;

namespace ConsultaMed_WEB.Controllers
{
    public class ConvenioController : Controller
    {
        //OBJETOS:
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

                    Session.Add("Mensagem", "Convenio adicionado com sucesso");
                    return RedirectToAction("Adicionar");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Não foi possível adicionar o convênio");
            }
            finally
            {
                _unitOfWork.Dispose();
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

                Session.Add("DeleteConvenio", "Convênio removido com sucesso");
                return RedirectToAction("Gerenciar");
            }
            catch (Exception)
            {
                Session.Add("Erro", "Não foi possível remover o convênio");
            }
            finally
            {
                _unitOfWork.Dispose();
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

        //
        // GET: /Convenio/Listar
        [Authorize(Roles = "Medico, RespClinica")]
        public ActionResult Listar()
        {
            try
            {
                var usuario = _unitOfWork.UsuarioRepositorio.Get(m=>m.UserName == User.Identity.Name).First();

                var clinicas = _unitOfWork.ClinicaRepositorio.Get(m=>m.ClinicaId == usuario.ClinicaId);
                var model = clinicas.SelectMany(m => m.Convenios);
                return View(model);
            }
            catch (Exception)
            {
                Session.Add("Erro", "Não foi possível realizar a pesquisa");
                TempData["Erro"] = Session["Erro"];
                Session.Remove("Erro");
                return View();
            }
        }
    }
}
