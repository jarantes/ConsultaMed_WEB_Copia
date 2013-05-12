using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultaMed_WEB.Models
{
    public class Horario
    {
        [Key]
        public int HorarioId { get; set; }

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
        [Display(Name = "Hor�rio de Descanso In�cio")]
        public TimeSpan TempoDescansoInicial { get; set; }

        [Required]
        [Display(Name = "Hor�rio de Descanso Fim")]
        public TimeSpan TempoDescansoFinal { get; set; }

        [Required]
        [Display(Name = "Data Inicial Per�odo")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PerInicio { get; set; }

        [Required]
        [Display(Name = "Data final Per�odo")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PerFim { get; set; }

        [Required]
        public int MedicoUserId { get; set; }

        //[ForeignKey("MedicoUserId")]
        //public virtual UsuarioMedico UsuarioMedico { get; set; }

        public virtual ICollection<Horario> Horarios { get; set; }
        public virtual ICollection<HorarioTemp> HorarioTemps { get; set; }
    }
}
