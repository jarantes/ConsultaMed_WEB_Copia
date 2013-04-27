using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultaMed_WEB.Models
{
    public class Agendamento
    {
        [Key]
        public int AgendamentoId { get; set; }

        public int MarcadorId { get; set; }

        [Required]
        [Display(Name = "Selecione o Horário")]
        public TimeSpan Horario { get; set; }

        [Required]
        [Display(Name = "Selecione a data")]
        public DateTime DataConsulta { get; set; }

        public DateTime DataCriacao { get; set; }

        public int PacienteUserId { get; set; }

        public int? HorarioId { get; set; }

        public int? HorarioTempId { get; set; }

        [Required]
        [Display(Name = "Selecione o Médico")]
        public int MedicoUserId { get; set; }

        [ForeignKey("MedicoUserId")]
        public virtual UsuarioMedico UsuarioMedico { get; set; }

        [ForeignKey("PacienteUserId")]
        public virtual UsuarioPaciente UsuarioPaciente { get; set; }

        [ForeignKey("HorarioId")]
        public virtual Horario Horarios { get; set; }

        [ForeignKey("HorarioTempId")]
        public virtual HorarioTemp HorarioTemp { get; set; }


    }
}
