//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace APIRestTedie
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    
    public partial class PEDIDO_ITEM
    {
        [JsonIgnore]
        public int NUMERO_PEDIDO { get; set; }
        public int IDPRODUTO { get; set; }
        public Nullable<int> QTDE { get; set; }
        public Nullable<double> VALOR { get; set; }
        public Nullable<double> VALOR_PROMOCIONAL { get; set; }
        public string STATUS { get; set; }
        public Nullable<double> VALOR_UNIT { get; set; }
    }
}
