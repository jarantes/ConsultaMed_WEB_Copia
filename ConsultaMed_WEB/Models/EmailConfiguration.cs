using System;
using System.ComponentModel.DataAnnotations;

namespace ConsultaMed_WEB.Models
{
    public class EmailConfiguration
    {
        [Key]
        public int TypeId { get; set; }

        [Required]
        public int  TipoEmail { get; set; }

        [Required]
        public String Assunto { get; set; }

        [Required]
        public String Corpo { get; set; }

        public String Descriao { get; set; }
    }
}