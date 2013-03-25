using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultaMed_WEB.Models
{
    public class Clinica
    {
        [UIHint("ClinicaDropDownList")]
        [Key]
        public int ClinicaId { get; set; }

        [Required]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Descri��o")]
        public string Descricao { get; set; }

        [Display(Name = "Site")]
        public string Site { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Atendimento")]
        [RegularExpression(@"^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Formato de hora inv�lido")]
        public string HorarioInicial { get; set; }

        [Required]
        [Display(Name = "�s ")]
        [RegularExpression(@"^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Formato de hora inv�lido")]
        public string HorarioFinal { get; set; }

        public int EnderecoId { get; set; }

        [ForeignKey("EnderecoId")]
        public virtual Endereco Endereco { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
        public virtual ICollection<Convenio> Convenios { get; set; }
    }
}
 
