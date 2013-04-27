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
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }

        [Required]
        [Display(Name = "Hor�rio de Descanso In�cio")]
        public TimeSpan TempoDescansoInicial { get; set; }

        [Required]
        [Display(Name = "Hor�rio de Descanso Fim")]
        public TimeSpan TempoDescansoFinal { get; set; }

        
        public int MedicoUserId { get; set; }

        [ForeignKey("MedicoUserId")]
        public virtual UsuarioMedico UsuarioMedico { get; set; }
    }
}
