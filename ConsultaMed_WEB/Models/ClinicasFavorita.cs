using System.ComponentModel.DataAnnotations;

namespace ConsultaMed_WEB.Models
{
    public class ClinicasFavorita
    {
        [Key]
        public int FavoritaId { get; set; }

        public int ClinicaId { get; set; }

        public int UserId { get; set; }

        public int Ordem { get; set; }
    }

}