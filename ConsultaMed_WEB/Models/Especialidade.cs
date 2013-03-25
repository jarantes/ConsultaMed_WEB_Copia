using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConsultaMed_WEB.Models
{
    public class Especialidade
    {
        [UIHint("EspecialidadeDropDownList")]
        [Key]
        public int EspecialidadeId { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public virtual ICollection<UsuarioMedico> UsuarioMedico { get; set; }
    }
}
