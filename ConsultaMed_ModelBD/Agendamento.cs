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
    
    public partial class Agendamento
    {
        public int AgendamentoId { get; set; }
        public int MarcadorId { get; set; }
        public System.DateTime Horario { get; set; }
        public System.DateTime Data { get; set; }
        public int PacienteUserId { get; set; }
        public int MedicoUserId { get; set; }
    
        public virtual Usuario Usuario { get; set; }
        public virtual Usuario Usuario1 { get; set; }
    }
}