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
        [Display(Name = "Selecione o Hor�rio")]
        public DateTime Horario { get; set; }

        [Required]
        [Display(Name = "Selecione a data")]
        public DateTime DataAgendamento { get; set; }

        public DateTime DataCriacao { get; set; }

        public int PacienteUserId { get; set; }

        [Required]
        [Display(Name = "Selecione o M�dico")]
        public int MedicoUserId { get; set; }

        [ForeignKey("MedicoUserId")]
        public virtual UsuarioMedico UsuarioMedico { get; set; }

         [ForeignKey("PacienteUserId")]
        public virtual UsuarioPaciente UsuarioPaciente { get; set; }
    }
}
