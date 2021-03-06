﻿using System;
using System.Linq;
using System.Web.Mvc;
using ConsultaMed_WEB.Models;
using ConsultaMed_WEB.Models.Repositorio;

namespace ConsultaMed_WEB.Controllers
{
    public class ClinicaController : Controller
    {
        //OBJETOS:
        public string Sucesso;
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        //
        //GET : /Clinica/Adicionar
        [Authorize(Roles = "Administrador")]
        public ActionResult Adicionar()
        {
            TempData["Mensagem"] = Session["Mensagem"];
            Session.Remove("Mensagem");
            return View();
        }

        //
        //POST : /Clinica/Adicionar
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Adicionar(Clinica model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.ClinicaRepositorio.Insert(model);
                    _unitOfWork.Save();

                    Session.Add("Mensagem", "Clínica adicionada com sucesso");
                    return RedirectToAction("Adicionar");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Não foi possível adicionar a clínica");
            }
            finally
            {
                _unitOfWork.Dispose();
            }
            return View(model);
        }

        //
        // GET: /Clinica/Gerenciar
        [Authorize(Roles = "Administrador")]
        public ActionResult Gerenciar()
        {
            TempData["Mensagem"] = Session["Clinica"];
            TempData["Erro"] = Session["Erro"];
            //limpar sessão
            Session.Remove("Clinica");
            Session.Remove("Erro");

            var model = _unitOfWork.ClinicaRepositorio.Get();
            _unitOfWork.Dispose();
            return View(model);
        }

        //
        // GET: /Clinica/Deletar
        [Authorize(Roles = "Administrador")]
        public ActionResult Deletar(int id)
        {
            try
            {
                //deletando Clínica
                var model = _unitOfWork.ClinicaRepositorio.GetById(id);
                _unitOfWork.ClinicaRepositorio.Delete(id);
                _unitOfWork.EnderecoRepositorio.Delete(model.EnderecoId);
                _unitOfWork.Save();

                Session.Add("Clinica", "Clínica removida com sucesso");
                return RedirectToAction("Gerenciar");
            }
            catch (Exception)
            {
                Session.Add("Erro", "Não foi possível remover a clínica");
            }
            finally
            {
                _unitOfWork.Dispose();
            }
            return RedirectToAction("Gerenciar");
        }

        //
        // GET: /Clinica/Atualizar
        public ActionResult Atualizar(int clinicaId)
        {
            TempData["Mensagem"] = Session["AtualizaClinica"];
            TempData["Erro"] = Session["Erro"];
            //limpar sessão
            Session.Remove("AtualizaClinica");
            Session.Remove("Erro");
            try
            {
                var model = _unitOfWork.ClinicaRepositorio.GetById(clinicaId);
                Session.Add("clinicaId", model.ClinicaId);
                Session.Add("enderecoId", model.EnderecoId);
                return View(model);
            }
            catch (Exception)
            {
                Session.Add("Erro", "Não foi possível carregar a clínica");
            }
            return RedirectToAction("Gerenciar");
        }

        //
        // POST: /Clinica/SalvarAtualizacao
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult SalvarAtualizacao(Clinica model)
        {
            try
            {
                model.ClinicaId = Convert.ToInt32(Session["clinicaId"]);
                model.EnderecoId = Convert.ToInt32(Session["enderecoId"]);
                model.Endereco.EnderecoId = model.EnderecoId;
                _unitOfWork.ClinicaRepositorio.Update(model);
                _unitOfWork.EnderecoRepositorio.Update(model.Endereco);
                _unitOfWork.Save();

                Session.Add("Clinica", "Clínica atualizada com sucesso");
                return RedirectToAction("Gerenciar", model.ClinicaId);
            }
            catch (Exception)
            {
                Session.Add("Erro", "Não foi possível atualizar a clínica");
            }
            finally
            {
                _unitOfWork.Dispose();
            }
            return RedirectToAction("Gerenciar", model.ClinicaId);
        }


        //
        // GET: /Clinica/AssociarConvenio
        [Authorize(Roles = "Administrador")]
        public ActionResult AssociarConvenio()
        {
            TempData["Mensagem"] = Session["Mensagem"];
            TempData["Erro"] = Session["Erro"];
            Session.Remove("Mensagem");
            Session.Remove("Erro");
            return View();
        }

        //
        // POST: /Clinica/SalvarConvenio
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult SalvarConvenio(int[] clinicaId, int[] convenioId)
        {
            try
            {
                for (var i = 0; i < clinicaId.Count(); i++)
                {
                    var clinica = _unitOfWork.ClinicaRepositorio.GetById(clinicaId[i]);
                    for (var j = 0; j < convenioId.Count(); j++)
                    {
                        var conv = _unitOfWork.ConvenioRepositorio.GetById(convenioId[j]);
                        clinica.Convenios.Add(conv);
                        _unitOfWork.ClinicaRepositorio.Update(clinica);
                        _unitOfWork.Save();
                    }
                }

                Session.Add("Mensagem", "Associação efetuada com sucesso!");
                return RedirectToAction("AssociarConvenio");
            }
            catch (Exception)
            {
                Session.Add("Erro", "Não foi possível realizar a associação!");
            }
            finally
            {
                _unitOfWork.Dispose();
            }
            return RedirectToAction("AssociarConvenio");
        }
    }
}
