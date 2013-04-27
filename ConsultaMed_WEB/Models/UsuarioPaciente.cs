using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultaMed_WEB.Models
{    
    public class UsuarioPaciente : Usuario
    {
        public string NumeroConvenio { get; set; }

        public int ConvenioId { get; set; }

        //public ICollection<Agendamento> Agendamento { get; set; }
        public ICollection<Prontuario> Prontuario { get; set; }

        [ForeignKey("ConvenioId")]
        public virtual Convenio Convenio { get; set; }



    }
}
