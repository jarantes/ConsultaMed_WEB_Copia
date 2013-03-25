using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConsultaMed_WEB.Models
{    
    public class Exame
    { 
        [Key]
        public int ExameId { get; set; }
        
        [Required]
        [Display(Name = "Descri��o")]
        public string Descricao { get; set; }

        public virtual ICollection<Prontuario> Prontuario { get; set; }
    }
}
