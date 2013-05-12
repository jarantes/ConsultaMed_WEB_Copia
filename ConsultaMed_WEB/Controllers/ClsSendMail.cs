using System;
using System.Linq;
using System.Text;
using System.Web.Helpers;
using ConsultaMed_WEB.Models;
using ConsultaMed_WEB.Models.Repositorio;

namespace ConsultaMed_WEB.Controllers
{
    public class ClsSendMail:IDisposable
    {
        //OBJETO para obetenção de parâmetros
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        public string Retorno;

        public bool Envia(string email, string assunto, string corpo)
        {
            try
            {
                //**Definições para acesso ao servidor**
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl = true;
                WebMail.UserName = "cmedsuporte@gmail.com";
                WebMail.Password = "consultamed";

                WebMail.Send(email, assunto, corpo, "cmedsuporte@gmail.com");
            }
            catch (Exception ex)
            {
                Retorno = ex.Message;
                return false;
            }
            return true;
        }


        public string CreateEmail(int type, int? medicoUserId, int? respId, int? userId, string dataConsulta, string horario, string email)
        {
            //************************************************************************
            //Tipos de Email
            // 1 - Nova Consulta 
            // 2 - Desmarcar Consulta
            // 3 - Disponibilização de Prontuário
            // 4 - Primeiro Acesso
            // 5 - Recuperar Senha
            //************************************************************************

            EmailConfiguration param;
            UsuarioMedico medico;
            Usuario resp;
            var corpo = new StringBuilder();

            switch (type)
            {
                case 1:
                    try
                    {
                        param = _unitOfWork.EmailConfigurationRepositorio.Get(m => m.TipoEmail == type).First();
                        medico = _unitOfWork.MedicoRepositorio.Get(m => m.UserId == medicoUserId).First();
                        resp = _unitOfWork.UsuarioRepositorio.Get(m => m.UserId == respId).First();
                        corpo.Append("<b>" + param.Corpo + "</b>");
                        corpo.Append("<br>");
                        corpo.Append("<br>");
                        corpo.Append("<b>Data da Consulta: </b>" + dataConsulta + " às " + horario + "hs;");
                        corpo.Append("<br>");
                        corpo.Append("<b>Médico: </b>" + medico.Nome + " " + medico.Sobrenome + ";");
                        corpo.Append("<br>");
                        corpo.Append("<b>Agendado por: </b>" + resp.Nome + " " + resp.Sobrenome + ";");
                        corpo.Append("<br>");
                        corpo.Append("<br>");
                        corpo.Append("<br>");
                        corpo.Append("<br>");
                        corpo.Append("<br>");
                        corpo.Append("<br>");
                        corpo.Append("<b>" + "Atenciosamente, Equipe ConsultaMed - " + DateTime.Now + "</b>");

                        return Envia(email, param.Assunto, corpo.ToString()) ? null : Retorno;

                    }
                    catch (Exception)
                    {
                        throw new Exception();
                    }
                    finally
                    {
                        _unitOfWork.Dispose();
                    }

                case 2:
                    try
                    {
                        param = _unitOfWork.EmailConfigurationRepositorio.Get(m => m.TipoEmail == type).First();
                        medico = _unitOfWork.MedicoRepositorio.Get(m => m.UserId == medicoUserId).First();
                        resp = _unitOfWork.UsuarioRepositorio.Get(m => m.UserId == respId).First();
                        corpo.Append("<b>" + param.Corpo + "</b>");
                        corpo.Append("<br>");
                        corpo.Append("<br>");
                        corpo.Append("<b>Data da Consulta: </b>" + dataConsulta + " às " + horario + "hs;");
                        corpo.Append("<br>");
                        corpo.Append("<b>Médico: </b>" + medico.Nome + " " + medico.Sobrenome + ";");
                        corpo.Append("<br>");
                        corpo.Append("<b>Desmarcado por: </b>" + resp.Nome + " " + resp.Sobrenome + ";");
                        corpo.Append("<br>");
                        corpo.Append("<br>");
                        corpo.Append("<br>");
                        corpo.Append("<br>");
                        corpo.Append("<br>");
                        corpo.Append("<br>");
                        corpo.Append("<b>" + "Atenciosamente, Equipe ConsultaMed - " + DateTime.Now + "</b>");

                        return Envia(email, param.Assunto, corpo.ToString()) ? null : Retorno;
                    }
                    catch (Exception)
                    {
                        throw new Exception();
                    }
                    finally
                    {
                        _unitOfWork.Dispose();
                    }

                case 3:
                    try
                    {
                        param = _unitOfWork.EmailConfigurationRepositorio.Get(m => m.TipoEmail == type).First();
                        medico = _unitOfWork.MedicoRepositorio.Get(m => m.UserId == medicoUserId).First();
                        corpo.Append("<b>" + param.Corpo + "</b>");
                        corpo.Append("<br>");
                        corpo.Append("<br>");
                        corpo.Append("<b>Data da Consulta: </b>" + dataConsulta + " às " + horario + "hs;");
                        corpo.Append("<br>");
                        corpo.Append("<b>Médico: </b>" + medico.Nome + " " + medico.Sobrenome + ";");
                        corpo.Append("<br>");
                        corpo.Append("<br>");
                        corpo.Append("<br>");
                        corpo.Append("<br>");
                        corpo.Append("<br>");
                        corpo.Append("<br>");
                        corpo.Append("<b>" + "Atenciosamente, Equipe ConsultaMed - " + DateTime.Now + "</b>");

                        return Envia(email, param.Assunto, corpo.ToString()) ? null : Retorno;
                    }
                    catch (Exception)
                    {
                        throw new Exception();
                    }
                    finally
                    {
                        _unitOfWork.Dispose();
                    }

                case 4:
                    param = _unitOfWork.EmailConfigurationRepositorio.Get(m => m.TipoEmail == 4).First();
                    resp = _unitOfWork.PacienteRepositorio.Get(m => m.UserId == userId).First();
                    corpo.Append("<b>" + "Bem vindo" + " " + resp.Nome + " " + resp.Sobrenome + "!</b>");
                    corpo.Append("<br>");
                    corpo.Append(param.Corpo);
                    corpo.Append("<br>");
                    corpo.Append("<br>");
                    corpo.Append("<br>");
                    corpo.Append("<br>");
                    corpo.Append("<br>");
                    corpo.Append("<br>");
                    corpo.Append("<br>");
                    corpo.Append("<br>");
                    corpo.Append("<b>" + "Atenciosamente, Equipe ConsultaMed - " + DateTime.Now + "</b>");
                    break;

            }

            return null;
        }



        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _unitOfWork.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }



}