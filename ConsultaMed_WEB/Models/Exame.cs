using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConsultaMed_WEB.Models
{    
    public class Exame
    { 
        [Key]
        public int ExameId { get; set; }

        [Required]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public virtual ICollection<Especialidade> Especialidades  { get; set; }
    }
}
