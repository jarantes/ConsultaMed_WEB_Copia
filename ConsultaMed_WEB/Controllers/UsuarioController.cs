using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using ConsultaMed_WEB.Models;
using ConsultaMed_WEB.Models.Repositorio;
using WebMatrix.WebData;

namespace ConsultaMed_WEB.Controllers
{
    public class UsuarioController : Controller
    {
        //OBJETOS:
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        //
        // GET: /Usuario/Registrar
        [Authorize(Roles = "Administrador")]
        public ActionResult Registrar()
        {
            TempData["Mensagem"] = Session["Mensagem"];
            Session.Remove("Mensagem");
            return View();
        }

        //
        // POST: /Usuario/Registrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Registrar(RegisterModel model)
        {
            if (model.Perfil == null)
            {
                ModelState.AddModelError("", "O perfil do usuário é obrigatório");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                // Tente registrar o usuário
                try
                {
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                    Session.Add("UserNameCadastrado", model.UserName);
                    Roles.AddUserToRole(model.UserName, model.Perfil);
                    Session.Add("Mensagem", "Para concluir o cadastro preencha as informações abaixo!");
                    switch (model.Perfil)
                    {
                        case "Medico":
                            return RedirectToAction("AddMedico");

                        case "Paciente":
                            return RedirectToAction("AddUserPaciente");

                        case "RespClinica":
                            return RedirectToAction("AddUserClinica");

                        case "Administrador":
                            Session.Add("Mensagem", "Novo administrador incluído com sucesso!");
                            return RedirectToAction("Registrar");
                    }
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            return View(model);
        }

        //
        // GET: /Usuario/AddMedico
        [Authorize(Roles = "Administrador")]
        public ActionResult AddMedico()
        {
            TempData["Mensagem"] = Session["Mensagem"];
            //limpar sessão
            Session.Remove("Mensagem");
            return View();
        }

        //
        // POST: /Usuario/AddMedico
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult AddMedico(UsuarioMedico medico)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //validar Unique Constraint
                    var validacao =  ValidacaoBruta(medico.Email, medico.Rg, medico.Cpf, null);
                    if (validacao != null)
                    {
                        ModelState.AddModelError("", validacao);
                        return View(medico);
                    }

                    //se passar pela validação grava...
                    //Gravando relação de clinica e especialidade
                    var clinica = _unitOfWork.ClinicaRepositorio.Get(m => m.ClinicaId == medico.ClinicaId).First();
                    var espec =
                        _unitOfWork.EspecialidadeRepositorio.Get(m => m.EspecialidadeId == medico.EspecialidadeId)
                            .First();
                    clinica.Especialidades.Add(espec);

                    //Gravando Médico na tabela de usuário
                    medico.UserName = (string)Session["UserNameCadastrado"];
                    medico.UserId =
                        _unitOfWork.UsuarioRepositorio.GetIdByUserName((string)Session["UserNameCadastrado"]);
                    medico.Perfil = "Medico";
                    _unitOfWork.MedicoRepositorio.Insert(medico);
                    _unitOfWork.Save();

                    //Adcionando Mensagem de Sucesso na sessão para mostrar na view
                    Session.Add("Mensagem", "Médico adiciondado com sucesso");
                    return RedirectToAction("Registrar");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Não foi possível adicionar o novo Médico");
            }
            finally
            {
                _unitOfWork.Dispose();
            }
            return View(medico);
        }

        //
        // GET: /Usuario/AddUserClinica
        [Authorize(Roles = "Administrador")]
        public ActionResult AddUserClinica()
        {
            TempData["Mensagem"] = Session["Mensagem"];
            //limpar sessão
            Session.Remove("Mensagem");
            return View();
        }

        //
        // POST: /Usuario/AddUserClinica
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult AddUserClinica(UsuarioRespClinica userClinica)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //validar Unique Constraint
                    var validacao = ValidacaoBruta(userClinica.Email, userClinica.Rg, userClinica.Cpf, null);
                    if (validacao != null)
                    {
                        ModelState.AddModelError("", validacao);
                        return View(userClinica);
                    }

