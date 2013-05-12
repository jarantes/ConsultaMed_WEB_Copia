using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConsultaMed_WEB.Models
{
    public sealed class Convenio
    {    
        [Key]
        public int ConvenioId { get; set; }

        [Required]
        [Display(Name = "Nome do Convênio")]
        public string Descricao { get; set; }

        [Required]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        [Required]
        [Display(Name = "Site")]
        public string Site { get; set; }

        public ICollection<UsuarioPaciente> UsuarioPacientes { get; set; }
        public ICollection<Clinica> Clinicas { get; set; }
    }
}
