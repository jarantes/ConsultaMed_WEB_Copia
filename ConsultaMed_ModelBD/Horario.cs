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
    
    public partial class Horario
    {
        public int HorarioId { get; set; }
        public System.DateTime HorarioIni { get; set; }
        public System.DateTime HorarioFim { get; set; }
        public System.DateTime TempoConsulta { get; set; }
        public int UserId { get; set; }
        public System.DateTime PerInicio { get; set; }
        public System.DateTime PerFim { get; set; }
    
        public virtual Usuario Usuario { get; set; }
    }
}