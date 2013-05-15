using System;
using System.Linq;
using System.Web.Mvc;
using ConsultaMed_WEB.Models;
using ConsultaMed_WEB.Models.Repositorio;

namespace ConsultaMed_WEB.Controllers
{
    public class EspecialidadeController : Controller
    {

        //OBJETOS:
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        //
        // GET: /Especialidade/AssociarExame
        [Authorize(Roles = "Administrador")]
        public ActionResult AssociarExame()
        {
            TempData["Mensagem"] = Session["Mensagem"];
            TempData["Erro"] = Session["Erro"];
            Session.Remove("Mensagem");
            Session.Remove("Erro");
            return View();
        }

        //
        // POST: /Especialdade/SalvarExame
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult SalvarExame(int[] especialidadeId, int[] exameId)
        {
            try
            {
                for (var i = 0; i < especialidadeId.Count(); i++)
                {
                    var espec = _unitOfWork.EspecialidadeRepositorio.GetById(especialidadeId[i]);
                    for (var j = 0; j < exameId.Count(); j++)
                    {
                        var ex = _unitOfWork.ExameRepositorio.GetById(exameId[j]);
                        espec.Exames.Add(ex);
                        _unitOfWork.EspecialidadeRepositorio.Update(espec);
                        _unitOfWork.Save();
                    }
                }

                Session.Add("Mensagem", "Associação efetuada com sucesso!");
                return RedirectToAction("AssociarExame");
            }
            catch (Exception)
            {
                Session.Add("Erro", "Não foi possível realizar a associação!");
            }
            finally
            {
                _unitOfWork.Dispose();
            }
            return RedirectToAction("AssociarExame");
        }

        //
        //GET: Especialidade/CarregaEspecialidade
        [HttpGet]
        [Authorize(Roles = "Paciente")]
        public JsonResult CarregaEspecialidade(int id)
        {
            try
            {
                var clinica = _unitOfWork.ClinicaRepositorio.Get(m => m.ClinicaId == id);
                var filtro = clinica.SelectMany(m => m.Especialidades);
                var data = filtro.Select(m => new { m.EspecialidadeId, m.Descricao }).ToList();

                var medico = _unitOfWork.MedicoRepositorio.Get(m => m.ClinicaId == id);
                var usuarioMedicos = medico as UsuarioMedico[] ?? medico.ToArray();

                //faço a validação de epecialidade - clinica - médico
                for (var i = 0; i < data.Count; i++)
                {
                    var aux = 0;
                    for (var j = 0; j < usuarioMedicos.Count(); j++)
                    {
                        if (data[i].EspecialidadeId == usuarioMedicos[j].EspecialidadeId)
                        {
                            aux = 1; //seto minha variável auxiliar caso haja a especialidade para ambos(médico e clinica)
                        }
                    }
                    if (aux == 0)
                    {
                        data.RemoveAt(i); //caso a variável não tenha sido setada eu removo a especialidade daquele indice do vetor
                        i--; //se removi tenho que decrementar o indice para não pular um intervalo indevido
                    }
                }

                return Json(new { Result = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw new Exception();
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

    }
}
