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
        [Display(Name = "Horário Inicial")]
        public TimeSpan HorarioIni { get; set; }

        [Required]
        [Display(Name = "Horário Final")]
        public TimeSpan HorarioFim { get; set; }

        [Required]
        [Display(Name = "Tempo médio da Consulta")]
        public TimeSpan TempoConsulta { get; set; }

        [Required]
        [Display(Name = "Horário de Descanso Início")]
        public TimeSpan TempoDescansoInicial { get; set; }

        [Required]
        [Display(Name = "Horário de Descanso Fim")]
        public TimeSpan TempoDescansoFinal { get; set; }

        [Required]
        [Display(Name = "Data Inicial Período")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PerInicio { get; set; }

        [Required]
        [Display(Name = "Data final Período")]
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
