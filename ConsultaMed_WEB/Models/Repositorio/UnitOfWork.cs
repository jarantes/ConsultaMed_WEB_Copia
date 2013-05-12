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
        private RepositorioGenerico<Endereco> _enderecoRepositorio;
        private RepositorioGenerico<Especialidade> _especialidadeRepositorio;
        private RepositorioGenerico<ClinicasFavorita>  _clinicaFavoritaRepositorio;
        private RepositorioGenerico<EmailConfiguration> _emailConfigurationRepositorio;
        private RepositorioGenerico<CmLog> _cmLogRepositorio;

        public RepositorioGenerico<CmLog> CmLogRepositorio
        {
            get { return _cmLogRepositorio ?? (_cmLogRepositorio = new RepositorioGenerico<CmLog>(_context)); }
        }

        public RepositorioGenerico<EmailConfiguration> EmailConfigurationRepositorio
        {
            get { return _emailConfigurationRepositorio ?? (_emailConfigurationRepositorio = new RepositorioGenerico<EmailConfiguration>(_context)); }
        }

        public RepositorioGenerico<ClinicasFavorita> ClinicaFavoritaRepositorio
        {
            get { return _clinicaFavoritaRepositorio ?? (_clinicaFavoritaRepositorio = new RepositorioGenerico<ClinicasFavorita>(_context)); }
        }

        public  RepositorioGenerico<Especialidade> EspecialidadeRepositorio
        {
            get { return _especialidadeRepositorio ?? (_especialidadeRepositorio = new RepositorioGenerico<Especialidade>(_context)); }
        }

        public RepositorioGenerico<Endereco> EnderecoRepositorio
        {
            get { return _enderecoRepositorio ?? (_enderecoRepositorio = new RepositorioGenerico<Endereco>(_context)); }
        }

        public RepositorioGenerico<Usuario> UsuarioRepositorio
        {
            get { return _usuarioRepositorio ?? (_usuarioRepositorio = new RepositorioGenerico<Usuario>(_context)); }
        }

        public RepositorioGenerico<UsuarioMedico> MedicoRepositorio
        {
            get { return _medicoRepositorio ?? (_medicoRepositorio = new RepositorioGenerico<UsuarioMedico>(_context)); }
        }

        public RepositorioGenerico<UsuarioPaciente> PacienteRepositorio
        {
            get { return _pacienteRepositorio ?? (_pacienteRepositorio = new RepositorioGenerico<UsuarioPaciente>(_context)); }
        }

        public RepositorioGenerico<Agendamento> AgendamentoRepositorio
        {
            get { return _agendamentoRepositorio ?? (_agendamentoRepositorio = new RepositorioGenerico<Agendamento>(_context)); }
        }

        public RepositorioGenerico<Clinica> ClinicaRepositorio
        {
            get { return _clinicaRepositorio ?? (_clinicaRepositorio = new RepositorioGenerico<Clinica>(_context)); }
        }

        public RepositorioGenerico<Convenio> ConvenioRepositorio
        {
            get { return _convenioRepositorio ?? (_convenioRepositorio = new RepositorioGenerico<Convenio>(_context)); }
        }

        public RepositorioGenerico<Exame> ExameRepositorio
        {
            get { return _exameRepositorio ?? (_exameRepositorio = new RepositorioGenerico<Exame>(_context)); }
        }

        public RepositorioGenerico<Horario> HorarioRepositorio
        {
            get { return _horarioRepositorio ?? (_horarioRepositorio = new RepositorioGenerico<Horario>(_context)); }
        }

        public RepositorioGenerico<HorarioTemp> HorarioTempRepositorio
        {
            get
            { return _horariotempRepositorio ?? (_horariotempRepositorio = new RepositorioGenerico<HorarioTemp>(_context)); }
        }

        public RepositorioGenerico<Prontuario> ProntuarioRepositorio
        {
            get { return _prontuarioRepositorio ?? (_prontuarioRepositorio = new RepositorioGenerico<Prontuario>(_context)); }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
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