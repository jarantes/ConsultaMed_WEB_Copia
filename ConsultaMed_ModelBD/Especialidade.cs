//------------------------------------------------------------------------------
// <auto-generated>
//    O código foi gerado a partir de um modelo.
//
//    Alterações manuais neste arquivo podem provocar comportamento inesperado no aplicativo.
//    Alterações manuais neste arquivo serão substituídas se o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsultaMed_ModelBD
{
    using System;
    using System.Collections.Generic;
    
    public partial class Especialidade
    {
        public Especialidade()
        {
            this.Usuario = new HashSet<Usuario>();
        }
    
        public int EspecialidadeId { get; set; }
        public string Descricao { get; set; }
    
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}