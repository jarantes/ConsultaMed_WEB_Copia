using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Security;
using System.Web.Mvc;
using ConsultaMed_WEB.Models;
using ConsultaMed_WEB.Models.Repositorio;
using DotNetOpenAuth.OAuth.Messages;

namespace ConsultaMed_WEB.Controllers
{
    public class ConsultaController : Controller
    {
        //OBJETOS:
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        private readonly ClsSendMail _sendMail = new ClsSendMail();

        //
        //GET: Consulta/NovaAgenda
        [Authorize(Roles = "Medico, RespClinica")]
        public ActionResult NovaAgenda()
        {
            TempData["Mensagem"] = Session["Mensagem"];
            Session.Remove("Mensagem");
            return View();
        }

        //
        //POST: Consulta/NovaAgenda
        [HttpPost]
        [Authorize(Roles = "Medico, RespClnica")]
        public ActionResult NovaAgenda(Horario model)
        {
            if (!FuValidaAgenda(model))
            {
                return View(model);
            }

            model.MedicoUserId = _unitOfWork.UsuarioRepositorio.GetIdByUserName(User.Identity.Name);

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.HorarioRepositorio.Insert(model);
                    _unitOfWork.Save();

                    Session.Add("Mensagem", "Agenda adiciondada com sucesso");
                    return RedirectToAction("NovaAgenda");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Não foi possível adicionar a nova agenda");
                }
                finally
                {
                    _unitOfWork.Dispose();
                }
            }
            return View(model);
        }

        //
        //GET: Consulta/NovaAgendaTemp
        [Authorize(Roles = "Medico, RespClinica")]
        public ActionResult NovaAgendaTemp()
        {
            TempData["Mensagem"] = Session["Mensagem"];
            Session.Remove("Mensagem");
            return View();
        }

        //
        //POST: Consulta/NovaAgendaTemp
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Medico, RespClinica")]
        public ActionResult NovaAgendaTemp(HorarioTemp model)
        {
            if (model.MedicoUserId == 0 && Roles.GetRolesForUser(User.Identity.Name)[0].ToString(CultureInfo.InvariantCulture) == "Medico")
            {
                model.MedicoUserId = _unitOfWork.UsuarioRepositorio.GetIdByUserName(User.Identity.Name);
            }

            if (!FuValidaAgendaTemp(model))
            {
                return View(model);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.HorarioTempRepositorio.Insert(model);
                    _unitOfWork.Save();

                    Session.Add("Mensagem", "Agenda temporária adiciondada com sucesso");
                    return RedirectToAction("NovaAgendaTemp");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Não foi possível adicionar a nova agenda temporária");
                }
                finally
                {
                    _unitOfWork.Dispose();
                }
            }
            return View(model);
        }

        //
        //GET: Consulta/Gerenciar
        [Authorize(Roles = "Medico")]
        public ActionResult Gerenciar()
        {
            TempData["Mensagem"] = Session["Mensagem"];
            TempData["Erro"] = Session["Erro"];
            Session.Remove("Mensagem");
            Session.Remove("Erro");
            try
            {
                var medico = _unitOfWork.MedicoRepositorio.Get(m => m.UserName == User.Identity.Name).First();
                var model = _unitOfWork.AgendamentoRepositorio.GetAgendforDoctor(medico.UserId, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(20));
                return View(model);
            }
            catch (Exception)
            {
                Session.Add("Erro", "Não foi possível retornar a pesquisa");
                TempData["Erro"] = Session["Erro"];
                Session.Remove("Erro");
                return View();
            }
        }

        //
        //GET: Consulta/Gerenciar2
        [Authorize(Roles = "RespClinica")]
        public ActionResult Gerenciar2(int? id)
        {
            TempData["Mensagem"] = Session["Mensagem"];
            TempData["Erro"] = Session["Erro"];
            Session.Remove("Mensagem");
            Session.Remove("Erro");
            try
            {
                IEnumerable<Agendamento> model;

                if (id != null)
                {
                    model = _unitOfWork.AgendamentoRepositorio.GetAgendforDoctor(id, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(7));
                    var clinica = _unitOfWork.UsuarioRepositorio.Get(m => m.UserName == User.Identity.Name).First();
                    CarregaListas("medicoList", clinica.ClinicaId);
                }
                else
                {
                    var clinica = _unitOfWork.UsuarioRepositorio.Get(m => m.UserName == User.Identity.Name).First();
                    CarregaListas("medicoList", clinica.ClinicaId);
                    model = _unitOfWork.AgendamentoRepositorio.GetAgendForClinica(clinica.ClinicaId, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(7));
                }
                return View(model);
            }
            catch (Exception)
            {
                Session.Add("Erro", "Não foi possível retornar a pesquisa");
                TempData["Erro"] = Session["Erro"];
                Session.Remove("Erro");
                return View();
            }
        }

        //
        //GET: Consulta/Gerenciar3
        [Authorize(Roles = "Paciente")]
        public ActionResult Gerenciar3()
        {
            TempData["Mensagem"] = Session["Mensagem"];
            TempData["Erro"] = Session["Erro"];
            Session.Remove("Mensagem");
            Session.Remove("Erro");
            try
            {
                var hoje = DateTime.Now.Date;
                var user = _unitOfWork.PacienteRepositorio.Get(m => m.UserName == User.Identity.Name).First();
                var model = _unitOfWork.AgendamentoRepositorio.Get(m=>m.PacienteUserId==user.UserId && m.DataConsulta >= hoje);
                return View(model);
            }
            catch (Exception)
            {
                Session.Add("Erro", "Não foi possível retornar a pesquisa");
                TempData["Erro"] = Session["Erro"];
                Session.Remove("Erro");
                return View();
            }
        }

        //
        //GET: Consulta/Remover
        [Authorize(Roles = "Medico, RespClnica, Paciente")]
        public ActionResult Deletar(int id)
        {
            try
            {
                var pront = _unitOfWork.ProntuarioRepositorio.Get(m => m.AgendamentoId == id);
                var prontuarios = pront as Prontuario[] ?? pront.ToArray();
                if (prontuarios.Any())
                {
                    _unitOfWork.ProntuarioRepositorio.Delete(prontuarios.First().ProntuarioId);
                }
                _unitOfWork.AgendamentoRepositorio.Delete(id);
                _unitOfWork.Save();

                Session.Add("Mensagem", "Agendamento removido com sucesso");
                return RedirectToAction("Gerenciar");
            }
            catch (Exception)
            {
                Session.Add("Erro", "Não foi possível excluir o agendamento");
            }
            finally
            {
                _unitOfWork.Dispose();
            }
            return RedirectToAction("Gerenciar");
        }

        //
        //GET: Consulta/Agendar
        [Authorize(Roles = "Medico")]
        public ActionResult Agendar()
        {
            TempData["Mensagem"] = Session["Mensagem"];
            Session.Remove("Mensagem");

            using (var ctx = new CmContext())
            {
                ViewData["pacienteList"] = new SelectList((from i in ctx.Usuarios.ToList()
                                                           where i.Perfil == "Paciente"
                                                           select new
                                                               {
                                                                   i.UserId,
                                                                   NomeInteiro = i.Nome + " " + i.Sobrenome
                                                               }),
                                                          "UserId",
                                                          "NomeInteiro",
                                                          null);
            }
            return View();
        }

        //
        //POST: Consulta/Agendar
        [HttpPost]
        [Authorize(Roles = "Medico")]
        public ActionResult Agendar(Agendamento model)
        {
            var def = Convert.ToDateTime("01/01/0001");
            if (model.DataConsulta < DateTime.Now.Date && model.DataConsulta != def)
            {
                ModelState.AddModelError("", "Data não pode ser menor que hoje!");
                CarregaListas("pacienteList", null);
                return View();
            }
            try
            {
                model.MarcadorId = _unitOfWork.UsuarioRepositorio.GetIdByUserName(User.Identity.Name);
                model.MedicoUserId = model.MarcadorId;
                model.DataCriacao = DateTime.Now;

                if (Session["HorarioId"] == null)
                {
                    model.HorarioTempId = Convert.ToInt32(Session["HorarioTempId"]);
                }
                else
                {
                    model.HorarioId = Convert.ToInt32(Session["HorarioId"]);
                }

                if (ModelState.IsValid)
                {
                    _unitOfWork.AgendamentoRepositorio.Insert(model);
                    _unitOfWork.Save();

                    //envio de email
                    var email = _unitOfWork.PacienteRepositorio.Get(m => m.UserId == model.PacienteUserId).First();
                    var retorno = _sendMail.CreateEmail(1, model.MedicoUserId, model.MarcadorId, model.PacienteUserId, model.DataConsulta.ToShortDateString(), model.Horario.ToString(), email.Email);
                    if (retorno == null)
                    {
                        Session.Add("Mensagem", "Consulta Agendada com sucesso, foi enviado um e-mail de confirmação");
                        return RedirectToAction("Agendar");
                    }
                    else
                    {
                        Session.Add("Mensagem", "Consulta Agendada com sucesso");
                        return RedirectToAction("Agendar");
                    }
                }
            }

            catch (Exception)
            {
                ModelState.AddModelError("", "Não foi possível agendar a consulta");
            }
            finally
            {
                _unitOfWork.Dispose();
            }
            CarregaListas("pacienteList", null);
            ModelState.AddModelError("", "Não foi possível agendar a consulta");
            return View();
        }

        //
        //GET: Consulta/CarregaAgenda
        [Authorize(Roles = "Medico, RespClinica, Paciente")]
        public ActionResult CarregaAgenda(String agendaData, int? medicoUserId)
        {
            var dataAtual = DateTime.Now.AddMinutes(10.0);
            var horaAtual = new TimeSpan(dataAtual.Hour, dataAtual.Minute, dataAtual.Second);
            try
            {
                //declarando e setando variáveis
                var data = new string[20]; //array de 20 horários no máximo
                var agendaDt = Convert.ToDateTime(agendaData);
                int medicoId = medicoUserId != null ? Convert.ToInt32(medicoUserId) : _unitOfWork.UsuarioRepositorio.GetIdByUserName(User.Identity.Name);
                var modelTemp = _unitOfWork.HorarioTempRepositorio.Get(m => m.Data == agendaDt && m.MedicoUserId == medicoId);

                var horarioTemps = modelTemp as HorarioTemp[] ?? modelTemp.ToArray();
                if (horarioTemps.Any())
                {
                    //pega informações do db e seta nas variáveis
                    var horarioInicial = horarioTemps.First().HorarioIni;
                    var horarioFinal = horarioTemps.First().HorarioFim;
                    var intervalo = horarioTemps.First().TempoConsulta;
                    var iniAlmoco = horarioTemps.First().TempoDescansoInicial;
                    var fimAlmoco = horarioTemps.First().TempoDescansoFinal;
                    var idx = 0;
                    Session.Add("HorarioTempId", horarioTemps.First().HorarioTempId);

                    //começa loop do horario
                    while (horarioInicial <= horarioFinal)
                    {
                        TimeSpan horarioTempIncrement = horarioInicial;
                        if (!
                            _unitOfWork.AgendamentoRepositorio.Get(m => m.MedicoUserId == medicoId && m.Horario == horarioTempIncrement && m.DataConsulta == agendaDt)
                                       .Any())
                        {
                            if (agendaDt.Date == DateTime.Now.Date) //se a data for = hoje, valida o horário e almoço
                            {
                                if (horarioTempIncrement >= horaAtual)
                                {
                                    if (horarioTempIncrement < iniAlmoco || horarioTempIncrement >= fimAlmoco)
                                    {
                                        data[idx] = horarioTempIncrement.ToString();
                                        idx++;
                                    }
                                }
                            }
                            else //se não valida somente almoço
                            {
                                if (horarioTempIncrement < iniAlmoco || horarioTempIncrement >= fimAlmoco)
                                {
                                    data[idx] = horarioTempIncrement.ToString();
                                    idx++;
                                }
                            }
                        }
                        horarioInicial = horarioTempIncrement.Add(intervalo);
                    }
                }
                else
                {
                    var model = _unitOfWork.HorarioRepositorio.Get(m => m.PerInicio <= agendaDt && m.PerFim >= agendaDt && m.MedicoUserId == medicoId);

                    var horario = model as Horario[] ?? model.ToArray();
                    if (horario.Any())
                    {
                        //pega informações do db
                        var horarioInicial = horario.First().HorarioIni;
                        var horarioFinal = horario.First().HorarioFim;
                        var intervalo = horario.First().TempoConsulta;
                        var iniAlmoco = horario.First().TempoDescansoInicial;
                        var fimAlmoco = horario.First().TempoDescansoFinal;
                        var idx = 0;
                        Session.Add("HorarioId", horario.First().HorarioId);
                        //começa loop do horario
                        while (horarioInicial <= horarioFinal)
                        {
                            TimeSpan horarioIncrement = horarioInicial;
                            if (!
                                _unitOfWork.AgendamentoRepositorio.Get(m => m.MedicoUserId == medicoId && m.Horario == horarioIncrement && m.DataConsulta == agendaDt)
                                           .Any())
                            {
                                if (agendaDt.Date == DateTime.Now.Date) //se a data for = hoje, valida o horário e almoço
                                {
                                    if (horarioIncrement >= horaAtual)
                                    {
                                        if (horarioIncrement < iniAlmoco || horarioIncrement >= fimAlmoco)
                                        {
                                            data[idx] = horarioIncrement.ToString();
                                            idx++;
                                        }
                                    }
                                }
                                else //se não valida somente almoço
                                {
                                    if (horarioIncrement < iniAlmoco || horarioIncrement >= fimAlmoco)
                                    {
                                        data[idx] = horarioIncrement.ToString();
                                        idx++;
                                    }
                                }
                            }
                            horarioInicial = horarioIncrement.Add(intervalo);
                        }
                    }
                }

                //retorna lista de horários disponíveis
                return Json(new { data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return RedirectToAction("Agendar");
            }
        }

        //
        //GET: Consulta/CriarProntuario
        [Authorize(Roles = "Medico")]
        public ActionResult CriarProntuario(int id)
        {
            TempData["Mensagem"] = Session["Mensagem"];
            Session.Remove("Mensagem");
            try
            {
                var consulta = _unitOfWork.AgendamentoRepositorio.Get(m => m.AgendamentoId == id).First();
                Session.Add("PacienteUserId", consulta.PacienteUserId); //para gravar no prontuário
                Session.Add("MedicoUserId", _unitOfWork.UsuarioRepositorio.GetIdByUserName(User.Identity.Name)); //para selecionar os exames correspondentes na View
                Session.Add("AgendamentoId", id);
                var medicoUserId = Convert.ToInt32(Session["MedicoUserId"]);
                var espec = _unitOfWork.MedicoRepositorio.Get(m => m.UserId == medicoUserId).First();
                Session.Add("EspecialidadeId", espec.EspecialidadeId);
                CarregaListas("Nomepaciente", consulta.PacienteUserId);
            }
            catch (Exception)
            {
                return new HttpNotFoundResult();
            }
            finally
            {
                _unitOfWork.Dispose();
            }
            return View();
        }

        //
        //POST: Consulta/CriarProntuario
        [HttpPost]
        [Authorize(Roles = "Medico")]
        public ActionResult CriarProntuario(Prontuario model)
        {
            var agendamentoId = Convert.ToInt32(Session["AgendamentoId"]);
            var userId = Convert.ToInt32(Session["PacienteUserId"]);
            if (_unitOfWork.ProntuarioRepositorio.Get(m => m.AgendamentoId == agendamentoId).Any())
            {
                ModelState.AddModelError("", "já existe um prontuário criado para essa consulta.");
                CarregaListas("Nomepaciente", userId);
                return View(model);
            }
            try
            {
                if (model.ListaExame.Any())
                {
                    for (var i = 0; i < model.ListaExame.Count(); i++)
                    {
                        if (model.Exame == null)
                        {
                            model.Exame = model.Exame + model.ListaExame.ElementAt(i);
                        }
                        else
                        {
                            model.Exame = model.Exame + "; " + model.ListaExame.ElementAt(i);
                        }
                    }
                }
                model.UserId = userId;
                model.DataCriacao = DateTime.Now;
                model.AgendamentoId = agendamentoId;
                if (ModelState.IsValid)
                {
                    _unitOfWork.ProntuarioRepositorio.Insert(model);
                    _unitOfWork.Save();
                    Session.Add("Mensagem", "Prontuário criado com sucesso");
                    return RedirectToAction("Gerenciar");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Não foi possível criar o prontuário");
            }
            finally
            {
                _unitOfWork.Dispose();
            }
            return RedirectToAction("Gerenciar");
        }

        //
        //GET: Consulta/Agendar2
        [Authorize(Roles = "RespClinica")]
        public ActionResult Agendar2()
        {
            TempData["Mensagem"] = Session["Mensagem"];
            Session.Remove("Mensagem");

            CarregaListas("pacienteList", null);

            return View();
        }

        //
        //POST: Consulta/Agendar2
        [HttpPost]
        [Authorize(Roles = "RespClinica")]
        public ActionResult Agendar2(Agendamento model)
        {
            var def = Convert.ToDateTime("01/01/0001");
            if (model.DataConsulta < DateTime.Now.Date && model.DataConsulta != def)
            {
                ModelState.AddModelError("", "Data não pode ser menor que hoje!");
                CarregaListas("pacienteList", null);
                return View();
            }
            try
            {
                model.MarcadorId = _unitOfWork.UsuarioRepositorio.GetIdByUserName(User.Identity.Name);
                model.DataCriacao = DateTime.Now;

                if (Session["HorarioId"] == null)
                {
                    model.HorarioTempId = Convert.ToInt32(Session["HorarioTempId"]);
                }
                else
                {
                    model.HorarioId = Convert.ToInt32(Session["HorarioId"]);
                }

                if (ModelState.IsValid)
                {
                    _unitOfWork.AgendamentoRepositorio.Insert(model);
                    _unitOfWork.Save();


                    //envio de email
                    var email = _unitOfWork.PacienteRepositorio.Get(m => m.UserId == model.PacienteUserId).First();
                    var retorno = _sendMail.CreateEmail(1, model.MedicoUserId, model.MarcadorId, model.PacienteUserId, model.DataConsulta.ToShortDateString(), model.Horario.ToString(), email.Email);
                    if (retorno == null)
                    {
                        Session.Add("Mensagem", "Consulta Agendada com sucesso, foi enviado um e-mail de confirmação");
                        return RedirectToAction("Agendar2");
                    }
                    else
                    {
                        Session.Add("Mensagem", "Consulta Agendada com sucesso");
                        return RedirectToAction("Agendar2");
                    }
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Não foi possível agendar a consulta");
            }
            finally
            {
                _unitOfWork.Dispose();
            }
            CarregaListas("pacienteList", null);
            ModelState.AddModelError("", "Não foi possível agendar a consulta");
            return View();
        }

        //
        //GET: Consulta/Agendar3
        [Authorize(Roles = "Paciente")]
        public ActionResult Agendar3()
        {
            TempData["Mensagem"] = Session["Mensagem"];
            Session.Remove("Mensagem");

            return View();
        }

        //
        //POST: Consulta/Agendar3
        [HttpPost]
        [Authorize(Roles = "Paciente")]
        public ActionResult Agendar3(Agendamento model)
        {
            var def = Convert.ToDateTime("01/01/0001");
            if (model.DataConsulta < DateTime.Now.Date && model.DataConsulta != def)
            {
                ModelState.AddModelError("", "Data não pode ser menor que hoje!");
                return View();
            }
            try
            {
                model.MarcadorId = _unitOfWork.UsuarioRepositorio.GetIdByUserName(User.Identity.Name);
                model.PacienteUserId = model.MarcadorId;
                model.DataCriacao = DateTime.Now;

                if (Session["HorarioId"] == null)
                {
                    model.HorarioTempId = Convert.ToInt32(Session["HorarioTempId"]);
                }
                else
                {
                    model.HorarioId = Convert.ToInt32(Session["HorarioId"]);
                }

                if (ModelState.IsValid)
                {
                    _unitOfWork.AgendamentoRepositorio.Insert(model);
                    _unitOfWork.Save();


                    //envio de email
                    var email = _unitOfWork.PacienteRepositorio.Get(m => m.UserId == model.PacienteUserId).First();
                    var retorno = _sendMail.CreateEmail(1, model.MedicoUserId, model.MarcadorId, model.PacienteUserId, model.DataConsulta.ToShortDateString(), model.Horario.ToString(), email.Email);
                    if (retorno == null)
                    {
                        Session.Add("Mensagem", "Consulta Agendada com sucesso, foi enviado um e-mail de confirmação");
                        return RedirectToAction("Agendar3");
                    }
                    else
                    {
                        Session.Add("Mensagem", "Consulta Agendada com sucesso");
                        return RedirectToAction("Agendar3");
                    }
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Não foi possível agendar a consulta");
            }
            finally
            {
                _unitOfWork.Dispose();
            }
            CarregaListas("pacienteList", null);
            ModelState.AddModelError("", "Não foi possível agendar a consulta");
            return View();
        }

        #region ValidarAgenda
        private bool FuValidaAgenda(Horario model)
        {
            if (model.PerInicio.Date < DateTime.Now.Date)
            {
                ModelState.AddModelError("", "A data inicial não pode ser menor que hoje");
                return false;
            }

            if (model.PerFim.Date < model.PerInicio.Date)
            {
                ModelState.AddModelError("", "A data final não pode ser menor que a data inicial");
                return false;
            }

            if (model.TempoDescansoFinal < model.TempoDescansoInicial)
            {
                ModelState.AddModelError("", "O tempo de descanso final não pode ser menor que o tempo de descanso inicial");
                return false;
            }

            if (model.TempoDescansoFinal <= model.TempoDescansoInicial)
            {
                ModelState.AddModelError("", "O tempo de descanso final não pode ser menor que o tempo de descanso inicial");
                return false;
            }

            if (model.TempoDescansoInicial < model.HorarioIni)
            {
                ModelState.AddModelError("", "O tempo de descanso inicial não pode ser menor que o horário inicial");
                return false;
            }
            var minCons = new TimeSpan(0, 0, 10, 0);
            if (model.TempoConsulta < minCons)
            {
                ModelState.AddModelError("", "O tempo da consulta deve ser igual ou superior a 10 minutos");
                return false;
            }

            if (model.HorarioFim <= model.HorarioIni)
            {
                ModelState.AddModelError("", "O horário inicial não pode ser maior ou igual ao horário final");
                return false;
            }

            if (
                _unitOfWork.HorarioRepositorio.Get(
                    m => m.MedicoUserId == model.MedicoUserId && model.PerInicio <= m.PerFim).Any())
            {
                ModelState.AddModelError("", "Já existe uma agenda para o período informado");
                return false;
            }
            return true;
        }
        #endregion

        #region ValidarAgendaTemp
        private bool FuValidaAgendaTemp(HorarioTemp model)
        {
            if (model.TempoDescansoFinal < model.TempoDescansoInicial)
            {
                ModelState.AddModelError("", "O tempo de descanso final não pode ser menor que o tempo de descanso inicial");
                return false;
            }

            if (model.TempoDescansoFinal <= model.TempoDescansoInicial)
            {
                ModelState.AddModelError("", "O tempo de descanso final não pode ser menor que o tempo de descanso inicial");
                return false;
            }

            if (model.TempoDescansoInicial < model.HorarioIni)
            {
                ModelState.AddModelError("", "O tempo de descanso inicial não pode ser menor que o horário inicial");
                return false;
            }
            var minCons = new TimeSpan(0, 0, 10, 0);
            if (model.TempoConsulta < minCons)
            {
                ModelState.AddModelError("", "O tempo da consulta deve ser igual ou superior a 10 minutos");
                return false;
            }

            if (model.HorarioFim <= model.HorarioIni)
            {
                ModelState.AddModelError("", "O horário inicial não pode ser maior ou igual ao horário final");
                return false;
            }

            model.MedicoUserId = _unitOfWork.UsuarioRepositorio.GetIdByUserName(User.Identity.Name);
            if (
                _unitOfWork.HorarioTempRepositorio.Get(
                    m => m.MedicoUserId == model.MedicoUserId && model.Data == m.Data).Any())
            {
                ModelState.AddModelError("", "Já existe uma agenda temporária para a data informada");
                return false;
            }
            return true;
        }
        #endregion

        #region CarregarListas
        private void CarregaListas(string lista, int? id)
        {
            if (lista == "pacienteList")
            {
                using (var ctx = new CmContext())
                {
                    ViewData["pacienteList"] = new SelectList((from i in ctx.Usuarios.ToList()
                                                               where i.Perfil == "Paciente"
                                                               select new
                                                                   {
                                                                       i.UserId,
                                                                       NomeInteiro = i.Nome + " " + i.Sobrenome
                                                                   }),
                                                              "UserId",
                                                              "NomeInteiro",
                                                              null);
                }
            }
            else if (lista == "Nomepaciente")
            {
                using (var ctx = new CmContext())
                {
                    ViewData["NomePaciente"] = (from i in ctx.Usuarios
                                                where i.UserId == id
                                                select new
                                                    {
                                                        Paciente = i.Nome + " " + i.Sobrenome
                                                    }).First();

                }
            }
            else if (lista == "medicoList")
            {
                using (var ctx = new CmContext())
                {
                    ViewData["medicoList"] = new SelectList((from i in ctx.Usuarios.ToList()
                                                             where i.Perfil == "Medico"
                                                             && i.ClinicaId == id
                                                             select new
                                                             {
                                                                 i.UserId,
                                                                 NomeInteiro = i.Nome + " " + i.Sobrenome
                                                             }),
                                                              "UserId",
                                                              "NomeInteiro",
                                                              null);
                }
            }

        }

        #endregion
    }
}


