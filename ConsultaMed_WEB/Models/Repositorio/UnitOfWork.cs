using System;

namespace ConsultaMed_WEB.Models.Repositorio
{
    public class UnitOfWork : IDisposable
    {
        private readonly CmContext _context = new CmContext();
        private RepositorioGenerico<UsuarioMedico> _medicoRepositorio;
        private RepositorioGenerico<UsuarioPaciente> _pacienteRepositorio;
        private RepositorioGenerico<Agendamento> _agendamentoRepositorio;
        private RepositorioGenerico<Clinica> _clinicaRepositorio;
        private RepositorioGenerico<Convenio> _convenioRepositorio;
        private RepositorioGenerico<Exame> _exameRepositorio;
        private RepositorioGenerico<Horario> _horarioRepositorio;
        private RepositorioGenerico<HorarioTemp> _horariotempRepositorio;
        private RepositorioGenerico<Prontuario> _prontuarioRepositorio;
        private RepositorioGenerico<Usuario> _usuarioRepositorio;

        public RepositorioGenerico<Usuario> UsuarioRepositorio
        {
            get
            {

                if (this._usuarioRepositorio == null)
                {
                    this._usuarioRepositorio = new RepositorioGenerico<Usuario>(_context);
                }
                return _usuarioRepositorio;
            }
        }

        public RepositorioGenerico<UsuarioMedico> MedicoRepositorio
        {
            get
            {

                if (this._medicoRepositorio == null)
                {
                    this._medicoRepositorio = new RepositorioGenerico<UsuarioMedico>(_context);
                }
                return _medicoRepositorio;
            }
        }

        public RepositorioGenerico<UsuarioPaciente> PacienteRepositorio
        {
            get
            {

                if (this._pacienteRepositorio == null)
                {
                    this._pacienteRepositorio = new RepositorioGenerico<UsuarioPaciente>(_context);
                }
                return _pacienteRepositorio;
            }
        }

        public RepositorioGenerico<Agendamento> AgendamentoRepositorio
        {
            get
            {

                if (this._agendamentoRepositorio == null)
                {
                    this._agendamentoRepositorio = new RepositorioGenerico<Agendamento>(_context);
                }
                return _agendamentoRepositorio;
            }
        }

        public RepositorioGenerico<Clinica> ClinicaRepositorio
        {
            get
            {

                if (this._clinicaRepositorio == null)
                {
                    this._clinicaRepositorio = new RepositorioGenerico<Clinica>(_context);
                }
                return _clinicaRepositorio;
            }
        }

        public RepositorioGenerico<Convenio> ConvenioRepositorio
        {
            get
            {

                if (this._convenioRepositorio == null)
                {
                    this._convenioRepositorio = new RepositorioGenerico<Convenio>(_context);
                }
                return _convenioRepositorio;
            }
        }

        public RepositorioGenerico<Exame> ExameRepositorio
        {
            get
            {

                if (this._exameRepositorio == null)
                {
                    this._exameRepositorio = new RepositorioGenerico<Exame>(_context);
                }
                return _exameRepositorio;
            }
        }

        public RepositorioGenerico<Horario> HorarioRepositorio
        {
            get
            {

                if (this._horarioRepositorio == null)
                {
                    this._horarioRepositorio = new RepositorioGenerico<Horario>(_context);
                }
                return _horarioRepositorio;
            }
        }

        public RepositorioGenerico<HorarioTemp> HorarioTempRepositorio
        {
            get
            {

                if (this._horariotempRepositorio == null)
                {
                    this._horariotempRepositorio = new RepositorioGenerico<HorarioTemp>(_context);
                }
                return _horariotempRepositorio;
            }
        }

        public RepositorioGenerico<Prontuario> ProntuarioRepositorio
        {
            get
            {

                if (this._prontuarioRepositorio == null)
                {
                    this._prontuarioRepositorio = new RepositorioGenerico<Prontuario>(_context);
                }
                return _prontuarioRepositorio;
            }
        }
      
        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}