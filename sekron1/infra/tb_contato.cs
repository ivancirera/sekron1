//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace sekron1.infra
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_contato
    {
        public long codContato { get; set; }
        public long codUsuario { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string telefone { get; set; }
    
        public virtual tb_usuario tb_usuario { get; set; }
    }
}
