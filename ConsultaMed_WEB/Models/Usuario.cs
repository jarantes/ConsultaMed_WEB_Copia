using System;
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
        [DataType(DataType.EmailAddress, ErrorMessage = "O email informado � inv�lido.")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [Required]
        [Display(Name = "RG")]
        public string Rg { get; set; }

        [Required]
        [Display(Name = "Telefone")]
        //TODO verificar melhores regex
        //[RegularExpression(@"^([0-9]{2}) [0-9]{4}-[0-9]{4}$", ErrorMessage = "O telefone informado n�o � v�lido")]
        public string Telefone { get; set; }

        [Display(Name = "Celular")]
        //TODO verificar melhores regex
        //[RegularExpression(@"^(\(11\) [9][0-9]{4}-[0-9]{4})|(\(1[2-9]\) [5-9][0-9]{3}-[0-9]{4})|(\([2-9][1-9]\) [5-9][0-9]{3}-[0-9]{4})$", ErrorMessage = "O celular informado n�o � v�lido")]
        public string Celular { get; set; }

        [Required]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date, ErrorMessage = "A data informada � inv�lida.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd//MM/yyyy}")]
        public DateTime DataNascimento { get; set; }

        public string Perfil { get; set; }

        public int ClinicaId { get; set; }

        public int EnderecoId { get; set; }

        [ForeignKey("EnderecoId")]
        public virtual Endereco Endereco { get; set; }

        [ForeignKey("ClinicaId")]
        public virtual Clinica Clinica { get; set; }

    }
}