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
    
    public partial class tb_transacao
    {
        public long codTransacao { get; set; }
        public long codCartao { get; set; }
        public string dataTransacao { get; set; }
        public string status { get; set; }
        public long codServico { get; set; }
    
        public virtual tb_cartao tb_cartao { get; set; }
        public virtual tb_servico tb_servico { get; set; }
    }
}