                    //se passar pela validação grava...
                    userClinica.UserName = (string)Session["UserNameCadastrado"];
                    userClinica.UserId =
                        _unitOfWork.UsuarioRepositorio.GetIdByUserName((string)Session["UserNameCadastrado"]);
                    userClinica.Perfil = "RespClinica";
                    _unitOfWork.UsuarioRepositorio.Insert(userClinica);
                    _unitOfWork.Save();

                    Session.Add("Mensagem", "Usuário adiciondado com sucesso");
                    return RedirectToAction("Registrar");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Não foi possível adicionar o Usuário");
            }
            finally
            {
                _unitOfWork.Dispose();
            }
            return View(userClinica);
        }

        //
        // GET: /Usuario/AddUserPaciente
        [Authorize(Roles = "Administrador, Paciente")]
        public ActionResult AddUserPaciente()
        {
            TempData["Mensagem"] = Session["Mensagem"];
            //limpar sessão
            Session.Remove("Mensagem");
            return View();
        }

        //
        // POST: /Usuario/AddUserPaciente
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Paciente")]
        public ActionResult AddUserPaciente(UsuarioPaciente userPaciente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //validar Unique Constraint
                    var validacao = ValidacaoBruta(userPaciente.Email, userPaciente.Rg, userPaciente.Cpf, userPaciente.NumeroConvenio);
                    if (validacao != null)
                    {
                        ModelState.AddModelError("", validacao);
                        return View(userPaciente);
                    }
                    //se passar pela validação grava...
                    userPaciente.UserName = (string)Session["UserNameCadastrado"];
                    userPaciente.UserId =
                        _unitOfWork.UsuarioRepositorio.GetIdByUserName((string)Session["UserNameCadastrado"]);
                    userPaciente.Perfil = "Paciente";
                    _unitOfWork.UsuarioRepositorio.Insert(userPaciente);
                    _unitOfWork.Save();

                    Session.Add("Mensagem", "Usuário adiciondado com sucesso");
                    return Roles.GetRolesForUser(User.Identity.Name)[0] == "Administrador"
                        ? RedirectToAction("Registrar")
                        : RedirectToAction("Index", "Home");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Não foi possível adicionar o Usuário");
            }
            finally
            {
                _unitOfWork.Dispose();
            }
            return View(userPaciente);
        }

        //
        // GET: /Usuario/GerenciarUsuario
        [Authorize(Roles = "Administrador")]
        public ActionResult Gerenciar()
        {
            TempData["Mensagem"] = Session["DeleteUser"];
            TempData["Erro"] = Session["Erro"];
            //limpar sessão
            Session.Remove("DeleteUser");
            Session.Remove("Erro");

            var model = _unitOfWork.UsuarioRepositorio.Get();
            _unitOfWork.Dispose();
            return View(model);
        }

        //
        // GET: /Usuario/DeletarUsuario
        [Authorize(Roles = "Administrador")]
        public ActionResult Deletar(int id)
        {
            try
            {
                //deletando Perfil
                using (var ctuser = new UsersContext())
                {
                    var user = ctuser.UserProfiles.First(n => n.UserId == id);
                    var role = Roles.GetRolesForUser(user.UserName).First();
                    Roles.RemoveUserFromRole(user.UserName, role);
                    ctuser.UserProfiles.Remove(user);
                    ctuser.SaveChanges();
                }
                //deletando Usuário
                var model = _unitOfWork.UsuarioRepositorio.GetById(id);
                _unitOfWork.UsuarioRepositorio.Delete(id);
                _unitOfWork.EnderecoRepositorio.Delete(model.EnderecoId);
                _unitOfWork.Save();

                Session.Add("DeleteUser", "Usuário removido com sucesso");
                return RedirectToAction("Gerenciar");
            }
            catch (Exception)
            {
                Session.Add("Erro", "Não foi possível excluir o usuário");
            }
            finally
            {
                _unitOfWork.Dispose();
            }
            return RedirectToAction("Gerenciar");
        }

        //
        // GET: /Usuario/Listar
        [Authorize(Roles = "Medico")]
        public ActionResult Listar()
        {
            try
            {
                var medicoId = _unitOfWork.UsuarioRepositorio.GetIdByUserName(User.Identity.Name);
                var model = _unitOfWork.PacienteRepositorio.GetUsersforDoctor(medicoId, DateTime.Now.AddDays(-1),
                    DateTime.Now.AddDays(7));

                return View(model);
            }
            catch (Exception)
            {
                Session.Add("Erro", "Não foi possível retornar a pesquisa");
                TempData["Erro"] = Session["Erro"];
                Session.Remove("Erro");
                return View();
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        //
        //GET: Consulta/CarregaMedico
        [HttpGet]
        [Authorize(Roles = "RespClinica, Paciente")]
        public JsonResult CarregaMedico(int id)
        {
            try
            {
                var medicos = _unitOfWork.MedicoRepositorio.Get(m => m.EspecialidadeId == id);
                var data = medicos.Select(m => new { m.UserId, m.Nome, m.Sobrenome }).ToList();
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

        public string ValidacaoBruta(string email, string rg, string cpf, string numCarteira)
        {
            //validação do email
            if (_unitOfWork.UsuarioRepositorio.Get(m => m.Email == email).Any())
            {
                return "Já existe um registro com esse e-mail";
            }
            //validação do RG
            if (_unitOfWork.UsuarioRepositorio.Get(m => m.Rg == rg).Any())
            {
                return "Já existe um registro com esse RG";
            }
            //validação do CPF
            if (_unitOfWork.UsuarioRepositorio.Get(m => m.Cpf == cpf).Any())
            {
                return "Já existe um registro com esse CPF";
            }
            //validação da Carteirinha
            if (_unitOfWork.PacienteRepositorio.Get(m => m.NumeroConvenio == numCarteira).Any())
            {
                return "Já existe um registro com esse número da Carteirinha";
            }

            return null;
        }





        #region Auxiliares

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "O nome de usuário já existe. Insira um nome de usuário diferente.";

                case MembershipCreateStatus.DuplicateEmail:
                    return
                        "Já existe um nome de usuário para este endereço de email. Insira um endereço de email diferente.";

                case MembershipCreateStatus.InvalidPassword:
                    return "A senha fornecida é inválida. Insira um valor de senha válido.";

                case MembershipCreateStatus.InvalidEmail:
                    return "O endereço de email fornecido é inválido. Verifique o valor e tente novamente.";

                case MembershipCreateStatus.InvalidAnswer:
                    return
                        "A resposta de recuperação de senha fornecida é inválida. Verifique o valor e tente novamente.";

                case MembershipCreateStatus.InvalidQuestion:
                    return
                        "A pergunta de recuperação de senha fornecida é inválida. Verifique o valor e tente novamente.";

                case MembershipCreateStatus.InvalidUserName:
                    return "O nome de usuário fornecido é inválido. Verifique o valor e tente novamente.";

                case MembershipCreateStatus.ProviderError:
                    return
                        "O provedor de autenticação retornou um erro. Verifique sua entrada e tente novamente. Se o problema continuar, contate o Usuario do sistema.";

                case MembershipCreateStatus.UserRejected:
                    return
                        "A solicitação de criação do usuário foi cancelada. Verifique sua entrada e tente novamente. Se o problema continuar, contate o Usuario do sistema.";

                default:
                    return
                        "Erro desconhecido. Verifique sua entrada e tente novamente. Se o problema continuar, contate o Usuario do sistema.";
            }
        }

        #endregion
    }
}
