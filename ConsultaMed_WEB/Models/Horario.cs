using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultaMed_WEB.Models
{    
    public class Horario
    {
        [Key]
        public int HorarioId { get; set; }

        [Required]
        [Display(Name="Horário Inicial")]
        public TimeSpan HorarioIni { get; set; }

        [Required]
        [Display(Name = "Horário Final")]
        public TimeSpan HorarioFim { get; set; }

        [Required]
        [Display(Name = "Tempo médio da Consulta")]
        public TimeSpan TempoConsulta { get; set; }

        [Required]
        [Display(Name = "Período")]
        public DateTime PerInicio { get; set; }
        
        [Required]
        [Display(Name = "data final do período")]
        public DateTime PerFim { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual UsuarioMedico UsuarioMedico { get; set; }
    }
}
