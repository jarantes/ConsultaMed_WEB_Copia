using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultaMed_WEB.Models
{    
    [Table("Usuario")]

    public abstract class Usuario
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Sobrenome")]
        public string Sobrenome { get; set; }

        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "O email informado é inválido.")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [Required]
        [Display(Name = "RG")]
        public string Rg { get; set; }

        [Required]
        [Display(Name = "Telefone")]
        [RegularExpression(@"^\([0-9]{2}\) [0-9]{4}-[0-9]{4}$", ErrorMessage = "Número de telefone inválido")]
        public string Telefone { get; set; }

        [Display(Name = "Celular")]
        //[RegularExpression(@"^\([0-9]{3}\) [6-9]{1}[0-9]{1}-[0-9]{4}$", ErrorMessage = "Número de celular inválido")]
        public string Celular { get; set; }

        [Required]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date, ErrorMessage = "A data informada é inválida."), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataNascimento { get; set; }

        public int? ClinicaId { get; set; }

        public string Perfil { get; set; }

        public int EnderecoId { get; set; }

        [ForeignKey("EnderecoId")]
        public virtual Endereco Endereco { get; set; }

        [ForeignKey("ClinicaId")]
        public virtual Clinica Clinica { get; set; }

        public ICollection<CmLog> CmLogs { get; set; }
    }
}
