using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultaMed_WEB.Models
{    
    public class Prontuario
    {
        [Key]
        public int ProntuarioId { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }


        public DateTime DataCriacao { get; set; }

        public int ExameId { get; set; }

        public int UserId { get; set; }

        [ForeignKey("ExameId")]
        public virtual Exame Exame { get; set; }

        [ForeignKey("UserId")]
        public virtual UsuarioPaciente UsuarioPaciente { get; set; }
    }
}
