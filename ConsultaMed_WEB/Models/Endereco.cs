using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultaMed_WEB.Models
{ 
    public class Endereco
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int EnderecoId { get; set; }

        [Required]
        [Display(Name = "Rua")]
        public string Rua { get; set; }

        [Required]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }

        [Required]
        [Display(Name = "Bairro")]
        public string Bairro { get; set; }

        [Required]
        [Display(Name = "Numero")]
        public int Numero { get; set; }

        public string Complemento { get; set; }

        [Required]
        [Display(Name = "Cep")]
        public string Cep { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public string Estado { get; set; }
     
        public virtual ICollection<Usuario> Usuario { get; set; }

        public virtual ICollection<Clinica> Clinica { get; set; } 
    }
}
