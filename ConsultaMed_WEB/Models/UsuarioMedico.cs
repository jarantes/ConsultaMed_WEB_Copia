using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ConsultaMed_WEB.Models
{
    public class UsuarioMedico : Usuario
    {

        [Required]
        [Display(Name = "CRM")]        
        public string Crm { get; set; }
        public int EspecialidadeId { get; set; }

        [ForeignKey("EspecialidadeId")]
        public virtual Especialidade Especialidade { get; set; }

        public virtual ICollection<Agendamento> Agendamento { get; set; }
        public virtual ICollection<Horario> Horario { get; set; }
        public virtual ICollection<HorarioTemp> HorarioTemp { get; set; }
    }
}
