using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultaMed_WEB.Models
{    
    public class HorarioTemp
    {
        [Key]
        public int HorarioTempId { get; set; }

        [Required]
        [Display(Name = "Hor�rio Inicial")]
        public TimeSpan HorarioIni { get; set; }

        [Required]
        [Display(Name = "Hor�rio Final")]
        public TimeSpan HorarioFim { get; set; }

        [Required]
        [Display(Name = "Tempo m�dio da Consulta")]
        public TimeSpan TempoConsulta { get; set; }

        [Required]
        [Display(Name = "Data")]
        public DateTime Data { get; set; }
        
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual UsuarioMedico UsuarioMedico { get; set; }
    }
}
