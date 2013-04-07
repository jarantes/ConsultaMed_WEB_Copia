using System;
using System.Linq;
using System.Web.Mvc;
using ConsultaMed_WEB.Models;
using ConsultaMed_WEB.Models.Repositorio;

namespace ConsultaMed_WEB.Controllers
{
    public class ConsultaController : Controller
    {
        //OBJETOS:
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

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
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.HorarioRepositorio.Insert(model);
                    _unitOfWork.Save();
                    _unitOfWork.Dispose();

                    Session.Add("Mensagem", "Agenda adiciondada com sucesso");
                    return RedirectToAction("NovaAgenda");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Não foi possível adicionar a nova agenda");
                }
            }
            return View(model);
        }

        //
        //GET: Consulta/NovaAgendaTemp
        [Authorize(Roles = "Medico, UserClinica")]
        public ActionResult NovaAgendaTemp()
        {
            TempData["Mensagem"] = Session["Mensagem"];
            Session.Remove("Mensagem");
            return View();
        }

        //
        //POST: Consulta/NovaAgendaTemp
        [HttpPost]
        [Authorize(Roles = "Medico, RespClinica")]
        public ActionResult NovaAgendaTemp(HorarioTemp model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.HorarioTempRepositorio.Insert(model);
                    _unitOfWork.Save();
                    _unitOfWork.Dispose();

                    Session.Add("Mensagem", "Agenda temporária adiciondada com sucesso");
                    return RedirectToAction("NovaAgenda");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Não foi possível adicionar a nova agenda temporária");
                }
            }
            return View(model);
        }

        //
        //GET: Consulta/Listar
        [Authorize(Roles = "Medico, UserClinica")]
        public ActionResult Listar()
        {
            try
            {
                var medicoId = _unitOfWork.UsuarioRepositorio.GetIdByUserName(User.Identity.Name);
                var model = _unitOfWork.AgendamentoRepositorio.GetAgendforDoctor(medicoId, DateTime.Now.AddDays(-7), DateTime.Now.AddDays(7));
                _unitOfWork.Dispose();
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
        //GET: Consulta/Agendar
        [Authorize(Roles = "Medico")]
        public ActionResult Agendar()
        {
            TempData["Mensagem"] = Session["Mensagem"];
            Session.Remove("Mensagem");
            TempData["Erro"] = Session["Erro"];
            Session.Remove("Erro");
            using (var ctx = new CmContext())
            {
                ViewData["pacienteList"] = new SelectList((from i in ctx.Usuarios.ToList()
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
        //GET: Consulta/Agendar
        [Authorize(Roles = "Medico")]
        [HttpPost]
        public ActionResult Agendar(Agendamento model)
        {
            try
            {
                model.MarcadorId = _unitOfWork.UsuarioRepositorio.GetIdByUserName(User.Identity.Name);
                model.MedicoUserId = model.MarcadorId;
                model.DataCriacao = DateTime.Now;

                if (ModelState.IsValid)
                {
                    _unitOfWork.AgendamentoRepositorio.Insert(model);
                    _unitOfWork.Save();
                    _unitOfWork.Dispose();

                    Session.Add("Mensagem", "Consulta Agendada com sucesso");
                    return RedirectToAction("Agendar");
                }
            }
            catch (Exception)
            {
                Session.Add("Erro", "Não foi possível agendar a consulta");
            }
            Session.Add("Erro", "Não foi possível agendar a consulta");
            return RedirectToAction("Agendar");
        }

        //GET: Consulta/CarregaAgenda
        [Authorize(Roles = "Medico, RespClinica")]
        public ActionResult CarregaAgenda(String agendaData)
        {
            try
            {
                //aqui oque ele irá retornar, array string 20
                var data = new string[20];
                var agendaDt = Convert.ToDateTime(agendaData);
                var medicoId = _unitOfWork.UsuarioRepositorio.GetIdByUserName(User.Identity.Name);
                var modelTemp = _unitOfWork.HorarioTempRepositorio.Get(m => m.Data == agendaDt && m.UserId == medicoId);

                var horarioTemps = modelTemp as HorarioTemp[] ?? modelTemp.ToArray();
                if (horarioTemps.Any())
                {
                    //pega informações do db
                    var horarioInicial = horarioTemps.First().HorarioIni;
                    var horarioFinal = horarioTemps.First().HorarioFim;
                    var intervalo = horarioTemps.First().TempoConsulta;
                    var idx = 0;

                    //começa loop do horario
                    while (horarioInicial <= horarioFinal)
                    {
                        TimeSpan horarioIncrement = horarioInicial;
                        if (!
                            _unitOfWork.AgendamentoRepositorio.Get(m => m.MedicoUserId == medicoId && m.Horario == horarioIncrement)
                                       .Any())
                        {
                            data[idx] =  horarioIncrement.ToString();
                            idx++;
                        }
                        horarioInicial = horarioIncrement.Add(intervalo);
                    }
                }
                else
                {
                    var model = _unitOfWork.HorarioRepositorio.Get(m => m.PerInicio <= agendaDt && m.PerInicio >= agendaDt && m.UserId == medicoId);

                    var horario = model as Horario[] ?? model.ToArray();
                    if (horario.Any())
                    {
                        //pega informações do db
                        var horarioInicial = horario.First().HorarioIni;
                        var horarioFinal = horario.First().HorarioFim;
                        var intervalo = horario.First().TempoConsulta;
                        var idx = 0;

                        //começa loop do horario
                        while (horarioInicial <= horarioFinal)
                        {
                            TimeSpan horarioIncrement = horarioInicial;
                            if (!
                                _unitOfWork.AgendamentoRepositorio.Get(m => m.MedicoUserId == medicoId && m.Horario == horarioIncrement)
                                           .Any())
                            {
                                data[idx] = horarioIncrement.ToString();
                                idx++;
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
                throw new Exception();
            }

        }




    }
}
