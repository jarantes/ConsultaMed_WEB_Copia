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

        //-------------------------------------INÍCIO DAS ACTIONS DO ADMINISTRADOR--------------------------------
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
        [AllowAnonymous]
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
                    return RedirectToAction(model.Perfil == "Medico" ? "AddMedico" : "AddUserClinica");
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
            return View();
        }

        //
        // POST: /Usuario/AddMedico
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult AddMedico(UsuarioMedico medico)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    medico.UserName = (string)Session["UserNameCadastrado"];
                    medico.UserId =
                        _unitOfWork.UsuarioRepositorio.GetIdByUserName((string)Session["UserNameCadastrado"]);
                    medico.Perfil = "Medico";
                    _unitOfWork.MedicoRepositorio.Insert(medico);
                    _unitOfWork.Save();
                    _unitOfWork.Dispose();
                    Session.Add("Mensagem", "Médico adiciondado com sucesso");
                    return RedirectToAction("Registrar");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Não foi possível adicionar o novo Médico");
            }
            return View(medico);
        }

        //
        // GET: /Usuario/AddUserClinica
        [Authorize(Roles = "Administrador")]
        public ActionResult AddUserClinica()
        {
            return View();
        }

        //
        // POST: /Usuario/AddUserClinica
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult AddUserClinica(UsuarioRespClinica userClinica)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    userClinica.UserName = (string)Session["UserNameCadastrado"];
                    userClinica.UserId =
                        _unitOfWork.UsuarioRepositorio.GetIdByUserName((string)Session["UserNameCadastrado"]);
                    userClinica.Perfil = "RespClinica";
                    _unitOfWork.UsuarioRepositorio.Insert(userClinica);
                    _unitOfWork.Save();
                    _unitOfWork.Dispose();
                    Session.Add("Mensagem", "Usuário adiciondado com sucesso");
                    return RedirectToAction("Registrar");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Não foi possível adicionar o Usuário");
            }
            return View(userClinica);
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
                    ctuser.Dispose();
                }
                //deletando Usuário
                _unitOfWork.UsuarioRepositorio.Delete(id);
                _unitOfWork.Save();
                _unitOfWork.Dispose();

                Session.Add("DeleteUser", "Usuário removido com sucesso");
                return RedirectToAction("Gerenciar");
            }
            catch (Exception)
            {
                Session.Add("Erro", "Não foi possível excluir o usuário");
            }
            return RedirectToAction("Gerenciar");
        }

        //
        // GET: /Usuario/Listar
        public ActionResult Listar()
        {
            try
            {
                var medicoId = _unitOfWork.UsuarioRepositorio.GetIdByUserName(User.Identity.Name);
                var model = _unitOfWork.UsuarioRepositorio.GetUsersforDoctor(medicoId, DateTime.Now, DateTime.Now.AddDays(7));
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

        //--------------------------------FIM DAS ACTIONS DO ADMINISTRADOR----------------------------------------



        //----------------------------------INICIO DAS ACTIONS DO MÉDICO------------------------------------------

        //TODO => não implemtendo

        //-------------------------------------FIM DAS ACTIONS DO MÉDICO------------------------------------------



        //-----------------------------------INICIO DAS ACTIONS DO USUÁRIO----------------------------------------

        //TODO => não implemtendo

        //-------------------------------------FIM DAS ACTIONS DO USUÁRIO-----------------------------------------


        //----------------------------------INICIO DAS ACTIONS DO USERCLINICA-------------------------------------

        //TODO => não implemtendo

        //-----------------------------------FIM DAS ACTIONS DO USERCLINICA---------------------------------------
        //
        //POST: Usuario




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
