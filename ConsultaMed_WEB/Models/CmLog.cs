using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultaMed_WEB.Models
{
    public class CmLog
    {
        [Key]
        public int LogId { get; set; }

        [Required]
        public string DescricaoAcao { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime DataCriacao { get; set; }

        [ForeignKey("UserId")]
        public virtual Usuario Usuario { get; set; }
    }
}