using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ConsultaMed_WEB.Models
{    
    public class Prontuario
    {
        [Key]
        public int ProntuarioId { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public IEnumerable<String> ListaExame { get; set; }

        public String Exame { get; set; }

        public DateTime DataCriacao { get; set; }

        public int UserId { get; set; }

        public int? AgendamentoId { get; set; }

        [ForeignKey("UserId")]
        public virtual UsuarioPaciente UsuarioPaciente { get; set; }

        [ForeignKey("AgendamentoId")]
        public virtual Agendamento Agendamento { get; set; }
    }
}
